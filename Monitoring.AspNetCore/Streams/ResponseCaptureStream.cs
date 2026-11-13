using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.AspNetCore.Streams
{
    public class ResponseCaptureStream : Stream
    {
        private readonly Stream _originalStream;
        private readonly MemoryStream _captureStream = new();
        private readonly long _maxCaptureSize;

        public ResponseCaptureStream(Stream originalStream, long maxCaptureSize)
        {
            _originalStream = originalStream;
            _maxCaptureSize = maxCaptureSize;
        }

        public byte[] GetCapturedBytes()
        {
            return _captureStream.ToArray();
        }

        private void Capture(ReadOnlySpan<byte> buffer)
        {
            var remaining = _maxCaptureSize - _captureStream.Length;

            if (remaining <= 0)
                return;

            var bytesToCapture = (int)Math.Min(buffer.Length, remaining);

            _captureStream.Write(buffer[..bytesToCapture]);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _originalStream.Write(buffer, offset, count);
            Capture(buffer.AsSpan(offset, count));
        }

        public override void Write(ReadOnlySpan<byte> buffer)
        {
            _originalStream.Write(buffer);
            Capture(buffer);
        }

        public override async Task WriteAsync(
            byte[] buffer,
            int offset,
            int count,
            CancellationToken cancellationToken)
        {
            await _originalStream.WriteAsync(
                buffer, offset, count, cancellationToken);

            Capture(buffer.AsSpan(offset, count));
        }

        public override async ValueTask WriteAsync(
            ReadOnlyMemory<byte> buffer,
            CancellationToken cancellationToken = default)
        {
            await _originalStream.WriteAsync(buffer, cancellationToken);
            Capture(buffer.Span);
        }

        public override void Flush() => _originalStream.Flush();

        public override Task FlushAsync(CancellationToken cancellationToken)
            => _originalStream.FlushAsync(cancellationToken);

        public override bool CanRead => false;
        public override bool CanSeek => false;
        public override bool CanWrite => _originalStream.CanWrite;

        public override long Length => throw new NotSupportedException();

        public override long Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
            => throw new NotSupportedException();

        public override long Seek(long offset, SeekOrigin origin)
            => throw new NotSupportedException();

        public override void SetLength(long value)
            => throw new NotSupportedException();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _captureStream.Dispose();

            base.Dispose(disposing);
        }

        public override async ValueTask DisposeAsync()
        {
            await _captureStream.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}

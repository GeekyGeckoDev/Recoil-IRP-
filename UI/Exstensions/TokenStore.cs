namespace UI.Exstensions
{
    public class TokenStore : ITokenStore
    {
        private readonly Guid _instanceId = Guid.NewGuid();
        private string? _token;

        public Task SetAsync(string token)
        {
            Console.WriteLine($"TOKEN SET - Instance: {_instanceId}");

            _token = token;
            return Task.CompletedTask;
        }

        public Task<string?> GetAsync()
        {
            Console.WriteLine(
                $"TOKEN GET - Instance: {_instanceId}, HasToken: {!string.IsNullOrEmpty(_token)}");

            return Task.FromResult(_token);
        }
    }
}

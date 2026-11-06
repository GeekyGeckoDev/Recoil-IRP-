namespace UI.Exstensions
{
    public class TokenStore : ITokenStore
    {
        private readonly Guid _instanceId = Guid.NewGuid();
        private string? _token;

        public Task SetAsync(string token)
        {
            _token = token;
            return Task.CompletedTask;
        }

        public Task<string?> GetAsync()
        {
            return Task.FromResult(_token);
        }
    }
}

namespace UI.Exstensions
{
    public class AuthHeaderHandler : DelegatingHandler
    {
        private readonly ITokenStore _store;

        public AuthHeaderHandler(ITokenStore store)
        {
            _store = store;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = await _store.GetAsync();

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}

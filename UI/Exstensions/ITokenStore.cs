namespace UI.Exstensions
{
    public interface ITokenStore
    {
        Task SetAsync(string token);
        Task<string?> GetAsync();
    }
}

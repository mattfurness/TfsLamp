namespace TfsLamp.Infrastructure.Configuration
{
    public interface ITfsConnectionConfig
    {
        string Server { get; }
        string Username { get; }
        string Password { get; }
    }
}
namespace TfsLamp.Infrastructure.Configuration
{
    public class TfsConnectionConfig : ITfsConnectionConfig
    {
        private readonly string _server;
        private readonly string _username;
        private readonly string _password;

        public TfsConnectionConfig(string server, string username, string password)
        {
            _server = server;
            _username = username;
            _password = password;
        }

        public virtual string Server
        {
            get { return _server; }
        }

        public virtual string Username
        {
            get { return _username; }
        }

        public virtual string Password
        {
            get { return _password; }
        }
    }
}
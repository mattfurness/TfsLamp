using Autofac;
using PowerArgs;

namespace TfsLamp.Console.Configuration
{
    public abstract class StandardArgs
    {
        [ArgRequired(PromptIfMissing = false)]
        [ArgDescription("The TFS server to connect to.")]
        public string Server { get; set; }
        [ArgRequired(PromptIfMissing = false)]
        [ArgDescription("The username to use when connecting to the TFS server.")]
        public string Username { get; set; }
        [ArgRequired(PromptIfMissing = false)]
        [ArgDescription("The password to use when connecting to the TFS server.")]
        public string Password { get; set; }
        [ArgRequired(PromptIfMissing = false)]
        [ArgDescription("The full path and file name of the output file to generate. If it exists it will be overwriten.")]
        public string OutputFile { get; set; }

        public abstract Module GetRegistrar();
    }
}
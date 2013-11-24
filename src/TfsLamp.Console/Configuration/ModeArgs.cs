using PowerArgs;

namespace TfsLamp.Console.Configuration
{
    public class ModeArgs
    {
        [ArgShortcut("m")]
        [ArgDescription("The mode to put TFS lamp in.")]
        public Mode Mode { get; set; }
    }
}
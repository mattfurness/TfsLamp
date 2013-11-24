using Autofac;
using PowerArgs;
using TfsLamp.Infrastructure.Registration;

namespace TfsLamp.Console.Configuration
{
    public class MergeCandidatesArgs : StandardArgs
    {
        [ArgShortcut("fb")]
        [ArgDescription("The from or source branch.")]
        [ArgRequired(PromptIfMissing = false)]
        public string FromBranch { get; set; }
        [ArgShortcut("tb")]
        [ArgRequired(PromptIfMissing = false)]
        [ArgDescription("The to or target branch.")]
        public string ToBranch { get; set; }

        public override Module GetRegistrar()
        {
            return new MergeCAndidatesRegistrar(FromBranch, ToBranch);
        }
    }
}
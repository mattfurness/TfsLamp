using Autofac;
using PowerArgs;
using TfsLamp.Infrastructure.Registration;

namespace TfsLamp.Console.Configuration
{
    public class MergedChangesetsArgs : StandardArgs
    {
        [ArgShortcut("fb")]
        [ArgDescription("The from or source branch.")]
        [ArgRequired(PromptIfMissing = false)]
        public string FromBranch { get; set; }
        [ArgShortcut("tb")]
        [ArgDescription("The to or target branch.")]
        [ArgRequired(PromptIfMissing = false)]
        public string ToBranch { get; set; }
        [ArgShortcut("c")]
        [ArgDescription("The to or latest / highest changeset.")]
        [ArgRequired(PromptIfMissing = false)]
        public int Changeset { get; set; }

        public override Module GetRegistrar()
        {
            return new MergedChangesetsRegistrar(FromBranch, ToBranch, Changeset);
        }
    }
}
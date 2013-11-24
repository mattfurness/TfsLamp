using Autofac;
using PowerArgs;
using TfsLamp.Infrastructure.Registration;

namespace TfsLamp.Console.Configuration
{
    public class ChangesetRangeArgs : StandardArgs
    {
        [ArgShortcut("b")]
        [ArgDescription("The source branch.")]
        [ArgRequired(PromptIfMissing = false)]
        public string Branch { get; set; }
        [ArgDescription("The from or earliest / lowest changeset.")]
        [ArgShortcut("fc")]
        [ArgRequired(PromptIfMissing = false)]
        public int FromChangeset { get; set; }
        [ArgShortcut("tc")]
        [ArgDescription("The to or latest / highest changeset.")]
        [ArgRequired(PromptIfMissing = false)]
        public int ToChangeset { get; set; }

        public override Module GetRegistrar()
        {
            return new ChangesetRangeRegistrar(Branch, FromChangeset, ToChangeset);
        }
    }
}
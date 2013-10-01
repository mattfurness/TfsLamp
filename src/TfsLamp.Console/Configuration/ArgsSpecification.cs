using PowerArgs;
using TfsLamp.Infrastructure.Configuration;

namespace TfsLamp.Console.Configuration
{
    public class ArgsSpecification
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

        [ArgShortcut("fb")]
        [ArgDescription("The from or source branch. If using fromchangeset and tochangeset use this argument to specify the branch to use.")]
        public string FromBranch { get; set; }
        [ArgShortcut("tb")]
        [ArgDescription("The to or target branch.")]
        public string ToBranch { get; set; }
        [ArgShortcut("fc")]
        [ArgDescription("The from or earliest / lowest changeset.")]
        public int? FromChangeset { get; set; }
        [ArgShortcut("tc")]
        [ArgDescription("The to or latest / highest changeset.")]
        public int? ToChangeset { get; set; }

        public bool IsChangesetRange() {  return !string.IsNullOrEmpty(FromBranch) && FromChangeset != null && ToChangeset != null; }
        public bool IsMerge() {  return !string.IsNullOrEmpty(FromBranch) && !string.IsNullOrEmpty(ToBranch); }
        public bool IsAlreadyMerged() { return !string.IsNullOrEmpty(FromBranch) && !string.IsNullOrEmpty(ToBranch) && ToChangeset != null; }

        public void Validate()
        {
            if (!IsChangesetRange() && !IsMerge() && !IsAlreadyMerged())
                throw new ConfigurationException("Please supply either a valid source and target branch, or valid from and to changesets on a branch");

            if (IsChangesetRange() && IsMerge() && IsAlreadyMerged())
                throw new ConfigurationException("Please supply either a valid source and target branch, OR valid from and to changesets on a branch. Please do not specify both, they are mutually exclusive.");
        }
    }
}
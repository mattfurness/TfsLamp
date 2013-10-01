namespace TfsLamp.Infrastructure.Configuration
{
    public interface ITfsMergedChangesetConfig
    {
        string FromBranch { get; }
        string ToBranch { get; }
        int ChangesetId { get; }
    }

    public class TfsMergedChangesetConfig : ITfsMergedChangesetConfig
    {
        public TfsMergedChangesetConfig(string fromBranch, string toBranch, int changesetId)
        {
            FromBranch = fromBranch;
            ToBranch = toBranch;
            ChangesetId = changesetId;
        }

        public string FromBranch { get; private set; }
        public string ToBranch { get; private set; }
        public int ChangesetId { get; private set; }
    }
}
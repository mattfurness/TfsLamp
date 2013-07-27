namespace TfsLamp.Infrastructure.Configuration
{
    public class TfsChangesetRangeConfig : ITfsChangesetRangeConfig
    {
        private readonly string _branch;
        private readonly int _firstChangesetId;
        private readonly int _lastChangesetId;

        public TfsChangesetRangeConfig(string branch, int firstChangesetId, int lastChangesetId)
        {
            _branch = branch;
            _firstChangesetId = firstChangesetId;
            _lastChangesetId = lastChangesetId;
        }

        public virtual int FirstChangesetId
        {
            get { return _firstChangesetId; }
        }

        public virtual int LastChangesetId
        {
            get { return _lastChangesetId; }
        }

        public virtual string Branch
        {
            get { return _branch; }
        }
    }
}
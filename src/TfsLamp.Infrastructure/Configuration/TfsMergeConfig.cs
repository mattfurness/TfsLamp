namespace TfsLamp.Infrastructure.Configuration
{
    public class TfsMergeConfig : ITfsMergeConfig
    {
        private readonly string _fromBranch;
        private readonly string _toBranch;

        public TfsMergeConfig(string toBranch, string fromBranch)
        {
            _toBranch = toBranch;
            _fromBranch = fromBranch;
        }

        public virtual string FromBranch
        {
            get { return _fromBranch; }
        }

        public virtual string ToBranch
        {
            get { return _toBranch; }
        }
    }
}
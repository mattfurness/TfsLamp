using Autofac;
using TfsLamp.Infrastructure.ChangeInfo;
using TfsLamp.Infrastructure.Configuration;

namespace TfsLamp.Infrastructure.Registration
{
    public class MergedChangesetsRegistrar : Module
    {
        private readonly TfsMergedChangesetConfig _changesetRangeConfig;

        public MergedChangesetsRegistrar(string fromBranch, string toBranch, int mergeChangeset)
        {
            _changesetRangeConfig = new TfsMergedChangesetConfig(fromBranch, toBranch, mergeChangeset);
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (_changesetRangeConfig != null)
            {
                builder.RegisterInstance(_changesetRangeConfig).As<ITfsMergedChangesetConfig>();
                builder.RegisterType<MergedChangesetRetiever>().As<IChangesetRetriever>();
            }
        }
    }
}
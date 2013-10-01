using Autofac;
using TfsLamp.Infrastructure.ChangeInfo;
using TfsLamp.Infrastructure.Configuration;
using TfsLamp.Infrastructure.Connection;

namespace TfsLamp.Infrastructure.Registration
{
    public class ChangesetRangeRegistrar : Module
    {
        private readonly ITfsChangesetRangeConfig _changesetRangeConfig;

        public ChangesetRangeRegistrar(string fromBranch, int firstChangesetId, int lastChangesetId)
        {
            _changesetRangeConfig = new TfsChangesetRangeConfig(fromBranch, firstChangesetId, lastChangesetId);
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (_changesetRangeConfig != null)
            {
                builder.RegisterInstance(_changesetRangeConfig).As<ITfsChangesetRangeConfig>();
                builder.RegisterType<ChangesetRangeChangesetRetiever>().As<IChangesetRetriever>();
            }
        }
    }
}
using Autofac;
using TfsLamp.Infrastructure.ChangeInfo;
using TfsLamp.Infrastructure.Configuration;
using TfsLamp.Infrastructure.Connection;

namespace TfsLamp.Infrastructure.Registration
{
    public class PotentialMergeRegistrar : Module
    {
        private readonly ITfsMergeConfig _mergeConfig;

        public PotentialMergeRegistrar(string fromBranch, string toBranch)
        {
            _mergeConfig = new TfsMergeConfig(toBranch, fromBranch);
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_mergeConfig).As<ITfsMergeConfig>();
            builder.RegisterType<MergeChangesetRetriever>().As<IChangesetRetriever>();
        }
    }
}
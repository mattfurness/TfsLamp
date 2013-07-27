using Autofac;
using TfsLamp.Infrastructure.ChangeInfo;
using TfsLamp.Infrastructure.Configuration;
using TfsLamp.Infrastructure.Connection;

namespace TfsLamp.Infrastructure.Registration
{
    public class Registrar : Module
    {
        private readonly string _tfsServer;
        private readonly string _tfsUsername;
        private readonly string _tfsPassword;
        private readonly ITfsChangesetRangeConfig _changesetRangeConfig;
        private readonly ITfsMergeConfig _mergeConfig;

        public Registrar(string tfsServer, string tfsUsername, string tfsPassword, string fromBranch, int firstChangesetId, int lastChangesetId)
        {
            _tfsServer = tfsServer;
            _tfsUsername = tfsUsername;
            _tfsPassword = tfsPassword;
            _changesetRangeConfig = new TfsChangesetRangeConfig(fromBranch, firstChangesetId, lastChangesetId);
        }

        public Registrar(string tfsServer, string tfsUsername, string tfsPassword, string fromBranch, string toBranch)
        {
            _tfsServer = tfsServer;
            _tfsUsername = tfsUsername;
            _tfsPassword = tfsPassword;
            _mergeConfig = new TfsMergeConfig(toBranch, fromBranch);
        }

        protected override void Load(ContainerBuilder builder)
        {
            var config = new TfsConnectionConfig(_tfsServer, _tfsUsername, _tfsPassword);
            builder.RegisterInstance(config).As<ITfsConnectionConfig>().SingleInstance();

            if (_changesetRangeConfig != null)
            {
                builder.RegisterInstance(_changesetRangeConfig).As<ITfsChangesetRangeConfig>();
                builder.RegisterType<ChangesetRangeChangesetRetiever>().As<IChangesetRetriever>();
            }
            if (_mergeConfig != null)
            {
                builder.RegisterInstance(_mergeConfig).As<ITfsMergeConfig>();
                builder.RegisterType<MergeChangesetRetriever>().As<IChangesetRetriever>();
            }
            builder.RegisterType<TfsOperations>().As<ITfsOperations>();
            builder.RegisterType<ParentWorkItemPopulator>().As<IWorkItemPopulator>();
        }
    }
}
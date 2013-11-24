using Autofac;
using TfsLamp.Infrastructure.ChangeInfo;
using TfsLamp.Infrastructure.Configuration;
using TfsLamp.Infrastructure.Connection;

namespace TfsLamp.Infrastructure.Registration
{
    public class MergeCAndidatesRegistrar : Module
    {
        private readonly ITfsMergeCandidatesConfig _mergeCandidatesConfig;

        public MergeCAndidatesRegistrar(string fromBranch, string toBranch)
        {
            _mergeCandidatesConfig = new TfsMergeCandidatesConfig(toBranch, fromBranch);
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_mergeCandidatesConfig).As<ITfsMergeCandidatesConfig>();
            builder.RegisterType<MergeChangesetRetriever>().As<IChangesetRetriever>();
        }
    }
}
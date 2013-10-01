using Autofac;
using TfsLamp.Infrastructure.ChangeInfo;
using TfsLamp.Infrastructure.Configuration;
using TfsLamp.Infrastructure.Connection;

namespace TfsLamp.Infrastructure.Registration
{
    public class CommonRegistrar : Module
    {
        private readonly string _tfsServer;
        private readonly string _tfsUsername;
        private readonly string _tfsPassword;

        public CommonRegistrar(string tfsServer, string tfsUsername, string tfsPassword)
        {
            _tfsServer = tfsServer;
            _tfsUsername = tfsUsername;
            _tfsPassword = tfsPassword;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var config = new TfsConnectionConfig(_tfsServer, _tfsUsername, _tfsPassword);
            builder.RegisterInstance(config).As<ITfsConnectionConfig>().SingleInstance();
            builder.RegisterType<TfsOperations>().As<ITfsOperations>();
            builder.RegisterType<ParentWorkItemPopulator>().As<IWorkItemPopulator>();
        }
    }
}
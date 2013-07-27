using Autofac;
using TfsLamp.Infrastructure.ChangeInfo;
using TfsLamp.Infrastructure.Configuration;
using TfsLamp.Infrastructure.Connection;
using TfsLamp.Infrastructure.Tests.Connection;

namespace TfsLamp.Infrastructure.Tests.Registrar
{
    public class Registrar : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FakeTfsOperations>().As<ITfsOperations>().SingleInstance();
            builder.RegisterType<ParentWorkItemPopulator>().As<IWorkItemPopulator>().SingleInstance();
            builder.RegisterInstance(new TfsChangesetRangeConfig("whatever", 1, 7)).As<ITfsChangesetRangeConfig>();
            builder.RegisterType<ChangesetRangeChangesetRetiever>().As<IChangesetRetriever>();
        }
    }
}
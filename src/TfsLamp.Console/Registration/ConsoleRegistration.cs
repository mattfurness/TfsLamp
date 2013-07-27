using Autofac;
using TfsLamp.Console.Configuration;

namespace TfsLamp.Console.Registration
{
    public class ConsoleRegistration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PowerArgsArgumentParser>().As<IArgumentParser>();
        }
    }
}
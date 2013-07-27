using System;
using Autofac;
using TfsLamp.Console.Configuration;
using TfsLamp.Console.Registration;
using TfsLamp.HtmlRendering.Registration;
using TfsLamp.Infrastructure.ChangeInfo;
using TfsLamp.Infrastructure.Registration;
using TfsLamp.Infrastructure.Rendering;

namespace TfsLamp.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ConsoleRegistration());

            var consoleContainer = builder.Build();
            ArgsSpecification parsedArgs = null;

            try
            {
                parsedArgs = consoleContainer.Resolve<IArgumentParser>().ParseArguments(args);

                var appContainer = InitializeApplicationComponents(parsedArgs);

                var changesetRetriever = appContainer.Resolve<IChangesetRetriever>();
                var workItemPopulator = appContainer.Resolve<IWorkItemPopulator>();
                var renderer = appContainer.Resolve<IRenderer>();
                GenerateResults(renderer, workItemPopulator, changesetRetriever, parsedArgs.OutputFile);

            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                Environment.Exit(1);
            }
        }

        public static IContainer InitializeApplicationComponents(ArgsSpecification parsedArgs)
        {
            var builder = new ContainerBuilder();

            if (parsedArgs.IsMerge())
                builder.RegisterModule(new Registrar(parsedArgs.Server, parsedArgs.Username, parsedArgs.Password, parsedArgs.FromBranch, parsedArgs.ToBranch));
            if (parsedArgs.IsChangesetRange())
                builder.RegisterModule(new Registrar(parsedArgs.Server, parsedArgs.Username, parsedArgs.Password, parsedArgs.FromBranch, (int)parsedArgs.FromChangeset, (int)parsedArgs.ToChangeset));

            builder.RegisterModule(new HtmlRenderingRegistrar());

            var container = builder.Build();
            return container;
        }

        public static void GenerateResults(IRenderer renderer, IWorkItemPopulator populator, IChangesetRetriever retriever, string outputFileName)
        {
            var tfsChangesets = retriever.GetChangesets();
            var allRelevantWorkItems = populator.GetAssociatedWorkItemsForChangesets(tfsChangesets);

            renderer.Render(allRelevantWorkItems, outputFileName);
        }
    }
}

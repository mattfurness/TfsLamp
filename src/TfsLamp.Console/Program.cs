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

            try
            {
                var argumentParser = consoleContainer.Resolve<IArgumentParser>();
                var modeArgs = argumentParser.ParseArguments(typeof(ModeArgs), args.GetOnlyTheModeArgument()) as ModeArgs;
                var mode = ArgsTypeForModeProvider.GetArgsFormMode(modeArgs.Mode);
                var parsedArgs = argumentParser.ParseArguments(mode, args.StripTheModeArgument()) as StandardArgs;

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

        private static IContainer InitializeApplicationComponents(StandardArgs parsedArgs)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new CommonRegistrar(parsedArgs.Server, parsedArgs.Username, parsedArgs.Password));

            builder.RegisterModule(parsedArgs.GetRegistrar());

            builder.RegisterModule(new HtmlRenderingRegistrar());

            var container = builder.Build();
            return container;
        }

        private static void GenerateResults(IRenderer renderer, IWorkItemPopulator populator, IChangesetRetriever retriever, string outputFileName)
        {
            var tfsChangesets = retriever.GetChangesets();
            var allRelevantWorkItems = populator.GetAssociatedWorkItemsForChangesets(tfsChangesets);

            renderer.Render(allRelevantWorkItems, outputFileName);
        }
    }
}

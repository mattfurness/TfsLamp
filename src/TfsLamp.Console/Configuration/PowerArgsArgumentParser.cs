using PowerArgs;
using TfsLamp.Infrastructure.Configuration;

namespace TfsLamp.Console.Configuration
{
    public class PowerArgsArgumentParser : IArgumentParser
    {
        public ArgsSpecification ParseArguments(string[] args)
        {
            try
            {
                var argsSpecification = Args.Parse<ArgsSpecification>(args);

                argsSpecification.Validate();

                return argsSpecification;
            }
            catch (ArgException ex)
            {
                throw new ConfigurationException(ArgUsage.GetUsage<ArgsSpecification>("TsfMergeInformation"), ex);
            }
        }
    }
}
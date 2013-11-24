using System;
using System.Linq;
using System.Reflection;
using PowerArgs;
using TfsLamp.Infrastructure.Configuration;

namespace TfsLamp.Console.Configuration
{
    public class PowerArgsArgumentParser : IArgumentParser
    {
        public object ParseArguments(Type argsType, string[] args)
        {
            try
            {
                var argsSpecification = Args.Parse(argsType, args);

                return argsSpecification;
            }
            catch (ArgException ex)
            {
                var getUsageMethod = typeof (ArgUsage).GetMember("GetUsage").First() as MethodInfo;
                getUsageMethod = getUsageMethod.MakeGenericMethod(new[] { argsType });
                var usageInstructions = getUsageMethod.Invoke(null, new object[]{"TFSLamp usage", null}) as string;
                throw new ConfigurationException(usageInstructions, ex);
            }
        }
    }
}
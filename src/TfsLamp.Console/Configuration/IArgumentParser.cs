using System;

namespace TfsLamp.Console.Configuration
{
    public interface IArgumentParser
    {
        object ParseArguments(Type argsType, string[] args);
    }
}
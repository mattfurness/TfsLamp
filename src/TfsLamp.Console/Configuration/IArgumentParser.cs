namespace TfsLamp.Console.Configuration
{
    public interface IArgumentParser
    {
        ArgsSpecification ParseArguments(string[] args);
    }
}
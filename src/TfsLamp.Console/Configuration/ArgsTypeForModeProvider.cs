using System;
using System.Collections.Generic;
using System.Linq;

namespace TfsLamp.Console.Configuration
{
    public static class ArgumentsExtensions
    {
        private const int ModeArgCount = 2;

        public static string[] GetOnlyTheModeArgument(this string[] args)
        {
            return args.Take(ModeArgCount).ToArray();
        }

        public static string[] StripTheModeArgument(this string[] args)
        {
            return args.Skip(ModeArgCount).ToArray();
        }
    }

    public static class ArgsTypeForModeProvider
    {
        private static IDictionary<Mode, Type> _argTypes;

        static ArgsTypeForModeProvider()
        {
            _argTypes = new Dictionary<Mode, Type>
            {
                {
                    Mode.ChangesetRange, typeof (ChangesetRangeArgs)
                },
                {
                    Mode.MergeCandidates, typeof (MergeCandidatesArgs)
                },
                {
                    Mode.MergedChangeset, typeof (MergedChangesetsArgs)
                },
            };
        }


        public static Type GetArgsFormMode(Mode mode)
        {
            return _argTypes[mode];
        }
    }
}
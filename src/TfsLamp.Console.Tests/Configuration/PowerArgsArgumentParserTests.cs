using System;
using Machine.Fakes;
using Machine.Specifications;
using TfsLamp.Console.Configuration;
using TfsLamp.Infrastructure.Configuration;

namespace TfsLamp.Console.Tests.Configuration
{
    public static class TestInput
    {
        public const string ModeKey = "-mode";
        public const string ChangesetRangeModeValue = "ChangesetRange";
        public const string MergeCandidatesModeValue = "MergeCandidates";
        public const string MergedChangesetModeValue = "MergedChangeset";

        public const string ServerKey = "-server";
        public const string UserKey = "-username";
        public const string PasswordKey = "-password";
        public const string OutputFileKey = "-outputfile";

        public const string ChangesetKey = "-changeset";

        public const string FromChangesetKey = "-fromChangeset";
        public const string ToChangesetKey = "-toChangeset";
        public const string BranchKey = "-branch";

        public const string FromBranchKey = "-fromBranch";
        public const string ToBranchKey = "-toBranch";

        public const string ServerValue = "ServerValue";
        public const string UserValue = "UserValue";
        public const string PasswordValue = "PasswordValue";
        public const string OutputFileValue = "OutputFileValue";

        public const int FromChangesetValue = 0;
        public const int ToChangesetValue = 1;

        public const string FromBranchValue = "FromBranchValue";
        public const string ToBranchValue = "ToBranchValue";
    }

    public class When_parsing_changeset_range_arguments : WithSubject<PowerArgsArgumentParser>
    {
        private static ChangesetRangeArgs _args;

        Because of = () => 
        {
            _args = Subject.ParseArguments(typeof(ChangesetRangeArgs), new[]
            {
                TestInput.ServerKey, TestInput.ServerValue, TestInput.UserKey, TestInput.UserValue, TestInput.PasswordKey, TestInput.PasswordValue, 
                TestInput.BranchKey, TestInput.FromBranchValue, 
                TestInput.FromChangesetKey, TestInput.FromChangesetValue.ToString(), TestInput.ToChangesetKey, TestInput.ToChangesetValue.ToString(), 
                TestInput.OutputFileKey, TestInput.OutputFileValue
            }) as ChangesetRangeArgs; 
        };

        It should_generate_populated_server = () =>
            {
                _args.Server.ShouldEqual(TestInput.ServerValue);
            };

        It should_generate_populated_user = () =>
            {
                _args.Username.ShouldEqual(TestInput.UserValue);
            };

        It should_generate_populated_password = () =>
            {
                _args.Password.ShouldEqual(TestInput.PasswordValue);
            };

        It should_generate_populated_from_changeset = () =>
            {
                _args.FromChangeset.ShouldEqual(TestInput.FromChangesetValue);
            };

        It should_generate_populated_branch = () =>
            {
                _args.Branch.ShouldEqual(TestInput.FromBranchValue);
            };

        It should_generate_populated_to_changeset = () =>
            {
                _args.ToChangeset.ShouldEqual(TestInput.ToChangesetValue);
            };

        It should_generate_populated_output_file = () =>
            {
                _args.OutputFile.ShouldEqual(TestInput.OutputFileValue);
            };
    }

    public class When_parsing_merged_changesets_arguments : WithSubject<PowerArgsArgumentParser>
    {
        private static MergedChangesetsArgs _args;

        Because of = () => 
        {
            _args = Subject.ParseArguments(typeof(MergedChangesetsArgs), new[]
            {
                TestInput.ServerKey, TestInput.ServerValue, TestInput.UserKey, TestInput.UserValue, TestInput.PasswordKey, TestInput.PasswordValue, 
                TestInput.FromBranchKey, TestInput.FromBranchValue, 
                TestInput.ToBranchKey, TestInput.ToBranchValue, 
                TestInput.ChangesetKey, TestInput.FromChangesetValue.ToString(), 
                TestInput.OutputFileKey, TestInput.OutputFileValue
            }) as MergedChangesetsArgs; 
        };

        It should_generate_populated_server = () =>
            {
                _args.Server.ShouldEqual(TestInput.ServerValue);
            };

        It should_generate_populated_user = () =>
            {
                _args.Username.ShouldEqual(TestInput.UserValue);
            };

        It should_generate_populated_password = () =>
            {
                _args.Password.ShouldEqual(TestInput.PasswordValue);
            };

        It should_generate_populated_from_branch = () =>
            {
                _args.FromBranch.ShouldEqual(TestInput.FromBranchValue);
            };

        It should_generate_populated_to_branch = () =>
            {
                _args.ToBranch.ShouldEqual(TestInput.ToBranchValue);
            };

        It should_generate_populated_changeset = () =>
            {
                _args.Changeset.ShouldEqual(TestInput.FromChangesetValue);
            };

        It should_generate_populated_output_file = () =>
            {
                _args.OutputFile.ShouldEqual(TestInput.OutputFileValue);
            };
    }

    public class When_parsing_merge_candidate_arguments : WithSubject<PowerArgsArgumentParser>
    {
        private static MergeCandidatesArgs _args;

        Because of = () => 
        { 
            _args = Subject.ParseArguments(typeof(MergeCandidatesArgs), new[]
            {
                TestInput.ServerKey, TestInput.ServerValue, TestInput.UserKey, TestInput.UserValue, TestInput.PasswordKey, TestInput.PasswordValue, 
                TestInput.FromBranchKey, TestInput.FromBranchValue, 
                TestInput.ToBranchKey, TestInput.ToBranchValue, 
                TestInput.OutputFileKey, TestInput.OutputFileValue
            }) as MergeCandidatesArgs; 
        };

        It should_generate_populated_server = () =>
            {
                _args.Server.ShouldEqual(TestInput.ServerValue);
            };

        It should_generate_populated_user = () =>
            {
                _args.Username.ShouldEqual(TestInput.UserValue);
            };

        It should_generate_populated_password = () =>
            {
                _args.Password.ShouldEqual(TestInput.PasswordValue);
            };

        It should_generate_populated_from_branch = () =>
            {
                _args.FromBranch.ShouldEqual(TestInput.FromBranchValue);
            };

        It should_generate_populated_to_branch = () =>
            {
                _args.ToBranch.ShouldEqual(TestInput.ToBranchValue);
            };

        It should_generate_populated_output_file = () =>
            {
                _args.OutputFile.ShouldEqual(TestInput.OutputFileValue);
            };
    }
    
    public class When_no_arguments_are_present_for_merge_candidates : WithSubject<PowerArgsArgumentParser>
    {
        private static Exception _resultingException;

        Because of = () => 
        { 
            _resultingException = Catch.Exception(() => Subject.ParseArguments(typeof(MergeCandidatesArgs), new[]
            {
                string.Empty
            })); 
        };

        It should_be_configuration_exception = () =>
            {
                _resultingException.ShouldBeOfType<ConfigurationException>();
            };
    }
    
    public class When_no_arguments_are_present_for_merged_changesets : WithSubject<PowerArgsArgumentParser>
    {
        private static Exception _resultingException;

        Because of = () => 
        { 
            _resultingException = Catch.Exception(() => Subject.ParseArguments(typeof(MergedChangesetsArgs), new[]
            {
                string.Empty
            })); 
        };

        It should_be_configuration_exception = () =>
            {
                _resultingException.ShouldBeOfType<ConfigurationException>();
            };
    }
    
    public class When_no_arguments_are_present_for_changeset_range : WithSubject<PowerArgsArgumentParser>
    {
        private static Exception _resultingException;

        Because of = () => 
        { 
            _resultingException = Catch.Exception(() => Subject.ParseArguments(typeof(ChangesetRangeArgs), new[]
            {
                string.Empty
            })); 
        };

        It should_be_configuration_exception = () =>
            {
                _resultingException.ShouldBeOfType<ConfigurationException>();
            };
    }
    
    public class When_a_ChangesetRange_mode_is_supplied : WithSubject<PowerArgsArgumentParser>
    {
        private static ModeArgs _args;

        Because of = () =>
        {
            _args = Subject.ParseArguments(typeof(ModeArgs), new[]
            {
                TestInput.ModeKey, TestInput.ChangesetRangeModeValue
            }) as ModeArgs;
        };

        It should_generate_populated_server = () =>
        {
            _args.Mode.ShouldEqual(Mode.ChangesetRange);
        };
    }

    public class When_a_MergeCandidates_mode_is_supplied : WithSubject<PowerArgsArgumentParser>
    {
        private static ModeArgs _args;

        Because of = () =>
        {
            _args = Subject.ParseArguments(typeof(ModeArgs), new[]
            {
                TestInput.ModeKey, TestInput.MergeCandidatesModeValue
            }) as ModeArgs;
        };

        It should_generate_populated_server = () =>
        {
            _args.Mode.ShouldEqual(Mode.MergeCandidates);
        };
    }

    public class When_a_MergedChangeset_mode_is_supplied : WithSubject<PowerArgsArgumentParser>
    {
        private static ModeArgs _args;

        Because of = () =>
        {
            _args = Subject.ParseArguments(typeof(ModeArgs), new[]
            {
                TestInput.ModeKey, TestInput.MergedChangesetModeValue
            }) as ModeArgs;
        };

        It should_generate_populated_server = () =>
        {
            _args.Mode.ShouldEqual(Mode.MergedChangeset);
        };
    }

    public class When_an_invalid_mode_is_set : WithSubject<PowerArgsArgumentParser>
    {
        private static Exception _resultingException;

        Because of = () =>
        {
            _resultingException = Catch.Exception(() => Subject.ParseArguments(typeof(ModeArgs), new[]
            {
                TestInput.ModeKey, "Anything"
            }));
        };

        It should_be_configuration_exception = () =>
        {
            _resultingException.ShouldBeOfType<ConfigurationException>();
        };
    }
}

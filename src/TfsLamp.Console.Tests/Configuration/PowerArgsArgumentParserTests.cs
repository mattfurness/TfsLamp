using System;
using Machine.Fakes;
using Machine.Specifications;
using TfsLamp.Console.Configuration;
using TfsLamp.Infrastructure.Configuration;

namespace TfsLamp.Console.Tests.Configuration
{
    public static class TestInput
    {
        public const string ServerKey = "-server";
        public const string UserKey = "-username";
        public const string PasswordKey = "-password";
        public const string OutputFileKey = "-outputfile";

        public const string FromChangesetKey = "-fromChangeset";
        public const string ToChangesetKey = "-toChangeset";

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

    public class When_connection_and_changeset_arguments_are_present : WithSubject<PowerArgsArgumentParser>
    {
        private static ArgsSpecification _args;

        Because of = () => 
        { 
            _args = Subject.ParseArguments(new[]
            {
                TestInput.ServerKey, TestInput.ServerValue, TestInput.UserKey, TestInput.UserValue, TestInput.PasswordKey, TestInput.PasswordValue, 
                TestInput.FromBranchKey, TestInput.FromBranchValue, 
                TestInput.FromChangesetKey, TestInput.FromChangesetValue.ToString(), TestInput.ToChangesetKey, TestInput.ToChangesetValue.ToString(), 
                TestInput.OutputFileKey, TestInput.OutputFileValue
            }); 
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

        It should_generate_populated_to_changeset = () =>
            {
                _args.ToChangeset.ShouldEqual(TestInput.ToChangesetValue);
            };

        It should_generate_populated_output_file = () =>
            {
                _args.OutputFile.ShouldEqual(TestInput.OutputFileValue);
            };
    }

    public class When_connection_and_branch_arguments_are_present : WithSubject<PowerArgsArgumentParser>
    {
        private static ArgsSpecification _args;

        Because of = () => 
        { 
            _args = Subject.ParseArguments(new[]
            {
                TestInput.ServerKey, TestInput.ServerValue, TestInput.UserKey, TestInput.UserValue, TestInput.PasswordKey, TestInput.PasswordValue, TestInput.FromBranchKey, TestInput.FromBranchValue, TestInput.ToBranchKey, TestInput.ToBranchValue, TestInput.OutputFileKey, TestInput.OutputFileValue
            }); 
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

    public class When_neither_branch_not_changeset_arguments_are_present : WithSubject<PowerArgsArgumentParser>
    {
        private static Exception _resultingException;

        Because of = () => 
        { 
            _resultingException = Catch.Exception(() => Subject.ParseArguments(new[]
            {
                TestInput.ServerKey, TestInput.ServerValue, TestInput.UserKey, TestInput.UserValue, TestInput.PasswordKey, TestInput.PasswordValue, 
                TestInput.OutputFileKey, TestInput.OutputFileValue
            })); 
        };

        It should_be_configuration_exception = () =>
            {
                _resultingException.ShouldBeOfType<ConfigurationException>();
            };
    }

    public class When_both_branch_not_changeset_arguments_are_present : WithSubject<PowerArgsArgumentParser>
    {
        private static Exception _resultingException;

        Because of = () => 
        { 
            _resultingException = Catch.Exception(() => Subject.ParseArguments(new[]
            {
                TestInput.ServerKey, TestInput.ServerValue, TestInput.UserKey, TestInput.UserValue, TestInput.PasswordKey, TestInput.PasswordValue, 
                TestInput.FromBranchKey, TestInput.FromBranchValue, TestInput.ToBranchKey, TestInput.ToBranchValue, 
                TestInput.FromChangesetKey, TestInput.FromChangesetValue.ToString(), TestInput.ToChangesetKey, TestInput.ToChangesetValue.ToString(), 
                TestInput.OutputFileKey, TestInput.OutputFileValue
            })); 
        };

        It should_be_configuration_exception = () =>
            {
                _resultingException.ShouldBeOfType<ConfigurationException>();
            };
    }

    public class When_no_arguments_are_present : WithSubject<PowerArgsArgumentParser>
    {
        private static Exception _resultingException;

        Because of = () => 
        { 
            _resultingException = Catch.Exception(() => Subject.ParseArguments(new[]
            {
                string.Empty
            })); 
        };

        It should_be_configuration_exception = () =>
            {
                _resultingException.ShouldBeOfType<ConfigurationException>();
            };
    }
}

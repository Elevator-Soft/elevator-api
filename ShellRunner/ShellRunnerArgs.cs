using System;

namespace Shell
{
    public class ShellRunnerArgs
    {
        public string WorkingDirectory { get; }

        public string Command { get; }

        public string[] Arguments { get; }

        public ShellRunnerArgs(string workingDirectory, string command)
        {
            WorkingDirectory = workingDirectory ?? throw new ArgumentNullException(nameof(workingDirectory));
            Command = command ?? throw new ArgumentNullException(nameof(command));
            Arguments = new string[0];
        }

        public ShellRunnerArgs(string workingDirectory, string command, params string[] arguments)
        {
            WorkingDirectory = workingDirectory ?? throw new ArgumentNullException(nameof(workingDirectory));
            Command = command ?? throw new ArgumentNullException(nameof(command));
            Arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
        }

        public override string ToString()
        {
            return $"{nameof(WorkingDirectory)}: '{WorkingDirectory}'\n" +
                   $"{nameof(Command)}: '{Command}'\n" +
                   $"{nameof(Arguments)}: '{string.Join(';', Arguments)}'";
        }
    }
}
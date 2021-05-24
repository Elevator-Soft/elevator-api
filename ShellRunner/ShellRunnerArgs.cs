using System;

namespace Shell
{
    public class ShellRunnerArgs
    {
        public string WorkingDirectory { get; }

        public string Command { get; }

        public string[] Arguments { get; }

        public bool UseShellExecute { get; }

        public ShellRunnerArgs(string workingDirectory, string command, bool useShellExecute, params string[] arguments)
        {
            WorkingDirectory = workingDirectory ?? throw new ArgumentNullException(nameof(workingDirectory));
            Command = command ?? throw new ArgumentNullException(nameof(command));
            UseShellExecute = useShellExecute;
            Arguments = arguments ?? Array.Empty<string>();
        }

        public override string ToString()
        {
            return $"{nameof(WorkingDirectory)}: '{WorkingDirectory}'\n" +
                   $"{nameof(Command)}: '{Command}'\n" +
                   $"{nameof(Arguments)}: '{string.Join(';', Arguments)}'";
        }
    }
}
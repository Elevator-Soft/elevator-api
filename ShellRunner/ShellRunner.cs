using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Shell.Exceptions;

namespace Shell
{
    public class ShellRunner
    {
        private readonly ShellRunnerArgs args;

        public StreamReader OutputStream
        {
            get
            {
                if (outputStream == null)
                    throw new ProcessNotStartedYetException("Output stream does not exist yet");

                return outputStream;
            }
        }

        public StreamReader ErrorStream
        {
            get
            {
                if (errorStream == null)
                    throw new ProcessNotStartedYetException("Error stream does not exist yet");

                return errorStream;
            }
        }

        private StreamReader outputStream;
        private StreamReader errorStream;

        public ShellRunner(ShellRunnerArgs args)
        {
            this.args = args ?? throw new ArgumentNullException(nameof(args));
        }

        public async Task<int> RunAsync(CancellationToken cancellationToken = default)
        {
            var processStartInfo = new ProcessStartInfo(args.Command, string.Join(' ', args.Arguments))
            {
                WorkingDirectory = args.WorkingDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            var process = Process.Start(processStartInfo);

            if (process == null)
                throw new CannotStartProcessException($"Shell runner args:\n{args}'");

            outputStream = process.StandardOutput;
            errorStream = process.StandardError;

            await process.WaitForExitAsync(cancellationToken);

            return process.ExitCode;
        }

        public async Task<int> RunWithRetryAsync(int retryCount, CancellationToken cancellationToken = default)
        {
            var exitCode = await RunAsync(cancellationToken);
            if (exitCode == 0)
                return exitCode;

            for (var i = 1; i < retryCount; i++)
            {
                exitCode = await RunAsync(cancellationToken);
                if (exitCode == 0)
                    return exitCode;
            }

            return exitCode;
        }
    }
}

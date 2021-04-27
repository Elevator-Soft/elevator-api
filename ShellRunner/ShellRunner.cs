using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Shell
{
    public class ShellRunner
    {
        private readonly ShellRunnerArgs args;

        public ShellRunner(ShellRunnerArgs args)
        {
            this.args = args ?? throw new ArgumentNullException(nameof(args));
        }

        public async Task<OperationResult<ShellProcess>> RunAsync(CancellationToken cancellationToken = default)
        {
            var processStartInfo = new ProcessStartInfo(args.Command, string.Join(' ', args.Arguments))
            {
                WorkingDirectory = args.WorkingDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            var process = Process.Start(processStartInfo);

            if (process == null)
                return OperationResult<ShellProcess>.InternalServerError($"Process not started. Args: {this.args}");

            await process.WaitForExitAsync(cancellationToken);

            if (process.ExitCode != 0)
                return OperationResult<ShellProcess>.InternalServerError($"Exit code = {process.ExitCode}\n" + 
                                                                         $"Output: {await process.StandardOutput.ReadToEndAsync()}\n" + 
                                                                         $"Error: {await process.StandardError.ReadToEndAsync()}");

            return OperationResult<ShellProcess>.Ok(new ShellProcess(process.StandardOutput, process.StandardError));
        }

        public async Task<OperationResult<ShellProcess>> RunWithRetryAsync(int retryCount, CancellationToken cancellationToken = default)
        {
            var processResult = await RunAsync(cancellationToken);
            if (processResult.IsSuccessful)
                return processResult;

            for (var i = 1; i < retryCount; i++)
            {
                processResult = await RunAsync(cancellationToken);
                if (processResult.IsSuccessful)
                    return processResult;
            }

            return processResult;
        }
    }
}

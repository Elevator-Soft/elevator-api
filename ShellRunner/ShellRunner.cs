using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Shell
{
    public class ShellRunner
    {
        public async Task<OperationResult<ShellProcess>> RunAsync(ShellRunnerArgs args, CancellationToken cancellationToken = default)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));

            var processStartInfo = new ProcessStartInfo(args.Command, string.Join(' ', args.Arguments))
            {
                WorkingDirectory = args.WorkingDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            try
            {
                var process = Process.Start(processStartInfo);

                if (process == null)
                    return OperationResult<ShellProcess>.Failed($"Process not started. Args: {args}");

                await process.WaitForExitAsync(cancellationToken);

                if (process.ExitCode != 0)
                    return OperationResult<ShellProcess>.Failed($"Exit code = {process.ExitCode}\n" +
                                                                $"Output: {await process.StandardOutput.ReadToEndAsync()}\n" +
                                                                $"Error: {await process.StandardError.ReadToEndAsync()}");

                return OperationResult<ShellProcess>.Success(new ShellProcess(process.StandardOutput, process.StandardError));
            }
            catch (Exception e)
            {
                return OperationResult<ShellProcess>.Failed($"Starting process gone wrong. Args: {args}. Exception: {e.Message}");
            }
        }

        public async Task<OperationResult<ShellProcess>> RunWithRetryAsync(ShellRunnerArgs args, int retryCount, CancellationToken cancellationToken = default)
        {
            var processResult = await RunAsync(args, cancellationToken);
            if (processResult.IsSuccessful)
                return processResult;

            for (var i = 1; i < retryCount; i++)
            {
                processResult = await RunAsync(args, cancellationToken);
                if (processResult.IsSuccessful)
                    return processResult;
            }

            return processResult;
        }
    }
}

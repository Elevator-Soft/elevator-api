using System;
using System.Threading.Tasks;
using Git.Exceptions;
using Microsoft.Extensions.Logging;
using Shell;

namespace Git
{
    public class GitRepository
    {
        public string Directory { get; }

        private readonly ILogger<GitRepository> logger;

        public GitRepository(string directory, ILogger<GitRepository> logger)
        {
            Directory = directory ?? throw new ArgumentNullException(nameof(directory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task CheckoutAsync(string branch)
        {
            using var scope = logger.BeginScope("Checkout");

            var shellRunnerArgs = new ShellRunnerArgs(Directory, "git", "checkout", branch);
            var shellRunner = new ShellRunner(shellRunnerArgs);

            int exitCode;
            try
            {
                exitCode = await shellRunner.RunAsync();
            }
            catch (Exception e)
            {
                throw new CheckoutException(e);
            }

            if (exitCode != 0)
                throw new CheckoutException(await shellRunner.ErrorStream.ReadToEndAsync());

            logger.LogInformation(await shellRunner.ErrorStream.ReadToEndAsync());
        }

        public async Task<string> GetCommitHashAsync()
        {
            using var scope = logger.BeginScope("Get commit hash");

            var shellRunnerArgs = new ShellRunnerArgs(Directory, "git", "rev-parse", "HEAD");
            var shellRunner = new ShellRunner(shellRunnerArgs);

            int exitCode;
            try
            {
                exitCode = await shellRunner.RunAsync();
            }
            catch (Exception e)
            {
                throw new GetCommitHashException(e);
            }

            if (exitCode != 0)
                throw new GetCommitHashException(await shellRunner.ErrorStream.ReadToEndAsync());

            var commitHash = await shellRunner.OutputStream.ReadToEndAsync();

            return commitHash;
        }
    }
}

using System;
using System.Threading.Tasks;
using Common;
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

        public async Task<VoidOperationResult> CheckoutAsync(string branch)
        {
            using var scope = logger.BeginScope("Checkout");

            var shellRunnerArgs = new ShellRunnerArgs(Directory, "git", "checkout", branch);
            var shellRunner = new ShellRunner(shellRunnerArgs);

            var checkoutProcessResult = await shellRunner.RunAsync();

            if (!checkoutProcessResult.IsSuccessful)
                return VoidOperationResult.InternalServerError(checkoutProcessResult.Error);

            logger.LogInformation(await checkoutProcessResult.Value.Error.ReadToEndAsync());

            return VoidOperationResult.Ok();
        }

        public async Task<OperationResult<string>> GetCommitHashAsync()
        {
            using var scope = logger.BeginScope("Get commit hash");

            var shellRunnerArgs = new ShellRunnerArgs(Directory, "git", "rev-parse", "HEAD");
            var shellRunner = new ShellRunner(shellRunnerArgs);

            var getCommitHashProcessResult = await shellRunner.RunAsync();

            if (!getCommitHashProcessResult.IsSuccessful)
                return OperationResult<string>.Failed(getCommitHashProcessResult.Error);

            logger.LogInformation(await getCommitHashProcessResult.Value.Error.ReadToEndAsync());

            return OperationResult<string>.Success(await getCommitHashProcessResult.Value.Output.ReadToEndAsync());
        }
    }
}

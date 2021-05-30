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

        private readonly ShellRunner shellRunner;
        private readonly ILogger<GitRepository> logger;

        public GitRepository(ShellRunner shellRunner, string directory, ILogger<GitRepository> logger)
        {
            Directory = directory ?? throw new ArgumentNullException(nameof(directory));
            this.shellRunner = shellRunner ?? throw new ArgumentNullException(nameof(shellRunner));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<VoidOperationResult> CheckoutAsync(string branch)
        {
            using var scope = logger.BeginScope("Checkout");

            var shellRunnerArgs = new ShellRunnerArgs(Directory, "git", false, "checkout", branch);

            var checkoutProcessResult = await shellRunner.RunAsync(shellRunnerArgs);

            if (!checkoutProcessResult.IsSuccessful)
                return VoidOperationResult.Failed(checkoutProcessResult.Error);

            logger.LogInformation(await checkoutProcessResult.Value.Error.ReadToEndAsync());

            return VoidOperationResult.Success();
        }

        public async Task<OperationResult<string>> GetCommitHashAsync()
        {
            using var scope = logger.BeginScope("Get commit hash");

            var shellRunnerArgs = new ShellRunnerArgs(Directory, "git", false, "rev-parse", "HEAD");

            var getCommitHashProcessResult = await shellRunner.RunAsync(shellRunnerArgs);

            if (!getCommitHashProcessResult.IsSuccessful)
                return OperationResult<string>.Failed(getCommitHashProcessResult.Error);

            logger.LogInformation(await getCommitHashProcessResult.Value.Error.ReadToEndAsync());

            return OperationResult<string>.Success(await getCommitHashProcessResult.Value.Output.ReadToEndAsync());
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Microsoft.Extensions.Logging;
using Shell;

namespace Git
{
    public class GitProject
    {
        private readonly GitProjectInformation gitProjectInformation;
        private readonly ILogger<GitProject> logger;
        private readonly ILoggerFactory loggerFactory;

        public GitProject(GitProjectInformation gitProjectInformation, ILoggerFactory loggerFactory)
        {
            this.gitProjectInformation = gitProjectInformation ?? throw new ArgumentNullException(nameof(gitProjectInformation));
            this.loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            this.logger = loggerFactory.CreateLogger<GitProject>();
        }

        public async Task<OperationResult<GitRepository>> CloneAsync(CancellationToken cancellationToken = default)
        {
            using var scope = logger.BeginScope("Clone");

            var arguments = new List<string>
            {
                "clone",
                GetUrlWithAccessToken(),
                gitProjectInformation.TargetDirectory,
                "--verbose"
            };

            if (gitProjectInformation.CloneRecursive)
                arguments.Add("--recursive");

            var shellRunner = new ShellRunner(new ShellRunnerArgs(gitProjectInformation.WorkingDirectory, "git", arguments.ToArray()));

            var cloneProcessResult = await shellRunner.RunAsync(cancellationToken);
            
            if (!cloneProcessResult.IsSuccessful)
                return OperationResult<GitRepository>.InternalServerError(cloneProcessResult.Error);

            logger.LogInformation(await cloneProcessResult.Value.Error.ReadToEndAsync());
            logger.LogInformation(await cloneProcessResult.Value.Output.ReadToEndAsync());

            var repositoryDirectory = Path.Combine(gitProjectInformation.WorkingDirectory,
                gitProjectInformation.TargetDirectory);
            
            return OperationResult<GitRepository>.Ok(new GitRepository(repositoryDirectory, loggerFactory.CreateLogger<GitRepository>()));
        }

        private string GetUrlWithAccessToken()
        {
            var projectUrlString = gitProjectInformation.ProjectUrl.ToString();
            return $"{projectUrlString[..8]}{gitProjectInformation.AccessToken}@{projectUrlString[8..]}";
        }
    }
}

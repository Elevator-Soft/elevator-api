using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Git.Exceptions;
using Microsoft.Extensions.Logging;
using Shell;

namespace Git
{
    public class GitProject
    {
        private readonly GitProjectInformation gitProjectInformation;
        private readonly ILogger<GitProject> logger;
        private readonly ILoggerFactory loggerFactory;

        public GitProject(GitProjectInformation gitProjectInformation, ILogger<GitProject> logger, ILoggerFactory loggerFactory)
        {
            this.gitProjectInformation = gitProjectInformation ?? throw new ArgumentNullException(nameof(gitProjectInformation));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task<GitRepository> CloneAsync(CancellationToken cancellationToken = default)
        {
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

            try
            {
                var exitCode = await shellRunner.RunAsync(cancellationToken);
                if (exitCode != 0)
                    throw new CloneException(await shellRunner.ErrorStream.ReadToEndAsync());
            }
            catch (Exception e)
            {
                throw new CloneException(e);
            }

            logger.LogInformation(await shellRunner.ErrorStream.ReadToEndAsync());
            logger.LogInformation(await shellRunner.OutputStream.ReadToEndAsync());

            var repositoryDirectory = Path.Combine(gitProjectInformation.WorkingDirectory,
                gitProjectInformation.TargetDirectory);
            
            return new GitRepository(repositoryDirectory, loggerFactory.CreateLogger<GitRepository>());
        }

        private string GetUrlWithAccessToken()
        {
            var projectUrlString = gitProjectInformation.ProjectUrl.ToString();
            return $"{projectUrlString[..8]}{gitProjectInformation.AccessToken}@{projectUrlString[8..]}";
        }
    }
}

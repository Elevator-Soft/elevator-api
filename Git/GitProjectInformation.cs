using System;

namespace Git
{
    public class GitProjectInformation
    {
        public Uri ProjectUrl { get; }

        public string AccessToken { get; }

        public string WorkingDirectory { get; }

        public string TargetDirectory { get; }

        public bool CloneRecursive { get; }

        public GitProjectInformation(Uri projectUrl, string accessToken, string workingDirectory, string targetDirectory, bool cloneRecursive = false)
        {
            ProjectUrl = projectUrl ?? throw new ArgumentNullException(nameof(projectUrl));
            AccessToken = accessToken ?? throw new ArgumentNullException(nameof(AccessToken));
            WorkingDirectory = workingDirectory ?? throw new ArgumentNullException(nameof(workingDirectory));
            TargetDirectory = targetDirectory ?? throw new ArgumentNullException(nameof(targetDirectory));
            CloneRecursive = cloneRecursive;
        }
    }
}

using System;

namespace Git.Exceptions
{
    public class GetCommitHashException: Exception
    {
        public GetCommitHashException(string message) : base(message)
        { }

        public GetCommitHashException(Exception innerException) : base(innerException.ToString())
        { }
    }
}

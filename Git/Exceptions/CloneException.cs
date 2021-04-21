using System;

namespace Git.Exceptions
{
    public class CloneException: Exception
    {
        public CloneException(string message) : base(message)
        { }

        public CloneException(Exception innerException) : base(innerException.ToString())
        { }
    }
}

using System;

namespace Shell.Exceptions
{
    public class ProcessNotStartedYetException: Exception
    {
        public ProcessNotStartedYetException(string message) : base(message)
        { }
    }
}

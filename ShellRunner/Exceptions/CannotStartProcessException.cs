using System;

namespace Shell.Exceptions
{
    public class CannotStartProcessException: Exception
    {
        public CannotStartProcessException(string message) : base(message)
        { }
    }
}

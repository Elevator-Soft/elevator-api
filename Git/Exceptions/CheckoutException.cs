using System;

namespace Git.Exceptions
{
    public class CheckoutException: Exception
    {
        public CheckoutException(string message) : base(message)
        { }

        public CheckoutException(Exception innerException): base(innerException.ToString())
        { }
    }
}

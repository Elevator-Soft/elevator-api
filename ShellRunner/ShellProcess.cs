using System;
using System.IO;

namespace Shell
{
    public class ShellProcess
    {
        public StreamReader Output { get; }

        public StreamReader Error { get; }

        public ShellProcess(StreamReader output, StreamReader error)
        {
            Output = output ?? throw new ArgumentNullException(nameof(output));
            Error = error ?? throw new ArgumentNullException(nameof(error));
        }
    }
}
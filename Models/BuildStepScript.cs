namespace Models
{
    public class BuildStepScript
    {
        public string Command { get; set; }
        public string Arguments { get; set; }

        public override string ToString()
        {
            return $"{nameof(Command)}: {Command}\n" +
                   $"{nameof(Arguments)} : {Arguments}";
        }
    }
}

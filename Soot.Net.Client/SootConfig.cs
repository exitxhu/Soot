namespace Soot.Net.Client
{
    public class SootConfig
    {
        public SootConfig() { }
        public SootConfig(string hostAddress)
        {
            HostAddress = hostAddress;
        }
        public string HostAddress { get; set; }
        public Uri HostUri => new(HostAddress);

    }
}
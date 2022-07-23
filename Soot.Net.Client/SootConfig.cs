namespace Soot.Net.Client
{
    public class SootConfig
    {
        public string HostAddress { get; set; }
        public Uri HostUri => new Uri(HostAddress);

    }
}
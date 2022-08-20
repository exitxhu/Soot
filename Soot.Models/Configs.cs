using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soot.Common.Models
{
    public class SootKafkaConfig
    {
        public string Server { get; set; }
        public string[] Topics { get; set; }
        public string GroupId { get; set; }
    }
    public class SootRestConfig
    {
        public SootRestConfig() { }
        public SootRestConfig(string hostAddress)
        {
            HostAddress = hostAddress;
        }
        public string HostAddress { get; set; }
        public Uri HostUri => new(HostAddress);
    }
}

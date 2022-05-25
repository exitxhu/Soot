using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soot.Domain.Shared
{
    public class ResultDto
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soot.Domain.Base
{
    public abstract class Root<T> where T : new()
    {
        public abstract T SetTrueId(object id);
    }
}

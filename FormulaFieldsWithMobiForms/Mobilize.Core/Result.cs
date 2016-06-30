using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Core
{
    public class Result<T, P>
    {
        public T Status { get; set; }
        public P Data { get; set; }
        public string Message { get; set; }
    }
}

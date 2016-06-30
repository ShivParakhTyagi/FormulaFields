using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.GetLayoutService
{
    public class GetLayoutRequest
    {
        public List<string> ListOfForms { get; set; }
        public string Format { get; set; }
    }
}

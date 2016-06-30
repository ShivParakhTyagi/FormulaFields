using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobilize.Contract.Base;

namespace Mobilize.Contract.CreateMobiFormService
{
    public class CreateMobiFormResponse: ServiceResponse
    {
        public bool FormCreated { get; set; }
        public string MobiForm { get; set; }
        public bool FieldCreationFailed { get; set; }
        public string FieldNames { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobilize.Contract.Base;

namespace Mobilize.Contract.DeleteMobiFormService
{
    public class DeleteMobiFormResponse: ServiceResponse
    {
        public bool FormDeleted { get; set; }
    }
}

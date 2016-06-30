using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobilize.Contract.Base;

namespace Mobilize.Contract.CreateOrganizationService
{
    public class CreateOrganizationResponse : ServiceResponse
    {
        
        public string AdminLoginName { get; set; }
        public string AdminLoginPassword { get; set; }
    }
}

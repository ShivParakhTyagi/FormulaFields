using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.CreateOrganizationService
{
    public class CreateOrganizationRequest
    {
        public string OrganizationName { get; set; }
        public string SqlConnectionString { get; set; }

        public string CountryCode { get; set; }
        public string AdminEmail { get; set; }
        public string AdminPhone { get; set; }
    }
}

using System.Collections.Generic;
using Mobilize.Contract.Base;

namespace Mobilize.Contract.GetSubMobiFormsService
{
    public class GetSubMobiFormsResponse : ServiceResponse
    {
        public List<string> SubForms { get; set; }  
    }
}
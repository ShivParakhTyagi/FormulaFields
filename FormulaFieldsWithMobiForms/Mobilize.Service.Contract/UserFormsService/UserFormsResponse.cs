using System;
using System.Collections.Generic;
using Mobilize.Contract.Base;

namespace Mobilize.Contract.AssignedFormsService
{
    public class UserFormsResponse : ServiceResponse
    {
        public List<Form> Forms { get; set; }
    }
    /// <summary>
    /// this has the information about form name, its layout and the layouts last modified time.
    ///  in case layout is not available, layout will be set as null.
    /// </summary>
    public class Form
    {
        public string FormName { get; set; }
        public string FormDescription { get; set; }
        public string Layout { get; set; }
        public DateTime Modified { get; set; }
        public string FormType { get; set; }
        //public List<string> SubFormsList { get; set; }
    }
}
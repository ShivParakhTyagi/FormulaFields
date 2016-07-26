using System;
using System.Collections.Generic;

namespace EntityFrameworkToSql_ConsoleApp.Model
{
    public class SupportRequests
    {
        public int Id {get;set;}
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime PreferredTime { get; set; }
        public string ModeOfSupport { get; set; }
        public string Description { get; set; }
        public ICollection<FileAttachments> Attachments { get; set; }
        public virtual SupportType TypeOfSupport { get; set; }
        public virtual User User { get; set; }
    }
}
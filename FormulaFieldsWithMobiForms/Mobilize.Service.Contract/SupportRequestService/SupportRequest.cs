using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.SupportRequestService
{
    public class SupportRequest
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ModeOfSupport { get; set; }
        public int SupportType { get; set; }
        public string Description { get; set; }
        public DateTime PreferredTime { get; set; }
    }
    public class Attachment
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public long Size { get; set; }
        public byte[] Content { get; set; }
    }
}

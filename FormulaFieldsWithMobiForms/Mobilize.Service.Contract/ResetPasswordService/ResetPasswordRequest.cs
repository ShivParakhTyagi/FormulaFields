using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.ResetPasswordService
{
    public class ResetPasswordRequest
    {
        public string UserId { get; set; }
        public bool SendMail { get; set; }
        public string MailsTo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.DownloadFileService
{
    public class DownloadFileRequest
    {
        public string ListName { get; set; }
        public string InternalName { get; set; }
        public string ItemId { get; set; }
    }
}

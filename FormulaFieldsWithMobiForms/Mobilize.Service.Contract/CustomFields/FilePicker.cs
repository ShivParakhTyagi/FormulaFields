using Mobilize.Contract.MobilizeDataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.CustomFields
{
    public class FilePicker : FieldsInfo
    {
        public FilePicker()
        {
            MobilizeType = "FilePicker";
            TypeStructure = "FileName,FileLength,FileContent";
        }
        public string FileTypeFilter { get; set; }
        public string FileTypeFilterErrorMessage { get; set; }
        public string RestrictedFileLocation { get; set; }
        public string DefaultFileLocation { get; set; }
        public string MaxFileSize { get; set; }
        public string MaxFileSizeErrorMessage { get; set; }
        public string UploadButtonName { get; set; } = "Upload";
        public string DownloadButtonName { get; set; } = "Download";
        public string RemoveButtonName { get; set; } = "Remove";
        public string LookupField { get; set; }
        public string LookupList { get; set; }
    }
}

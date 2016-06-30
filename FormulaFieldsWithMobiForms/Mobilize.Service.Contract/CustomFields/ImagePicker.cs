using Mobilize.Contract.MobilizeDataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.CustomFields
{
    public class ImagePicker : FieldsInfo
    {
        public ImagePicker()
        {
            MobilizeType = "ImagePicker";
            TypeStructure = "ImageName,ImageSize,ImageContent";
        }
        public string FileTypeFilter { get; set; }
        public string FileTypeFilterErrorMessage { get; set; }
        public string RestrictedFileLocation { get; set; }
        public string DefaultFileLocation { get; set; }
        public string MaxFileSize { get; set; }
        public string MaxFileSizeErrorMessage { get; set; }
        public string CameraButtonName { get; set; } = "Open Camera";
        public string MediaButtonName { get; set; } = "Open Media Picker";
        public string DownloadButtonName { get; set; } = "Download";
        public string RemoveButtonName { get; set; } = "Remove";
        public string LookupField { get; set; }
        public string LookupList { get; set; }
    }
}

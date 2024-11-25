using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomScout.Models.AdminSide
{
    public class UploadedFile
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public bool IsCertified { get; set; }
        public DateTime UploadDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mediafierWebApp.RequestModel
{
    public class DocumentRequest
    {
        public string DocName { get; set; }
        public string DocContentType { get; set; }
        public int? DocSize { get; set; }
        public int? DocCreatedBy { get; set; }
        public DateTime? DocCreatedAt { get; set; }
        public int? DocFolderId { get; set; }
        public bool? DocIsDeleted { get; set; }
    }
}

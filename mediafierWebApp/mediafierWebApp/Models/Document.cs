using System;
using System.Collections.Generic;

namespace mediafierWebApp.Models
{
    public partial class Document
    {
        public int DocId { get; set; }
        public string DocName { get; set; }
        public string DocContentType { get; set; }
        public int? DocSize { get; set; }
        public int? DocCreatedBy { get; set; }
        public DateTime? DocCreatedAt { get; set; }
        public int? DocFolderId { get; set; }
        public bool? DocIsDeleted { get; set; }

        public Users DocCreatedByNavigation { get; set; }
        public Folders DocFolder { get; set; }
    }
}

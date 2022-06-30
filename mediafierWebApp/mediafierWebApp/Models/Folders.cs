using System;
using System.Collections.Generic;

namespace mediafierWebApp.Models
{
    public partial class Folders
    {
        public Folders()
        {
            Document = new HashSet<Document>();
        }

        public int FoldersId { get; set; }
        public string FoldersName { get; set; }
        public int? FoldersCreatedBy { get; set; }
        public DateTime? FoldersCreatedAt { get; set; }
        public bool? FoldersIsdeleted { get; set; }

        public Users FoldersCreatedByNavigation { get; set; }
        public ICollection<Document> Document { get; set; }
    }
}

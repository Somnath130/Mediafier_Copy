using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mediafierWebApp.RequestModel
{
    public class FoldersRequest
    {
        public string FoldersName { get; set; }
        public int? FoldersCreatedBy { get; set; }
        public DateTime? FoldersCreatedAt { get; set; }
        public bool? FoldersIsdeleted { get; set; }
    }
}

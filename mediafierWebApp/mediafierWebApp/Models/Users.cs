using System;
using System.Collections.Generic;

namespace mediafierWebApp.Models
{
    public partial class Users
    {
        public Users()
        {
            Document = new HashSet<Document>();
            Folders = new HashSet<Folders>();
        }

        public int UsersId { get; set; }
        public string UsersUsername { get; set; }
        public string UsersPassword { get; set; }
        public DateTime UsersCreatedAt { get; set; }

        public ICollection<Document> Document { get; set; }
        public ICollection<Folders> Folders { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mediafierWebApp.RequestModel
{
    public class UserRequest
    {
        public string UsersUsername { get; set; }
        public string UsersPassword { get; set; }
        public DateTime UsersCreatedAt { get; set; }
    }
}

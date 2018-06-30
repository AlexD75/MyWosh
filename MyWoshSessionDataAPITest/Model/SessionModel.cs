using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyWoshSessionDataAPITest.Model
{
    public class SessionData
    {
        private string _sessionID = Guid.NewGuid().ToString();
        private DateTime _createdOn = DateTime.Now;
        private DateTime _lastAccess = DateTime.Now;

        public string SessionId {
            get { return _sessionID; }
            internal set { _sessionID = value; }
        }

        [Required]
        public string ClientId { get; set; }

        [Required]
        public string Owner { get; set; }

        public DateTime CreatedOn { get { return _createdOn; } }

        public DateTime LastAccess
        {
            get { return _lastAccess; }
            internal set { _lastAccess = value; }
        }
    }
}

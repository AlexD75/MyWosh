using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWoshSessionDataAPITest.Model;

namespace MyWoshSessionDataAPITest.Controllers
{
    [Produces("application/json")]
    [Route("api/data")]
    public class SessionController : Controller
    {
        private static Dictionary<string, SessionData> _datastore = new Dictionary<string, SessionData>();

        static SessionController()
        {
            SessionData dummy = new SessionData { ClientId = "Client1", Owner = "Alex" };
            _datastore.Add(dummy.SessionId, dummy);
        }

        /// <summary>
        /// Get all active sessions
        /// </summary>
        /// <returns>Array of active Session</returns>
        /// <remarks>List of current active sessions or empty array if no session are present</remarks>
        [HttpGet("Sessions")]
        public IEnumerable<SessionData> Get()
        {
            return _datastore.Values.ToArray();
        }

        /// <summary>
        /// Get an active session
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Session/{id}")]
        public SessionData Get(string id)
        {
            return _datastore[id];
        }

        /// <summary>
        /// Create a new session
        /// </summary>
        /// <param name="value"></param>
        [HttpPost("Session")]
        public SessionData Create([FromBody]SessionData value)
        {
            SessionData newSession = new SessionData();
            newSession.SessionId = value.SessionId;
            newSession.ClientId = value.ClientId;
            newSession.Owner = value.Owner;

            _datastore.Add(value.SessionId, newSession);

            return newSession;
        }

        /// <summary>
        /// Update an existing session
        /// </summary>
        /// <param name="id"></param>
        [HttpPut("Session/{id}")]
        public SessionData Renew(string id)
        {
            var value = _datastore[id];

            if (value == null)
                return null;

            value.LastAccess = DateTime.Now;

            return value;
        }

        /// <summary>
        /// Delete an active session
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("Session/{id}")]
        public bool Expire(string id)
        {
            return _datastore.Remove(id);

        }
    }
}

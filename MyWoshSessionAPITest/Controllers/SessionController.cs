using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyWoshSessionAPITest;
using MyWoshSessionAPITest.API.Data;
using MyWoshSessionAPITest.API.Data.Models;

namespace MyWoshSessionAPITest.Controllers
{
    [Produces("application/json")]
    [Route("api/Session")]
    public class SessionController : Controller
    {
        IConfiguration Configuration { get; }

        public SessionController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        static MyWoshSessionDataServiceAPI getClientDataAPI(IConfiguration configuration)
        {
            var client = new MyWoshSessionDataServiceAPI(new Uri(configuration["SessionDataServiceAPIUrl"]));
            return client;
        }

        /// <summary>
        /// Get all active sessions
        /// </summary>
        /// <returns>Array of active Session</returns>
        /// <remarks>List of current active sessions or emplty array if no session are present</remarks>
        /// <response code="200">Returns the array of active sessions</response>
        /// <response code="404">If there isn't any active session</response>            
        [HttpGet("ActiveSessions")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SessionData>))]
        [ProducesResponseType(404)]
        [Authorize]
        public async Task<IActionResult> ActiveSessions()
        {
            using (var client = getClientDataAPI(Configuration))
            {
                var result = await client.ApiDataSessionsGetAsync();

                if (result == null)
                    return NotFound();
                else
                    return Ok(result);
            }
        }

        /// <summary>
        /// Get an active session
        /// </summary>
        /// <param name="SessionId">Id of session to retrive</param>
        /// <returns>A SessionData object</returns>
        /// <response code="200">Returns correspondig sessions</response>
        /// <response code="400">If SessionId is null, empty or not a valid Guid</response>            
        /// <response code="404">If no session is found</response>            
        [HttpGet("{SessionId}")]
        [ProducesResponseType(200, Type = typeof(SessionData))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Authorize]
        public async Task<IActionResult> Get(string SessionId)
        {
            Guid SessionGuid;

            if (string.IsNullOrWhiteSpace(SessionId))
                return BadRequest();

            if(!Guid.TryParse(SessionId, out SessionGuid))
                return BadRequest();

            using (var client = getClientDataAPI(Configuration))
            {
                var result = await client.ApiDataSessionByIdGetAsync(SessionId);

                if (result == null)
                    return NotFound();
                else
                    return Ok(result);
            }
        }

        /// <summary>
        /// Create a new session
        /// </summary>
        /// <param name="value">A SessionData object with ClientId and Owner property set</param>
        /// <returns>The newly created session</returns>
        /// <response code="200">Returns newly created session</response>
        /// <response code="400">If value is null or not a valid (eg. without owner info)</response>            
        [HttpPost("Create")]
        [ProducesResponseType(200, Type = typeof(SessionData))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody]SessionData value)
        {
            if (string.IsNullOrWhiteSpace(value.Owner))
                return BadRequest();

            if (string.IsNullOrWhiteSpace(value.ClientId))
                return BadRequest();

            using (var client = getClientDataAPI(Configuration))
            {
                var result = await client.ApiDataSessionPostAsync(value);

                return Ok(result);
            }
        }

        /// <summary>
        /// Renew an active session
        /// </summary>
        /// <param name="SessionId">Id of session to renew</param>
        /// <returns>The renewed SessionData object</returns>
        /// <response code="200">Returns renewed sessions object</response>
        /// <response code="400">If SessionId is null, empty or not a valid Guid</response>            
        /// <response code="404">If no active session is found</response>            
        [HttpPut("Renew/{SessionId}")]
        [ProducesResponseType(200, Type = typeof(SessionData))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Renew(string SessionId)
        {
            Guid SessionGuid;

            if (string.IsNullOrWhiteSpace(SessionId))
                return BadRequest();

            if (!Guid.TryParse(SessionId, out SessionGuid))
                return BadRequest();

            using (var client = getClientDataAPI(Configuration))
            {
                var result = await client.ApiDataSessionByIdPutAsync(SessionId);

                if (result == null)
                    return NotFound();
                else
                    return Ok(result);
            }
        }

        /// <summary>
        /// Expire an active session
        /// </summary>
        /// <param name="SessionId">Id of session to expire</param>
        /// <response code="200">Session successfully expired</response>
        /// <response code="400">If SessionId is null, empty or not a valid Guid</response>            
        /// <response code="404">If no active session is found</response>            
        [HttpDelete("{SessionId}")]
        [ProducesResponseType(200, Type = typeof(SessionData))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async void Delete(string SessionId)
        {
            Guid SessionGuid;

            if (string.IsNullOrWhiteSpace(SessionId))
                BadRequest();

            if (!Guid.TryParse(SessionId, out SessionGuid))
                BadRequest();

            using (var client = getClientDataAPI(Configuration))
            {
                var result = await client.ApiDataSessionByIdDeleteAsync(SessionId);

                if (result.HasValue && result.Value)
                    Ok();
                else
                    NotFound();
            }
        }
    }
}

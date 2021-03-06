// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace MyWoshSessionAPITest.API.Data.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Linq;

    public partial class SessionData
    {
        /// <summary>
        /// Initializes a new instance of the SessionData class.
        /// </summary>
        public SessionData()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the SessionData class.
        /// </summary>
        public SessionData(string clientId, string owner, string sessionId = default(string), System.DateTime? createdOn = default(System.DateTime?), System.DateTime? lastAccess = default(System.DateTime?))
        {
            SessionId = sessionId;
            ClientId = clientId;
            Owner = owner;
            CreatedOn = createdOn;
            LastAccess = lastAccess;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "sessionId")]
        public string SessionId { get; private set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "clientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "owner")]
        public string Owner { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdOn")]
        public System.DateTime? CreatedOn { get; private set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "lastAccess")]
        public System.DateTime? LastAccess { get; private set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (ClientId == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "ClientId");
            }
            if (Owner == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Owner");
            }
        }
    }
}

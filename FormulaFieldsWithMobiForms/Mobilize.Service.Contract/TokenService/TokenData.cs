using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Mobilize.Contract.TokenService
{
    /// <summary>
    ///TokenData class is used when user logs in for authentication purpose
    /// </summary>
    public class TokenData
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }

        [JsonProperty("userName")]
        public string Username { get; set; }

        [JsonProperty("orgName")]
        public string OrgName { get; set; }

        [JsonProperty(".issued")]
        public string Issued { get; set; }

        [JsonProperty(".expires")]
        public string Expired { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("organizationId")]
        public string OrgId { get; set; }
    }
}

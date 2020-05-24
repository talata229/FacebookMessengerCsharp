using Newtonsoft.Json;

namespace Facebook.DAL.Requests.Simsimi
{
    public class SimsimiRequest
    {
        [JsonProperty("utext")]
        public string Utext { get; set; }
        [JsonProperty("lang")]
        public string Lang { get; set; }
    }
}

using Facebook.DAL.Requests.Simsimi;
using Newtonsoft.Json;

namespace Facebook.DAL.Responses.Simsimi
{
    public class SimsimiResponse
    {
        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("statusMessage")]
        public string StatusMessage { get; set; }

        [JsonProperty("request")]
        public SimsimiRequest Request { get; set; }

        [JsonProperty("atext")]
        public string Atext { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }
    }
}

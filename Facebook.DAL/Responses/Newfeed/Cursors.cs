using Newtonsoft.Json;

namespace Facebook.DAL.Responses.Newfeed
{
    public class Cursors
    {
        [JsonProperty("after")]
        public string After { get; set; }

        [JsonProperty("before")]
        public string Before { get; set; }
    }
}
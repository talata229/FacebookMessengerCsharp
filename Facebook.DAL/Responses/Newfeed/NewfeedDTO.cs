using Newtonsoft.Json;

namespace Facebook.DAL.Responses.Newfeed
{
    public class NewfeedDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_time")]
        public string CreatedTime { get; set; }

        [JsonProperty("from")]
        public From From { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }
}
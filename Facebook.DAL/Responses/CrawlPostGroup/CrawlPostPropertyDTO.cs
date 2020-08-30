using Newtonsoft.Json;

namespace Facebook.DAL.Responses.CrawlPostGroup
{
    public class CrawlPostPropertyDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
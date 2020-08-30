using Newtonsoft.Json;

namespace Facebook.DAL.Responses.CrawlPostGroup
{
    public class CrawlPostSharesDTO
    {
        [JsonProperty("count")]
        public long? Count { get; set; }
    }
}
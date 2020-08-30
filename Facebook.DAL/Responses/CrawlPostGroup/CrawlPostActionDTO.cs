using System;
using Newtonsoft.Json;

namespace Facebook.DAL.Responses.CrawlPostGroup
{
    public class CrawlPostActionDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }
    }
}
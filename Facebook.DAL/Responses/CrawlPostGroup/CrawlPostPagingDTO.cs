using System;
using Newtonsoft.Json;

namespace Facebook.DAL.Responses.CrawlPostGroup
{
    public class CrawlPostPagingDTO
    {
        [JsonProperty("previous")]
        public string Previous { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }
    }
}
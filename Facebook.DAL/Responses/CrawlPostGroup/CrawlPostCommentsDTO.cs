using System.Collections.Generic;
using Newtonsoft.Json;

namespace Facebook.DAL.Responses.CrawlPostGroup
{
    public class CrawlPostCommentsDTO
    {
        [JsonProperty("data")]
        public List<CommentsDatumDTO> Data { get; set; }

        [JsonProperty("paging")]
        public CommentsPagingDTO Paging { get; set; }
    }
}
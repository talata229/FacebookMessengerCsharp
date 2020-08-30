using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Facebook.DAL.Responses.CrawlPostGroup
{
   public class CrawlPostGroupRoot
    {
        [JsonProperty("data")]
        public List<CrawlPostDTO> Data { get; set; }

        [JsonProperty("paging")]
        public CrawlPostPagingDTO Paging { get; set; }
    }
}

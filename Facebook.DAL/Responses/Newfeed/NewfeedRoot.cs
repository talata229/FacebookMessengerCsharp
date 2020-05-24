using Newtonsoft.Json;
using System.Collections.Generic;

namespace Facebook.DAL.Responses.Newfeed
{
    public class NewfeedRoot
    {
        [JsonProperty("data")]
        public List<NewfeedDTO> Data { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }
    }
}

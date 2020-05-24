using Newtonsoft.Json;

namespace Facebook.DAL.Responses.Newfeed
{
    public class Paging
    {
        [JsonProperty("cursors")]
        public Cursors Cursors { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }
    }
}
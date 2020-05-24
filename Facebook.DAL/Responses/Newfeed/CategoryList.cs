using Newtonsoft.Json;

namespace Facebook.DAL.Responses.Newfeed
{
    public class CategoryList
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
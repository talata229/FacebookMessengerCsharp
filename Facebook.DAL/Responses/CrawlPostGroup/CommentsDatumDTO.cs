using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Facebook.DAL.Responses.CrawlPostGroup
{
    public class CommentsDatumDTO
    {
        [JsonProperty("created_time")]
        public string CreatedTimeFromApi { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("can_remove")]
        public bool? CanRemove { get; set; }

        [JsonProperty("like_count")]
        public long? LikeCount { get; set; }

        [JsonProperty("user_likes")]
        public bool? UserLikes { get; set; }

        /// <summary>
        /// Id bên Facebook
        /// </summary>
        [JsonProperty("id")]
        public string Fb_Id { get; set; }
    }
}
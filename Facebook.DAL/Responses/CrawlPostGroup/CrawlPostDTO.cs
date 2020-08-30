using System;
using System.Collections.Generic;
using Facebook.DAL.Helpers;
using Newtonsoft.Json;

namespace Facebook.DAL.Responses.CrawlPostGroup
{
    public class CrawlPostDTO
    {
        /// <summary>
        /// Id của Facebook
        /// </summary>
        [JsonProperty("id")]
        public string Fb_Id { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("picture", NullValueHandling = NullValueHandling.Ignore)]
        public string Picture { get; set; }

        [JsonProperty("link", NullValueHandling = NullValueHandling.Ignore)]
        public string Link { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("icon", NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; set; }

        [JsonProperty("actions")]
        public List<CrawlPostActionDTO> Actions { get; set; }

        [JsonProperty("privacy")]
        public CrawlPostPrivacyDTO Privacy { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("status_type", NullValueHandling = NullValueHandling.Ignore)]
        public string StatusType { get; set; }

        [JsonProperty("object_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ObjectId { get; set; }

        [JsonProperty("created_time")]
        public string Fb_CreatedTime { get; set; }

        public DateTime? CreatedTime
        {
            get
            {
                return DateTimeHelperDAL.ConvertStringFromApiFacebookToDateTime(Fb_CreatedTime);
            }
        }
        [JsonProperty("updated_time")]
        public string Fb_UpdatedTime { get; set; }
        public DateTime? UpdatedTime
        {
            get
            {
                return DateTimeHelperDAL.ConvertStringFromApiFacebookToDateTime(Fb_UpdatedTime);
            }
        }

        [JsonProperty("shares", NullValueHandling = NullValueHandling.Ignore)]
        public CrawlPostSharesDTO Fb_Shares { get; set; }

        public long? Shares
        {
            get
            {
                return Fb_Shares?.Count;
            }
        }

        [JsonProperty("is_hidden")]
        public bool IsHidden { get; set; }

        [JsonProperty("is_expired")]
        public bool IsExpired { get; set; }

        [JsonProperty("comments")]
        public CrawlPostCommentsDTO Comments { get; set; }

        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public string Source { get; set; }

        [JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
        public List<CrawlPostPropertyDTO> Properties { get; set; }

        [JsonProperty("story", NullValueHandling = NullValueHandling.Ignore)]
        public string Story { get; set; }
    }
}
﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMessengerCsharp.Client.API
{
    public enum QuickReplyType
    {
        location,
        text,
        user_phone_number,
        user_email
    }

    /// <summary>
    /// Represents a quick reply 
    /// </summary>
    public class FB_QuickReply
    {
        /// Payload of the quick reply
        public JToken payload { get; set; }
        /// External payload for responses
        public JToken external_payload { get; set; }
        /// Additional data
        public JToken data { get; set; }
        /// Whether it's a response for a quick reply
        public bool is_response { get; set; }
        /// Type of the quick reply
        public QuickReplyType? _type { get; set; }

        /// <summary>
        /// Represents a quick reply 
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="payload"></param>
        /// <param name="external_payload"></param>
        /// <param name="data"></param>
        /// <param name="is_response"></param>
        public FB_QuickReply(QuickReplyType? _type = null, JToken payload = null, JToken external_payload = null, JToken data = null, bool is_response = false)
        {
            this._type = _type;
            this.payload = payload;
            this.external_payload = external_payload;
            this.data = data;
            this.is_response = is_response;
        }

        public static FB_QuickReply graphql_to_quick_reply(JToken q, bool is_response = false)
        {
            FB_QuickReply rtn = null;
            var data = new Dictionary<string, object>();
            var _type = q.get("content_type")?.Value<string>()?.ToLower();
            data["payload"] = q.get("payload");
            data["data"] = q.get("data");
            if (q["image_url"] != null && _type != QuickReplyType.location.ToString())
                data["image_url"] = q.get("image_url")?.Value<string>();
            data["is_response"] = is_response;
            if (_type == QuickReplyType.text.ToString())
            {
                if (q.get("title") != null)
                    data["title"] = q.get("title");
                rtn = new FB_QuickReplyText(
                    (JToken)data.GetValueOrDefault("payload"),
                    (JToken)data.GetValueOrDefault("external_payload"),
                    (JToken)data.GetValueOrDefault("data"),
                    (bool)data.GetValueOrDefault("is_response"),
                    (string)data.GetValueOrDefault("title"),
                    (string)data.GetValueOrDefault("image_url"));
            }
            else if (_type == QuickReplyType.location.ToString())
                rtn = new FB_QuickReplyLocation(
                    (JToken)data.GetValueOrDefault("payload"),
                    (JToken)data.GetValueOrDefault("external_payload"),
                    (JToken)data.GetValueOrDefault("data"),
                    (bool)data.GetValueOrDefault("is_response"));
            else if (_type == QuickReplyType.user_phone_number.ToString())
                rtn = new FB_QuickReplyPhoneNumber(
                    (JToken)data.GetValueOrDefault("payload"),
                    (JToken)data.GetValueOrDefault("external_payload"),
                    (JToken)data.GetValueOrDefault("data"),
                    (bool)data.GetValueOrDefault("is_response"),
                    (string)data.GetValueOrDefault("image_url"));
            else if (_type == QuickReplyType.user_email.ToString())
                rtn = new FB_QuickReplyEmail(
                    (JToken)data.GetValueOrDefault("payload"),
                    (JToken)data.GetValueOrDefault("external_payload"),
                    (JToken)data.GetValueOrDefault("data"),
                    (bool)data.GetValueOrDefault("is_response"),
                    (string)data.GetValueOrDefault("image_url"));
            return rtn;
        }
    }

    /// <summary>
    /// Represents a text quick reply 
    /// </summary>
    public class FB_QuickReplyText : FB_QuickReply
    {
        /// Title of the quick reply
        public string title { get; set; }
        /// URL of the quick reply image (optional)
        public string image_url { get; set; }

        /// <summary>
        /// Represents a text quick reply 
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="external_payload"></param>
        /// <param name="data"></param>
        /// <param name="is_response"></param>
        /// <param name="title"></param>
        /// <param name="image_url"></param>
        public FB_QuickReplyText(JToken payload = null, JToken external_payload = null, JToken data = null, bool is_response = false, string title = null, string image_url = null)
            : base(QuickReplyType.text, payload, external_payload, data, is_response)
        {
            this.title = title;
            this.image_url = image_url;
        }
    }

    /// <summary>
    /// Represents a location quick reply (Doesn't work on mobile)
    /// </summary>
    public class FB_QuickReplyLocation : FB_QuickReply
    {
        /// <summary>
        /// Represents a location quick reply (Doesn't work on mobile)
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="external_payload"></param>
        /// <param name="data"></param>
        /// <param name="is_response"></param>
        public FB_QuickReplyLocation(JToken payload = null, JToken external_payload = null, JToken data = null, bool is_response = false)
            : base(QuickReplyType.location, payload, external_payload, data, is_response)
        {
            this.is_response = false;
        }
    }

    /// <summary>
    /// Represents a location quick reply (Doesn't work on mobile)
    /// </summary>
    public class FB_QuickReplyPhoneNumber : FB_QuickReply
    {
        /// URL of the quick reply image (optional)
        public string image_url { get; set; }

        /// <summary>
        /// Represents a location quick reply (Doesn't work on mobile)
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="external_payload"></param>
        /// <param name="data"></param>
        /// <param name="is_response"></param>
        /// <param name="image_url"></param>
        public FB_QuickReplyPhoneNumber(JToken payload = null, JToken external_payload = null, JToken data = null, bool is_response = false, string image_url = null)
            : base(QuickReplyType.user_phone_number, payload, external_payload, data, is_response)
        {
            this.image_url = image_url;
        }
    }

    /// <summary>
    /// Represents a location quick reply (Doesn't work on mobile)
    /// </summary>
    public class FB_QuickReplyEmail : FB_QuickReply
    {
        /// URL of the quick reply image (optional)
        public string image_url { get; set; }

        /// <summary>
        /// Represents a location quick reply (Doesn't work on mobile)
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="external_payload"></param>
        /// <param name="data"></param>
        /// <param name="is_response"></param>
        /// <param name="image_url"></param>
        public FB_QuickReplyEmail(JToken payload = null, JToken external_payload = null, JToken data = null, bool is_response = false, string image_url = null)
            : base(QuickReplyType.user_email, payload, external_payload, data, is_response)
        {
            this.image_url = image_url;
        }
    }
}

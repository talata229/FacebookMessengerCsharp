﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMessengerCsharp.Client.API
{
    /// <summary>
    /// Represents a poll
    /// </summary>
    public class FB_Poll
    {
        /// Title of the poll
        public string title { get; set; }
        /// List of :class:`PollOption`, can be fetched with :func:`fbchat-sharp.Client.fetchPollOptions`
        public List<FB_PollOption> options { get; set; }
        /// Options count
        public int options_count { get; set; }
        /// ID of the poll
        public string uid { get; set; }

        /// <summary>
        /// Represents a poll
        /// </summary>
        /// <param name="title"></param>
        /// <param name="options"></param>
        /// <param name="options_count"></param>
        /// <param name="uid"></param>
        public FB_Poll(string title = null, List<FB_PollOption> options = null, int options_count = 0, string uid = null)
        {
            this.title = title;
            this.options = options ?? new List<FB_PollOption>();
            this.options_count = options_count;
            this.uid = uid;
        }

        public static FB_Poll _from_graphql(JToken data)
        {
            return new FB_Poll(
                uid: data.get("id")?.Value<string>(),
                title: data.get("title")?.Value<string>() ?? data.get("text")?.Value<string>(),
                options: data.get("options")?.Select((m) => FB_PollOption._from_graphql(m))?.ToList(),
                options_count: data.get("total_count")?.Value<int>() ?? 0);
        }
    }

    /// <summary>
    /// Represents a poll option
    /// </summary>
    public class FB_PollOption
    {
        /// Text of the poll option
        public string text { get; set; }
        /// Whether vote when creating or client voted
        public bool vote { get; set; }
        /// ID of the users who voted for this poll option
        public List<string> voters { get; set; }
        /// Votes count
        public int votes_count { get; set; }
        /// ID of the poll option
        public string uid { get; set; }

        /// <summary>
        /// Represents a poll option
        /// </summary>
        /// <param name="text"></param>
        /// <param name="vote"></param>
        /// <param name="voters"></param>
        /// <param name="votes_count"></param>
        /// <param name="uid"></param>
        public FB_PollOption(string text = null, bool vote = false, List<string> voters = null, int votes_count = 0, string uid = null)
        {
            this.text = text;
            this.vote = vote;
            this.voters = voters ?? new List<string>();
            this.votes_count = votes_count;
            this.uid = uid;
        }

        public static FB_PollOption _from_graphql(JToken data)
        {
            bool vote = false;
            if (data.get("viewer_has_voted") == null)
                vote = false;
            else if (data.get("viewer_has_voted")?.Type == JTokenType.Boolean)
                vote = data.get("viewer_has_voted")?.Value<bool>() ?? false;
            else
                vote = data.get("viewer_has_voted")?.Value<string>() == "true";

            return new FB_PollOption(
                uid: data.get("id")?.Value<string>(),
                text: data.get("text")?.Value<string>(),
                vote: vote,
                voters: (data.get("voters")?.Type == JTokenType.Object ?
                    data.get("voters")?.get("edges").Select((m) => m.get("node")?.get("id")?.Value<string>()) : data.get("voters")?.ToObject<List<string>>()).ToList(),
                votes_count: (data.get("voters")?.Type == JTokenType.Object ?
                    data.get("voters")?.get("count")?.Value<int>() : data.get("total_count")?.Value<int>()) ?? 0
            );
        }
    }
}

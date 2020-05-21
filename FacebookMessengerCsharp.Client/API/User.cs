﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMessengerCsharp.Client.API
{
    /// <summary>
    /// 
    /// </summary>
    public class GENDER
    {
        /// <summary>
        /// For standard requests
        /// </summary>
        public static readonly Dictionary<int, string> standard_GENDERS = new Dictionary<int, string>() {
            {0, "unknown"},
            {1, "female_singular"},
            {2, "male_singular"},
            {3, "female_singular_guess"},
            {4, "male_singular_guess"},
            {5, "mixed"},
            {6, "neuter_singular"},
            {7, "unknown_singular"},
            {8, "female_plural"},
            {9, "male_plural"},
            {10, "neuter_plural"},
            {11, "unknown_plural" },
        };

        /// <summary>
        /// For graphql requests
        /// </summary>
        public static readonly Dictionary<string, string> graphql_GENDERS = new Dictionary<string, string>()
        {
            { "UNKNOWN", "unknown" },
            { "FEMALE", "female_singular" },
            { "MALE", "male_singular" },
            { "NEUTER", "neuter_singular" }
        };
    }

    /// <summary>
    /// Used to specify whether the user is typing or has stopped typing
    /// </summary>
    public enum TypingStatus
    {
        STOPPED = 0,
        TYPING = 1,
    }

    /// <summary>
    /// Represents a Facebook user. Inherits `Thread`
    /// </summary>
    public class FB_User : FB_Thread
    {
        /// The profile url
        public string url { get; set; }
        /// The users first name
        public string first_name { get; set; }
        /// The users last name
        public string last_name { get; set; }
        /// Whether the user and the client are friends
        public bool is_friend { get; set; }
        /// The user"s gender
        public string gender { get; set; }
        /// From 0 to 1. How close the client is to the user
        public float affinity { get; set; }
        /// The user"s nickname
        public string nickname { get; set; }
        /// The clients nickname, as seen by the user
        public string own_nickname { get; set; }
        /// A :class:`ThreadColor`. The message color
        public string color { get; set; }
        /// The default emoji
        public JToken emoji { get; set; }

        /// <summary>
        /// Represents a Facebook user. Inherits `Thread`
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="photo"></param>
        /// <param name="name"></param>
        /// <param name="message_count"></param>
        /// <param name="last_message_timestamp"></param>
        /// <param name="plan"></param>
        /// <param name="url"></param>
        /// <param name="first_name"></param>
        /// <param name="last_name"></param>
        /// <param name="is_friend"></param>
        /// <param name="gender"></param>
        /// <param name="affinity"></param>
        /// <param name="nickname"></param>
        /// <param name="own_nickname"></param>
        /// <param name="color"></param>
        /// <param name="emoji"></param>
        public FB_User(string uid, string photo = null, string name = null, int message_count = 0, string last_message_timestamp = null, FB_Plan plan = null, string url = null, string first_name = null, string last_name = null, bool is_friend = false, string gender = null, float affinity = 0, string nickname = null, string own_nickname = null, string color = null, JToken emoji = null) :
            base(ThreadType.USER, uid, photo, name, message_count: message_count, last_message_timestamp: last_message_timestamp, plan: plan)
        {
            this.url = url;
            this.first_name = first_name;
            this.last_name = last_name ?? (first_name != null ? name?.Replace(first_name, "")?.Trim() : null);
            this.is_friend = is_friend;
            this.gender = gender;
            this.affinity = affinity;
            this.nickname = nickname;
            this.own_nickname = own_nickname;
            this.color = color ?? ThreadColor.MESSENGER_BLUE;
            this.emoji = emoji;
        }

        /// <summary>
        /// Represents a Facebook user. Inherits `Thread`
        /// </summary>
        /// <param name="uid"></param>
        public FB_User(string uid) :
            base(ThreadType.USER, uid)
        {

        }

        /// <returns>Pretty string representation of the thread</returns>
        public override string ToString()
        {
            return this.__unicode__();
        }

        private string __unicode__()
        {
            return string.Format("<{0} {1} {2} ({3})>", this.type.ToString(), this.first_name, this.last_name, this.uid);
        }

        public static FB_User _from_graphql(JToken data)
        {
            if (data.get("profile_picture") == null)
            {
                data["profile_picture"] = new JObject(new JProperty("uri", ""));
            }
            var c_info = FB_User._parse_customization_info(data);
            var plan = data.get("event_reminders")?.get("nodes")?.FirstOrDefault() != null ? FB_Plan._from_graphql(data.get("event_reminders")?.get("nodes")?.FirstOrDefault()) : null;

            var name = data.get("name")?.Value<string>();
            var first_name = data.get("first_name")?.Value<string>() ?? data.get("short_name")?.Value<string>();
            var last_name = first_name != null ? name?.Replace(first_name, "")?.Trim() : null;

            var gender = GENDER.graphql_GENDERS["UNKNOWN"];
            if (data.get("gender")?.Type == JTokenType.Integer)
            {
                gender = GENDER.standard_GENDERS[data.get("gender")?.Value<int>() ?? 0];
            }
            else
            {
                int gender_int = 0;
                if (int.TryParse(data.get("gender")?.Value<string>(), out gender_int))
                    gender = GENDER.standard_GENDERS[gender_int];
                else
                    gender = GENDER.graphql_GENDERS[data.get("gender")?.Value<string>() ?? "UNKNOWN"];
            };

            return new FB_User(
                uid: data.get("id")?.Value<string>(),
                url: data.get("url")?.Value<string>(),
                name: name,
                first_name: first_name,
                last_name: last_name,
                is_friend: data.get("is_viewer_friend")?.Value<bool>() ?? false,
                gender: gender,
                affinity: data.get("viewer_affinity")?.Value<float>() ?? 0,
                nickname: (string)c_info.GetValueOrDefault("nickname"),
                color: (string)c_info.GetValueOrDefault("color"),
                emoji: (JToken)c_info.GetValueOrDefault("emoji"),
                own_nickname: (string)c_info.GetValueOrDefault("own_nickname"),
                photo: data.get("profile_picture")?.get("uri")?.Value<string>(),
                message_count: data.get("messages_count")?.Value<int>() ?? 0,
                plan: plan);
        }

        public static FB_User _from_thread_fetch(JToken data)
        {
            var c_info = FB_User._parse_customization_info(data);
            var participants = data.get("all_participants")?.get("nodes")?.Select(node => node.get("messaging_actor"));
            var user = participants.Where((p) => p.get("id")?.Value<string>() == data.get("thread_key")?.get("other_user_id")?.Value<string>())?.FirstOrDefault();
            var last_message_timestamp = data.get("last_message")?.get("nodes")?.FirstOrDefault()?.get("timestamp_precise")?.Value<string>();

            var name = user.get("name")?.Value<string>();
            var first_name = user.get("first_name")?.Value<string>() ?? user.get("short_name")?.Value<string>();
            var last_name = first_name != null ? name?.Replace(first_name, "")?.Trim() : null;

            var gender = GENDER.graphql_GENDERS["UNKNOWN"];
            if (data.get("gender")?.Type == JTokenType.Integer)
            {
                gender = GENDER.standard_GENDERS[data.get("gender")?.Value<int>() ?? 0];
            }
            else
            {
                int gender_int = 0;
                if (int.TryParse(data.get("gender")?.Value<string>(), out gender_int))
                    gender = GENDER.standard_GENDERS[gender_int];
                else
                    gender = GENDER.graphql_GENDERS[data.get("gender")?.Value<string>() ?? "UNKNOWN"];
            };

            if (user.get("big_image_src") == null)
            {
                user["big_image_src"] = new JObject(new JProperty("uri", ""));
            }

            var plan = data.get("event_reminders")?.get("nodes")?.FirstOrDefault() != null ? FB_Plan._from_graphql(data.get("event_reminders")?.get("nodes")?.FirstOrDefault()) : null;

            return new FB_User(
                uid: user.get("id")?.Value<string>(),
                url: user.get("url")?.Value<string>(),
                name: name,
                first_name: first_name,
                last_name: last_name,
                is_friend: user.get("is_viewer_friend")?.Value<bool>() ?? false,
                gender: gender,
                affinity: user.get("viewer_affinity")?.Value<float>() ?? 0,
                nickname: (string)c_info.GetValueOrDefault("nickname"),
                color: (string)c_info.GetValueOrDefault("color"),
                emoji: (JToken)c_info.GetValueOrDefault("emoji"),
                own_nickname: (string)c_info.GetValueOrDefault("own_nickname"),
                photo: user.get("big_image_src")?.get("uri")?.Value<string>(),
                message_count: data.get("messages_count")?.Value<int>() ?? 0,
                last_message_timestamp: last_message_timestamp,
                plan: plan);
        }

        public static FB_User _from_all_fetch(JToken data)
        {
            var gender = GENDER.graphql_GENDERS["UNKNOWN"];
            if (data.get("gender")?.Type == JTokenType.Integer)
            {
                gender = GENDER.standard_GENDERS[data.get("gender")?.Value<int>() ?? 0];
            }
            else
            {
                int gender_int = 0;
                if (int.TryParse(data.get("gender")?.Value<string>(), out gender_int))
                    gender = GENDER.standard_GENDERS[gender_int];
                else
                    gender = GENDER.graphql_GENDERS[data.get("gender")?.Value<string>() ?? "UNKNOWN"];
            };

            return new FB_User(
                uid: data.get("id")?.Value<string>(),
                first_name: data.get("firstName")?.Value<string>(),
                url: data.get("uri")?.Value<string>(),
                photo: data.get("thumbSrc")?.Value<string>(),
                name: data.get("name")?.Value<string>(),
                is_friend: data.get("is_friend")?.Value<bool>() ?? false,
                gender: gender
            );
        }
    }

    /// <summary>
    /// User active status
    /// </summary>
    public class FB_ActiveStatus
    {
        /// Whether the user is active now
        public bool active { get; set; }
        /// Timestamp when the user was last active
        public string last_active { get; set; }
        /// Whether the user is playing Messenger game now
        public bool in_game { get; set; }

        /// <summary>
        /// User active status
        /// </summary>
        /// <param name="active"></param>
        /// <param name="last_active"></param>
        /// <param name="in_game"></param>
        public FB_ActiveStatus(bool active = false, string last_active = null, bool in_game = false)
        {
            this.active = active;
            this.last_active = last_active;
            this.in_game = in_game;
        }

        public static FB_ActiveStatus _from_chatproxy_presence(string id_, JToken data)
        {
            return new FB_ActiveStatus(
                active: new int[] { 2, 3 }.Contains(data.get("p")?.Value<int>() ?? 0),
                last_active: data.get("lat")?.Value<string>(),
                in_game: data.get("gamers")?.ToObject<List<string>>()?.Contains(id_) ?? false);
        }

        public static FB_ActiveStatus _from_buddylist_overlay(JToken data, bool in_game = false)
        {
            return new FB_ActiveStatus(
                active: new int[] { 2, 3 }.Contains(data.get("a")?.Value<int>() ?? 0),
                last_active: data.get("la")?.Value<string>(),
                in_game: in_game);
        }

        public static FB_ActiveStatus _from_buddylist_update(JToken data)
        {
            return new FB_ActiveStatus(
                active: new int[] { 0 }.Contains(data.get("status")?.Value<int>() ?? -1),
                last_active: null,
                in_game: false);
        }

        public static FB_ActiveStatus _from_orca_presence(JToken data, bool in_game = false)
        {
            return new FB_ActiveStatus(
                active: new int[] { 2, 3 }.Contains(data.get("p")?.Value<int>() ?? 0),
                last_active: data.get("l")?.Value<string>(),
                in_game: in_game);
        }
    }
}

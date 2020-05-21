﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMessengerCsharp.Client.API
{
    /// <summary>
    /// Represents a user location
    /// Latitude and longitude OR address is provided by Facebook
    /// </summary>
    public class FB_LocationAttachment : FB_Attachment
    {
        /// Latitude of the location
        public double latitude { get; set; }
        /// Longitude of the location
        public double longitude { get; set; }
        /// URL of image showing the map of the location
        public string image_url { get; set; }
        /// Width of the image
        public int image_width { get; set; }
        /// Height of the image
        public int image_height { get; set; }
        /// URL to Bing maps with the location
        public string url { get; set; }
        /// Address of the location
        public string address { get; set; }

        /// <summary>
        /// Represents a user location
        /// Latitude and longitude OR address is provided by Facebook
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="image_url"></param>
        /// <param name="image_width"></param>
        /// <param name="image_height"></param>
        /// <param name="url"></param>
        /// <param name="address"></param>
        public FB_LocationAttachment(string uid = null, double latitude = 0, double longitude = 0, string image_url = null, int image_width = 0, int image_height = 0, string url = null, string address = null)
            : base(uid)
        {
            this.latitude = latitude;
            this.longitude = longitude;
            this.image_url = image_url;
            this.image_width = image_width;
            this.image_height = image_height;
            this.url = url;
            this.address = address;
        }

        public static FB_LocationAttachment _from_graphql(JToken data)
        {
            var url = data.get("url")?.Value<string>();
            var address = Utils.get_url_parameter(Utils.get_url_parameter(url, "u"), "where1");
            double latitude = 0, longitude = 0;
            try
            {
                var split = address.Split(new[] { ", " }, StringSplitOptions.None);
                latitude = Double.Parse(split[0]);
                longitude = Double.Parse(split[1]);
                address = null;
            }
            catch (Exception)
            {
            }

            var rtn = new FB_LocationAttachment(
                uid: data.get("deduplication_key")?.Value<string>(),
                latitude: latitude,
                longitude: longitude,
                address: address);

            var media = data.get("media");
            if (media != null && media.get("image") != null)
            {
                var image = media.get("image");
                rtn.image_url = image?.get("uri")?.Value<string>();
                rtn.image_width = image?.get("width")?.Value<int>() ?? 0;
                rtn.image_height = image?.get("height")?.Value<int>() ?? 0;
            }
            rtn.url = url;

            return rtn;
        }
    }

    /// <summary>
    /// Represents a live user location
    /// </summary>
    public class FB_LiveLocationAttachment : FB_LocationAttachment
    {
        /// Name of the location
        public string name { get; set; }
        /// Timestamp when live location expires
        public string expiration_time { get; set; }
        /// True if live location is expired
        public bool is_expired { get; set; }

        /// <summary>
        /// Represents a live user location
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="image_url"></param>
        /// <param name="image_width"></param>
        /// <param name="image_height"></param>
        /// <param name="url"></param>
        /// <param name="address"></param>
        /// <param name="name"></param>
        /// <param name="expiration_time"></param>
        /// <param name="is_expired"></param>
        public FB_LiveLocationAttachment(string uid = null, double latitude = 0, double longitude = 0, string image_url = null, int image_width = 0, int image_height = 0, string url = null, string address = null, string name = null, string expiration_time = null, bool is_expired = false)
            : base(uid, latitude, longitude, image_url, image_width, image_height, url, address)
        {
            this.name = name;
            this.expiration_time = expiration_time;
            this.is_expired = is_expired;
        }

        public static FB_LiveLocationAttachment _from_pull(JToken data)
        {
            return new FB_LiveLocationAttachment(
                uid: data.get("id")?.Value<string>(),
                latitude: ((data.get("stopReason") == null) ? data.get("coordinate")?.get("latitude")?.Value<double>() ?? 0 : 0) / Math.Pow(10, 8),
                longitude: ((data.get("stopReason") == null) ? data.get("coordinate")?.get("longitude")?.Value<double>() ?? 0 : 0) / Math.Pow(10, 8),
                name: data.get("locationTitle")?.Value<string>(),
                expiration_time: data.get("expirationTime")?.Value<string>(),
                is_expired: data.get("stopReason")?.Value<bool>() ?? false);
        }

        public static new FB_LiveLocationAttachment _from_graphql(JToken data)
        {
            var target = data.get("target");
            var rtn = new FB_LiveLocationAttachment(
                uid: target.get("live_location_id")?.Value<string>(),
                latitude: target.get("coordinate")?.get("latitude")?.Value<double>() ?? 0,
                longitude: target.get("coordinate")?.get("longitude")?.Value<double>() ?? 0,
                name: data.get("title_with_entities")?.get("text")?.Value<string>(),
                expiration_time: target.get("expiration_time")?.Value<string>(),
                is_expired: target.get("is_expired")?.Value<bool>() ?? false);

            var media = data.get("media");
            if (media != null && media.get("image") != null)
            {
                var image = media.get("image");
                rtn.image_url = image?.get("uri")?.Value<string>();
                rtn.image_width = image?.get("width")?.Value<int>() ?? 0;
                rtn.image_height = image?.get("height")?.Value<int>() ?? 0;
            }
            rtn.url = data.get("url")?.Value<string>();

            return rtn;
        }
    }
}

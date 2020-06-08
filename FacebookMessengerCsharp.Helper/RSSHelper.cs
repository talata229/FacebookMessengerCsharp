using CodeHollow.FeedReader;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FacebookMessengerCsharp.Helper
{
    public class RSSHelper
    {
        public static List<NewfeedRss> GetTinMoiNhat()
        {
            List<NewfeedRss> listResult = new List<NewfeedRss>();
            var feed = FeedReader.Read("https://vnexpress.net/rss/tin-moi-nhat.rss");
            foreach (var item in feed.Items)
            {
                listResult.Add(new NewfeedRss
                {
                    Title = item.Title,
                    Link = item.Link,
                    PublishingDate = item.PublishingDate,
                });
            }
            return listResult.Take(10).ToList();
        }
    }
    public class NewfeedRss
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public DateTime? PublishingDate { get; set; }
    }
}

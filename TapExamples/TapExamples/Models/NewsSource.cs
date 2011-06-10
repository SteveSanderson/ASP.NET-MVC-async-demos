using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Collections;
using System.Web.Script.Serialization;

namespace TapExamples.Models
{
    public enum DataFormat { Rss = 1, TwitterJson = 2 }

    public class NewsSource
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public DataFormat DataFormat { get; set; }
    }
    
    public class Headline 
    {
        public NewsSource Source { get; set; }
        public string HeadlineText { get; set; }

        public Headline(NewsSource source, string headlineText)
        {
            Source = source;
            HeadlineText = headlineText;
        }
    }

    public static class ParserUtils
    {
        public static Headline[] ExtractHeadlines(NewsSource newsSource, string rawData)
        {
            switch (newsSource.DataFormat)
            {
                case DataFormat.Rss:
                    var xmlDoc = XDocument.Parse(rawData);
                    return (from channel in xmlDoc.Root.Elements("channel")
                            from item in channel.Elements("item")
                            select new Headline(newsSource, item.Element("title").Value)).ToArray();

                case DataFormat.TwitterJson:
                    var javaScriptObject = (IEnumerable)new JavaScriptSerializer().DeserializeObject(rawData);
                    return (from tweetInfo in javaScriptObject.Cast<IDictionary<string, object>>()
                            select new Headline(newsSource, (string)tweetInfo["text"])).ToArray();

                default:
                    return null;
            }
        }
    }
}
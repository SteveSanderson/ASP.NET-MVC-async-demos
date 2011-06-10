using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TapExamples;
using TapExamples.Models;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using System.IO;
using SignalR.Web;

namespace TapExamples.Controllers
{
    public class HomeController : TaskAsyncController
    {
        #region Data sources

        // Could be loaded from a database (asynchronously!)
        private ICollection<NewsSource> _newsSources = new List<NewsSource> {
            new NewsSource { Name = "Bill Gates' Twitter feed",   DataFormat = DataFormat.TwitterJson, Url = "http://localhost:8088/status/user_timeline/billgates.json?count=10" },
            new NewsSource { Name = "BBC News Twitter feed",      DataFormat = DataFormat.TwitterJson, Url = "http://localhost:8088/status/user_timeline/bbcnews.json?count=6" },
            new NewsSource { Name = "ScottGu's blog RSS feed",    DataFormat = DataFormat.Rss,         Url = "http://localhost:8088/scottgu/rss.aspx" },
            new NewsSource { Name = "MS Press Releases RSS feed", DataFormat = DataFormat.Rss,         Url = "http://localhost:8088/presspass/rss/RSSFeed.aspx" },
        };

        #endregion

        public async Task<ActionResult> FetchAsynchronously()
        {
            var resultItems = new List<Headline>();

            foreach (var newsSource in _newsSources)
            {
                string rawData = await new WebClient().DownloadStringTaskAsync(newsSource.Url);
                resultItems.AddRange(ParserUtils.ExtractHeadlines(newsSource, rawData));
            }

            return View("results", resultItems);
        }

        public async Task<ActionResult> FetchAsynchronouslyParallel()
        {
            var resultItems = new List<Headline>();

            var allTasks = from newsSource in _newsSources
                           select FetchHeadlinesTaskAsync(newsSource);
            Headline[][] results = await TaskEx.WhenAll(allTasks);

            return View("results", results.SelectMany(x => x));
        }

        public async Task<ActionResult> FetchFirstAsynchronouslyParallel()
        {
            var resultItems = new List<Headline>();

            var allTasks = from newsSource in _newsSources
                           select FetchHeadlinesTaskAsync(newsSource);
            Task<Headline[]> firstCompleted = await TaskEx.WhenAny(allTasks);

            return View("results", firstCompleted.Result);
        }

        private async Task<Headline[]> FetchHeadlinesTaskAsync(NewsSource newsSource)
        {
            string rawData = await new WebClient().DownloadStringTaskAsync(newsSource.Url);
            return ParserUtils.ExtractHeadlines(newsSource, rawData);
        }

        private Task<string> FetchStringEventually()
        {
            var completionSource = new TaskCompletionSource<string>();
            return completionSource.Task;

            // Later..
            completionSource.SetResult("Some string");
        }
    }
}

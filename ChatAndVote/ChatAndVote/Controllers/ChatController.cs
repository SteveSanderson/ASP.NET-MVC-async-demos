using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SignalR.Web;
using System.Threading.Tasks;
using System.Threading;

namespace ChatAndVote.Controllers
{
    public class ChatController : TaskAsyncController
    {
        private static TaskCompletionSource<string> nextMessage = new TaskCompletionSource<string>();

        public ActionResult Index()
        {
            return View();
        }

        public async Task<string> GetNextMessage()
        {
            return await nextMessage.Task;
        }

        public void PostMessage(string message)
        {
            nextMessage.SetResult(message);
            nextMessage = new TaskCompletionSource<string>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChatAndVote.Models;
using System.Threading.Tasks;
using SignalR.Web;

namespace ChatAndVote.Controllers
{
    public class VotingController : TaskAsyncController
    {
        static TaskCompletionSource<bool> hasNewVote = new TaskCompletionSource<bool>();

        // Could load this from a DB
        static Question question = new Question {
            QuestionText = "What's your favourite upcoming technology?",
            Answers = new List<Answer> {
                new Answer { AnswerId = 1, AnswerText = "C# 5" },
                new Answer { AnswerId = 2, AnswerText = "WebSockets" },
                new Answer { AnswerId = 3, AnswerText = "Windows 8" },
                new Answer { AnswerId = 5, AnswerText = "Magic rainbow unicorns" },
            }
        };

        public ActionResult Index()
        {
            return View(question);
        }

        public void SubmitVote(int answerId)
        {
            question.Answers.Single(x => x.AnswerId == answerId).NumVotes++;
            hasNewVote.SetResult(true);
            hasNewVote = new TaskCompletionSource<bool>();
        }

        public async Task<JsonResult> GetUpdatedState() {
            await hasNewVote.Task;
            return Json(question);
        }
    }
}

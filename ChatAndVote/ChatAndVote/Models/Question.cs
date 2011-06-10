using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatAndVote.Models
{
    public class Question
    {
        public string QuestionText { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }

    public class Answer
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public int NumVotes { get; set; }
    }
}
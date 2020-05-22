using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silxo_API
{
    public class Question
    {
        //The Questions refrence
        public string refrence;
        //The question
        public string question;

        //list of related answers
        public List<Answer> answers;

        //Type of expected answer
        public string type;

        public Question()
        {
            refrence = string.Empty;
            question = string.Empty;
            answers = new List<Answer>();

            type = string.Empty;
        }

        public void AddAnswer(Answer ans)
        {
            answers.Add(ans);
        }
    }
}
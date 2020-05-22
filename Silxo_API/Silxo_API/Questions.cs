using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silxo_API
{
    public class Questions
    {
        public List<Question> questions = new List<Question>();

        public Questions()
        {

        }

        public void AddQuestions(Question quest)
        {
            questions.Add(quest);
        }
    }
}
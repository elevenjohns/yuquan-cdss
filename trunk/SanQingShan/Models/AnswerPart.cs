using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

namespace SanQingShan.Models
{
    public partial class Answer
    {
        public Answer(Question question)
        {
            this.Choice = new Collection<Choice>();
            if (question != null)
            {
                this.Question = question;
                foreach (var option in question.Option)
                {
                    this.Choice.Add(new Choice() 
                    {
                        Option = option,
                        IsSelected = false
                    });
                }
            }            
        }
    }
}
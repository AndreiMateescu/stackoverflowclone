using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowProject.ViewModels
{
    public class EditAnswerViewModel
    {
        [Required]
        public int AnswerID { get; set; }

        [Required]
        public string AnswerText { get; set; }

        [Required]
        public DateTime AnswerDateAndTime { get; set; }

        [Required]
        public int UserID { get; set; }
        public int QuestionID { get; set; }

        [Required]
        public int VotesCount { get; set; }

        public virtual QuestionViewModel Question { get; set; }
    }
}

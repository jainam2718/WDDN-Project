using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WDDNProject.Models
{
    public class Questions
    {
        public int id { get; set; }
        [Required]
        [DisplayName("Question Statement")]
        public string question { get; set; }
        public string option1 { get; set; }
        public string option2 { get; set; }
        public string option3 { get; set; }
        public string option4 { get; set; }
        [Required]
        [DisplayName("Answer Option")]
        public int ans { get; set; }
        [Required]
        public int ExamId { get; set; }
        public virtual Exam Exam { get; set; }
    }
}

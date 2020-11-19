using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WDDNProject.Models
{
    public class Questions
    {
        public int id { get; set; }
        public string question { get; set; }
        public string option1 { get; set; }
        public string option2 { get; set; }
        public string option3 { get; set; }
        public string option4 { get; set; }
        public string ans { get; set; }

        public int ExamId { get; set; }
        public virtual Exam Exam { get; set; }
    }
}

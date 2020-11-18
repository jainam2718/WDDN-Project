using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WDDNProject.Models;

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
        public int answer { get; set; }
        [ForeignKey("examID")]
        public int examID { get; set; }
        public Exam Exam { get; set; }
    }
}

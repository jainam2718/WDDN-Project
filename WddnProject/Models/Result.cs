using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WDDNProject.Areas.Identity.Data;

namespace WDDNProject.Models
{
    public class Result
    {

        public int id { get; set; }
        [Required]
        public int totalMarks { get; set; }
        [Required]
        public int obtainedMarks { get; set; }
        [Required]
        public String AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        public int ExamId { get; set; }
        public virtual Exam Exam { get; set; }
    }
}
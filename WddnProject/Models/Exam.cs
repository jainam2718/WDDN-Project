using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WDDNProject.Areas.Identity.Data;

namespace WDDNProject.Models
{
    public class Exam
    {
        public int id { get; set; }
        [Required]
        public String Subject { get; set; }
        public String Description { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        public String AppUserId { get; set; }
        public AppUser AppUser { get; set; }

       // [ForeignKey("id")]
        public virtual ICollection<Questions> Questions { get; set; } 
    }

}

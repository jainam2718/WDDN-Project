using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WDDNProject.Areas.Identity.Data;
using WDDNProject.Data;

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
        public virtual AppUser AppUser { get; set; }

        public int? GroupId { get; set; }
        public virtual Group Group { get; set; }

        public virtual ICollection<Questions> Questions { get; set; }

        public virtual ICollection<Result> Results { get; set; }
    }

}
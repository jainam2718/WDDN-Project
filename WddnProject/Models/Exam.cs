using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WDDNProject.Areas.Identity.Data;

namespace WDDNProject.Models
{
    public class Exam
    {
        public int id { get; set; }
        public String Name { get; set; }

        public String AppEmail { get; set; }
        public virtual AppUser AppUser { get; set; }
    }

}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WDDNProject.Areas.Identity.Data;

namespace WDDNProject.Models
{
    public class Group
    {
        public int id { get; set; }
        [Required]
        [DisplayName("Group Name")]
        public String Name { get; set; }
        [Required]
        public String AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }

        public int? GroupMemberId { get; set; }
        public virtual GroupMember GroupMember { get; set; }


    }
}

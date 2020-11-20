using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WDDNProject.Areas.Identity.Data;

namespace WDDNProject.Models
{
    public class AppUserGroupMember
    {

        public String AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        public int GroupMemberId { get; set; }
        public virtual GroupMember GroupMember { get; set; }
    }
}
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WDDNProject.Models;

namespace WDDNProject.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the AppUser class
    public class AppUser : IdentityUser
    {

        public virtual ICollection<Exam> Exams { get; set; }
        
        public virtual ICollection<Group> Groups { get; set; }

        public virtual IList<AppUserGroupMember> AppUserGroupMembers { get; set; }
    }
}

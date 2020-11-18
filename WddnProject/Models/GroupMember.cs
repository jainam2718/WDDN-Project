using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WDDNProject.Areas.Identity.Data;

namespace WDDNProject.Models
{
    public class GroupMember
    {
        public int id { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public IList<AppUserGroupMember> AppUserGroupMembers { get; set; }

    }
}

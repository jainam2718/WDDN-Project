using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WDDNProject.Areas.Identity.Data;

namespace WDDNProject.Models
{
    public class GroupMember
    {
        public int id { get; set; }
        [Required]
        [DisplayName("Group Year")]
        public String Name { get; set; }
        public int? GroupId { get; set; }
        public virtual Group Group { get; set; }

        public IList<AppUserGroupMember> AppUserGroupMembers { get; set; }

    }
}

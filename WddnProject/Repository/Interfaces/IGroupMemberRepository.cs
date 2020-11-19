using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WDDNProject.Models;

namespace WDDNProject.Repository.Interfaces
{
    public interface IGroupMemberRepository
    {
        Task<GroupMember> GetGroupMemberById(int id);
        Task<GroupMember> GetGroupMemberByGroupId(int id);
        Task<int> CreateGroupMember(GroupMember groupMember);
        Task<bool> UpdateGroupMember(GroupMember groupMember);
        Task<int> DeleteGroupMember(int id);
    }
}

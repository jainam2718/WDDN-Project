using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WDDNProject.Models;

namespace WDDNProject.Repository.Interfaces
{
    public interface IGroupRepository
    {
        Task<Group> GetGroupById(int id);
        Task<IEnumerable<Group>> GetGroupsByAppUserId(String AppUserId);
        Task<Group> GetGroupByGroupMemberId(int id);
        Task<int> CreateGroup(Group group);
        Task<bool> UpdateGroup(Group group);
        Task<int> DeleteGroup(int id);
    }
}

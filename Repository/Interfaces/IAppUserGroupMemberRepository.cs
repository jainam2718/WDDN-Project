using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WDDNProject.Models;

namespace WDDNProject.Repository.Interfaces
{
    public interface IAppUserGroupMemberRepository
    {
        Task<IEnumerable<AppUserGroupMember>> GetAppUserGroupMembersByAppUserId(String id);
        Task<IEnumerable<AppUserGroupMember>> GetAppUserGroupMembersByGroupMemberId(int id);
        Task<int> CreateAppUserGroupMember(AppUserGroupMember appUserGroupMember);
        Task<int> DeleteAppUserGroupMember(AppUserGroupMember appUserGroupMember);

    }
}

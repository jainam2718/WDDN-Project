using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WDDNProject.Data;
using WDDNProject.Models;
using WDDNProject.Repository.Interfaces;

namespace WDDNProject.Repository.Implementations
{
    public class AppUserGroupMemberRepository : IAppUserGroupMemberRepository
    {
        private readonly AuthDbContext _context;

        public AppUserGroupMemberRepository(AuthDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<AppUserGroupMember>> GetAppUserGroupMembersByAppUserId(String id)
        {
            return await _context.AppUserGroupMembers
                .Include(e => e.AppUser)
                .Include(e => e.GroupMember)
                .Where(m => m.AppUserId == id)
                .ToListAsync();
        }
        public async Task<IEnumerable<AppUserGroupMember>> GetAppUserGroupMembersByGroupMemberId(int id)
        {
            return await _context.AppUserGroupMembers
                .Include(e => e.AppUser)
                .Include(e => e.GroupMember)
                .Where(m => m.GroupMemberId == id)
                .ToListAsync();
        }
        public async Task<int> CreateAppUserGroupMember(AppUserGroupMember appUserGroupMember)
        {
            _context.Add(appUserGroupMember);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteAppUserGroupMember(AppUserGroupMember appUserGroupMember)
        {
            _context.AppUserGroupMembers.Remove(appUserGroupMember);
            return await _context.SaveChangesAsync();
        }
    }
}

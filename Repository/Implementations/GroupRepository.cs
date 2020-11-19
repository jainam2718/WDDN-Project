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
    public class GroupRepository : IGroupRepository
    {
        private readonly AuthDbContext _context;

        public GroupRepository(AuthDbContext context)
        {
            this._context = context;
        }

        public async Task<Group> GetGroupById(int id)
        {
            return await _context.Groups
                .Include(e => e.GroupMember)
                    .ThenInclude(e => e.AppUserGroupMembers)
                        .ThenInclude(e => e.AppUser)
                .Include(e => e.AppUser)
                .FirstOrDefaultAsync(m => m.id == id);

        }

        public async Task<IEnumerable<Group>> GetGroupsByAppUserId(String AppUserId)
        {
            
            return await _context.Groups.Include(e => e.GroupMember)
                                            .ThenInclude(e => e.AppUserGroupMembers)
                                        .Include(e => e.AppUser)
                                              .Where(e => e.AppUserId == AppUserId)
                                              .ToListAsync();

        }

        public async Task<int> CreateGroup(Group group)
        {
            _context.Add(group);
            return await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateGroup(Group group)
        {
            try
            {
                _context.Update(group);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(group.id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<int> DeleteGroup(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            _context.Groups.Remove(group);
            return await _context.SaveChangesAsync();

        }

        public bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.id == id);
        }

        public async Task<Group> GetGroupByGroupMemberId(int id)
        {
            return await _context.Groups.Include(g => g.AppUser)
                                        .Include(g => g.GroupMember)
                                        .FirstOrDefaultAsync(g => g.GroupMemberId == id);
        }
    }
}

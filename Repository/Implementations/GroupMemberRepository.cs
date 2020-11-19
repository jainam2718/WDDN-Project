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
    public class GroupMemberRepository: IGroupMemberRepository
    {
        private readonly AuthDbContext _context;

        public GroupMemberRepository(AuthDbContext context)
        {
            this._context = context;
        }

        public async Task<GroupMember> GetGroupMemberById(int id)
        {
            return await _context.GroupMembers.Include(g => g.Group)
                                               .FirstOrDefaultAsync(g => g.id == id);
        }
        public async Task<GroupMember> GetGroupMemberByGroupId(int id)
        {
            return await _context.GroupMembers.Include(g => g.Group)
                                              .FirstOrDefaultAsync(g => g.GroupId == id);
        }
        public async Task<int> CreateGroupMember(GroupMember groupMember)
        {
            _context.Add(groupMember);
            return await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateGroupMember(GroupMember groupMember)
        {
            try
            {
                _context.Update(groupMember);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupMemberExists(groupMember.id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }
        public async Task<int> DeleteGroupMember(int id)
        {
            var groupMember = await _context.GroupMembers.FindAsync(id);
            _context.GroupMembers.Remove(groupMember);
            return await _context.SaveChangesAsync();
        }

        public bool GroupMemberExists(int id)
        {
            return _context.GroupMembers.Any(e => e.id == id);
        }
    }
}

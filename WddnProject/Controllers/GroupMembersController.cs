using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WDDNProject.Data;
using WDDNProject.Models;
using WDDNProject.Repository.Interfaces;

namespace WDDNProject.Controllers
{
    public class GroupMembersController : Controller
    {
        private readonly AuthDbContext _context;
        private readonly IGroupMemberRepository _groupMember;
        public GroupMembersController(AuthDbContext context, IGroupMemberRepository groupMember)
        {
            _context = context;
            _groupMember = groupMember;
        }

        // GET: GroupMembers
        public async Task<IActionResult> Index()
        {
            var authDbContext = _context.GroupMembers.Include(g => g.Group);
            return View(await authDbContext.ToListAsync());
        }

        // GET: GroupMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupMember = await _groupMember.GetGroupMemberById((int)id);
            if (groupMember == null)
            {
                return NotFound();
            }

            return View(groupMember);
        }

        // GET: GroupMembers/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "id", "AppUserId");
            return View();
        }

        // POST: GroupMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,GroupId")] GroupMember groupMember)
        {
            if (ModelState.IsValid)
            {
                await _groupMember.CreateGroupMember(groupMember);
                return RedirectToAction("Create", "AppUserGroupMembers", new { groupMemberId = groupMember.id });
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "id", "AppUserId", groupMember.GroupId);
            return View(groupMember);
        }

        // GET: GroupMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupMember = await _groupMember.GetGroupMemberById((int)id);
            if (groupMember == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "id", "AppUserId", groupMember.GroupId);
            return View(groupMember);
        }

        // POST: GroupMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,GroupId")] GroupMember groupMember)
        {
            if (id != groupMember.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _groupMember.UpdateGroupMember(groupMember);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupMemberExists(groupMember.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "id", "AppUserId", groupMember.GroupId);
            return View(groupMember);
        }

        // GET: GroupMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupMember = await _groupMember.GetGroupMemberById((int)id);
            if (groupMember == null)
            {
                return NotFound();
            }

            return View(groupMember);
        }

        // POST: GroupMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groupMember = await _groupMember.GetGroupMemberById(id);
            await _groupMember.DeleteGroupMember(id);
            return RedirectToAction(nameof(Index));
        }

        private bool GroupMemberExists(int id)
        {
            return _context.GroupMembers.Any(e => e.id == id);
        }
    }
}

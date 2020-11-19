using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WDDNProject.Data;
using WDDNProject.Models;
using WDDNProject.Repository.Implementations;
using WDDNProject.Repository.Interfaces;

namespace WDDNProject.Controllers
{
    [Authorize]
    public class AppUserGroupMembersController : Controller
    {
        private readonly AuthDbContext _context;
        private readonly IAppUserGroupMemberRepository _appUserGroupMember;
        public AppUserGroupMembersController(AuthDbContext context, IAppUserGroupMemberRepository appUserGroupMember)
        {
            _context = context;
            _appUserGroupMember = appUserGroupMember;
        }

        // GET: AppUserGroupMembers
        public async Task<IActionResult> Index()
        {
            var authDbContext = _context.AppUserGroupMembers.Include(a => a.AppUser).Include(a => a.GroupMember);
            return View(await authDbContext.ToListAsync());
        }

        // GET: AppUserGroupMembers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUserGroupMember = await _appUserGroupMember.GetAppUserGroupMembersByAppUserId(id);
            if (appUserGroupMember == null)
            {
                return NotFound();
            }

            return View(appUserGroupMember);
        }

        // GET: AppUserGroupMembers/Create
        /*public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Email");
            ViewData["GroupMemberId"] = new SelectList(_context.GroupMembers, "id", "Name");
            return View();
        }*/

        // GET: AppUserGroupMembers/Create/5
        public IActionResult Create(int? groupMemberId)
        {
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Email");
            if(groupMemberId!=null)
                ViewData["GroupMemberId"] = new SelectList(_context.GroupMembers, "id", "Name", groupMemberId);
            else
                ViewData["GroupMemberId"] = new SelectList(_context.GroupMembers, "id", "Name");
            return View();
        }
        // POST: AppUserGroupMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,AppUserId,GroupMemberId")] AppUserGroupMember appUserGroupMember)
        {
            if (ModelState.IsValid)
            {
                await _appUserGroupMember.CreateAppUserGroupMember(appUserGroupMember);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Id", appUserGroupMember.AppUserId);
            ViewData["GroupMemberId"] = new SelectList(_context.GroupMembers, "id", "id", appUserGroupMember.GroupMemberId);
            return View(appUserGroupMember);
        }


        // GET: AppUserGroupMembers1/Delete/5
        public async  Task<IActionResult> Delete(string appUserId, int? groupMemberId)
        {
            if (appUserId == null || groupMemberId == null)
            {
                return NotFound();
            }
            var appUserGroupMember = await _context.AppUserGroupMembers
                .Include(a => a.AppUser)
                .Include(a => a.GroupMember)
                .FirstOrDefaultAsync(m => m.AppUserId == appUserId );
            if(appUserGroupMember == null)
            {
                return NotFound();
            }
            return View(appUserGroupMember);
        }

        private IActionResult JsonResult(string appUserId)
        {
            throw new NotImplementedException();
        }

        // POST: AppUserGroupMembers1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("id,AppUserId,GroupMemberId")] AppUserGroupMember appUserGroupMember)
        {
            _context.AppUserGroupMembers.Remove(appUserGroupMember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

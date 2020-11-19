using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WDDNProject.Data;
using WDDNProject.Models;
using WDDNProject.Repository.Interfaces;

namespace WDDNProject.Controllers
{
    [Authorize]
    public class GroupsController : Controller
    {
        private readonly AuthDbContext _context;
        private readonly IGroupRepository _groupRepository;
        public GroupsController(AuthDbContext context,IGroupRepository groupRepository)
        {
            _context = context;
            this._groupRepository = groupRepository;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var groupList = await _groupRepository.GetGroupsByAppUserId(claim.Value);
            return View(groupList);
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _groupRepository.GetGroupById((int)id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "UserName", claim.Value);
            ViewData["GroupMemberId"] = new SelectList(_context.GroupMembers, "id", "id");
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,AppUserId,GroupMemberId")] Group @group)
        {
            if (ModelState.IsValid)
            {
                await this._groupRepository.CreateGroup(@group);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Email", @group.AppUserId);
            ViewData["GroupMemberId"] = new SelectList(_context.GroupMembers, "id", "Name", @group.GroupMemberId);
            return View(@group);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await this._groupRepository.GetGroupById((int)id);
            if (@group == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Email", @group.AppUserId);
            ViewData["GroupMemberId"] = new SelectList(_context.GroupMembers, "id", "Name", @group.GroupMemberId);
            return View(@group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,AppUserId,GroupMemberId")] Group @group)
        {
            if (id != @group.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool check = await this._groupRepository.UpdateGroup(@group);
                if(!check)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Email", @group.AppUserId);
            ViewData["GroupMemberId"] = new SelectList(_context.GroupMembers, "id", "Name", @group.GroupMemberId);
            return View(@group);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await this._groupRepository.GetGroupById((int)id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this._groupRepository.DeleteGroup(id);
            return RedirectToAction(nameof(Index));
        }

    }
}

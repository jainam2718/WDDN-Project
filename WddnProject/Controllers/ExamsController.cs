using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ExamsController : Controller
    {
        private readonly IExamRepository _examRepository;
        private readonly IGroupRepository _groupRepository;
        public ExamsController( IExamRepository examRepository,IGroupRepository groupRepository)
        {
            this._groupRepository = groupRepository;
            this._examRepository = examRepository;
        }

        // GET: Exams
        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var exam = await _examRepository.GetExamsByAppUserId(claim.Value);
            return View(exam);
        }

        // GET: Exams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await this._examRepository.GetExamById((int)id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // GET: Exams/Create
        public async Task<IActionResult> Create()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            ViewData["AppUserId"] = claim.Value;
            var stime = DateTime.Now;
            ViewData["StartTime"] = stime;
            ViewData["GroupId"] = new SelectList(await _groupRepository.GetGroupsByAppUserId(claim.Value), "id", "Name");
            return View();
        }

        // POST: Exams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Subject,Description,StartTime,EndTime,AppUserId,GroupId")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                await this._examRepository.CreateExam(exam);
                return RedirectToAction("Create","Questions", new { examid = exam.id });
            }
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            ViewData["AppUserId"] = claim.Value;
            //ViewData["GroupId"] = new SelectList(_context.Groups, "id", "AppUserId", exam.GroupId);
            return View(exam);
        }

        // GET: Exams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await this._examRepository.GetExamById((int)id);
            if (exam == null)
            {
                return NotFound();
            }
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            ViewData["AppUserId"] = claim.Value;
            ViewData["GroupId"] = new SelectList(await _groupRepository.GetGroupsByAppUserId(claim.Value), "id", "Name");

            //ViewData["GroupId"] = new SelectList(_context.Groups, "id", "AppUserId", exam.GroupId);
            return View(exam);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Subject,Description,StartTime,EndTime,AppUserId,GroupId")] Exam exam)
        {
            if (id != exam.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool check = await this._examRepository.UpdateExam(exam);
                if (!check)
                {
                    return NotFound();   
                }
                return RedirectToAction(nameof(Index));
            }
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            ViewData["AppUserId"] = claim.Value;
            ViewData["GroupId"] = new SelectList(await _groupRepository.GetGroupsByAppUserId(claim.Value), "id", "Name");

            //ViewData["GroupId"] = new SelectList(_context.Groups, "id", "AppUserId", exam.GroupId);
            return View(exam);
        }

        // GET: Exams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _examRepository.GetExamById((int)id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // POST: Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var temp = await this._examRepository.DeleteExam(id);
            return RedirectToAction(nameof(Index));
        }

    }
}

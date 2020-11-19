using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WDDNProject.Data;
using WDDNProject.Models;
using WDDNProject.Repository.Interfaces;

namespace WDDNProject.Controllers
{
    public class ResultsController : Controller
    {
        private readonly AuthDbContext _context;
        private readonly IResultRepository _resultRepository;

        public ResultsController(AuthDbContext context,IResultRepository resultRepository)
        {
            _context = context;
            _resultRepository = resultRepository;
        }

        // GET: Results
        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            return View(await this._resultRepository.GetResultsByAppUserId(claim.Value));
        }

        // GET: Results/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _resultRepository.GetResultById((int)id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        

        // POST: Results/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,totalMarks,obtainedMarks,AppUserId,ExamId")] Result result)
        {
            if (ModelState.IsValid)
            {
                _context.Add(result);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Id", result.AppUserId);
            ViewData["ExamId"] = new SelectList(_context.Exams, "id", "AppUserId", result.ExamId);
            return View(result);
        }

        // GET: Results/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _resultRepository.GetResultById((int)id);
            if (result == null)
            {
                return NotFound();
            }
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            ViewData["AppUserId"] = claim.Value;
            ViewData["ExamId"] = new SelectList(_context.Exams, "id", "Subject", result.ExamId);
            return View(result);
        }

        // POST: Results/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,totalMarks,obtainedMarks,AppUserId,ExamId")] Result result)
        {
            if (id != result.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool check = await this._resultRepository.UpdateResult(result);
                if(!check)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            ViewData["AppUserId"] = claim.Value;
            ViewData["ExamId"] = new SelectList(_context.Exams, "id", "Subject", result.ExamId);
            return View(result);
        }

        // GET: Results/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await this._resultRepository.GetResultById((int)id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this._resultRepository.DeleteResult(id);
            return RedirectToAction(nameof(Index));
        }

    }
}

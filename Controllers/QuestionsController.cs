using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WDDNProject.Data;
using WDDNProject.Models;
using WDDNProject.Repository.Interfaces;

namespace WDDNProject.Controllers
{
    [Authorize]
    public class QuestionsController : Controller
    {
        private readonly AuthDbContext _context;
        private readonly IQuestionsRepository _questionsRepository, _examRepository;


        public QuestionsController(AuthDbContext context,IQuestionsRepository questionsRepository, IQuestionsRepository examRepository)
        {
            _context = context;
            this._questionsRepository = questionsRepository;
            this._examRepository = examRepository;
        }

        public async Task<IActionResult> Index(int? examid)
        {
            if (examid == null)
            {
                return NotFound();
            }

            var questions = await this._questionsRepository.GetQuestionsByExamId((int)examid);
            ViewData["examid"] = examid;
            if (questions == null)
            {
                return NotFound();
            }

            return View(questions);
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questions = await this._questionsRepository.GetQuestionsById((int)id);
            if (questions == null)
            {
                return NotFound();
            }

            return View(questions);
        }

        // GET: Questions/Create
        public  IActionResult Create (int examid)
        {
            
            ViewData["ExamId"] = new SelectList(_context.Exams, "id", "Subject", examid);
            //ViewData["ExamId"] = new SelectList(_context.Exams, "id", "id",id);
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,question,option1,option2,option3,option4,ans,ExamId")] Questions questions)
        {
            
            if (ModelState.IsValid)
            {
                //return View(questions);
                var temp = await this._questionsRepository.CreateQuestion(questions);
                return RedirectToAction("Index", new { examid = questions.ExamId });
            }
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            ViewData["ExamId"] = new SelectList(_context.Exams.Where(e => e.AppUserId == claim.Value), "id", "AppUserId", questions.ExamId);
            return View(questions);
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questions = await this._questionsRepository.GetQuestionsById((int)id);
            if (questions == null)
            {
                return NotFound();
            }
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            ViewData["ExamId"] = new SelectList(_context.Exams.Where(e => e.AppUserId == claim.Value) , "id", "AppUserId", questions.ExamId);
            return View(questions);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,question,option1,option2,option3,option4,ans,ExamId")] Questions questions)
        {
            if (id != questions.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool check = await this._questionsRepository.UpdateQuestion(questions);
                if(!check)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            ViewData["ExamId"] = new SelectList(_context.Exams.Where(e => e.AppUserId == claim.Value), "id", "AppUserId", questions.ExamId);
            return View(questions);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questions = await this._questionsRepository.GetQuestionsById((int)id);
            if (questions == null)
            {
                return NotFound();
            }

            return View(questions);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this._questionsRepository.DeleteQuestion(id);
            return RedirectToAction(nameof(Index));
        }

    }
}

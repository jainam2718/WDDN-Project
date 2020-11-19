using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WDDNProject.Models;
using WDDNProject.Repository.Interfaces;

namespace WDDNProject.Controllers
{
    public class ExamAnswersController : Controller
    {
        private readonly IExamRepository _examRepository;
        private readonly IResultRepository _resultRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupMemberRepository _groupMemberRepository;
        private readonly IAppUserGroupMemberRepository _appUserGroupMemberRepository;

        public ExamAnswersController(IAppUserGroupMemberRepository appUserGroupMemberRepository,
                                    IExamRepository examRepository, IResultRepository resultRepository,
                                    IGroupMemberRepository groupMemberRepository,
                                    IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
            _groupMemberRepository = groupMemberRepository;
            _examRepository = examRepository;
            _resultRepository = resultRepository;
            _appUserGroupMemberRepository = appUserGroupMemberRepository;
        }


        public async Task<IActionResult> ShowExams()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var groupMember = await _appUserGroupMemberRepository.GetAppUserGroupMembersByAppUserId(claim.Value);
            List<Exam> exams = new List<Exam>(); 
            foreach(var g in groupMember)
            {
                Group temp = await _groupRepository.GetGroupByGroupMemberId(g.GroupMemberId);
                var examsLists = await _examRepository.GetExamsByGroupId(temp.id);
                foreach(var t in examsLists)
                {
                    exams.Add(t);
                }
            }
            return View(exams);
        }

        public async Task<IActionResult> TakeExam(int? id)
        {
            if(id == null)
            {
                NotFound();
            }
            var exam = await _examRepository.GetExamById((int)id);
            return View(exam);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TakeExam(IFormCollection form)
        {
            var result = new Result();
            var exam = await _examRepository.GetExamById(Convert.ToInt32(form["examId"]));
            result.ExamId =  Convert.ToInt32(form["examId"]);
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            result.AppUserId = claim.Value;
            result.totalMarks = exam.Questions.Count();
            int m = 0;
            
            foreach(var question in exam.Questions)
            {
                if(question.ans == form[question.id.ToString()])
                {
                    m++;
                }

            }
            result.obtainedMarks = m;
            await _resultRepository.CreateResult(result);
            return RedirectToAction("Index", "Result");

        }


    }
}

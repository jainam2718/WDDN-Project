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
    public class ResultsController : Controller
    {
        private readonly IResultRepository _resultRepository;

        public ResultsController(IResultRepository resultRepository)
        {
            _resultRepository = resultRepository;

        }

        // GET: Results
        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            return View(await this._resultRepository.GetResultsByAppUserId(claim.Value));
            //return Json(await this._resultRepository.GetResultsByAppUserId(claim.Value));
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



    }
}
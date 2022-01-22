using ImtahanaHazrlg.Helper;
using ImtahanaHazrlg.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ImtahanaHazrlg.Areas.Manage.Controllers
{
    [Area("manage")]
    public class TeamController : Controller
    {
        
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamController(DataContext context, IWebHostEnvironment env)
        {
           _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_context.Teams.Include(x=>x.SocialMedias).ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            Team team = _context.Teams.Include(x=>x.SocialMedias).FirstOrDefault(x => x.Id == id);
            return View(team);
        }
        public IActionResult Delete (int id)
        {
            Team team = _context.Teams.Include(x => x.SocialMedias).FirstOrDefault(x => x.Id == id);
            if (team == null) return NotFound();
            return View(team);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Team team)
        {
            if (!ModelState.IsValid) return View();
            if (team.FormFile == null)
            {
                ModelState.AddModelError("FormFile", "Sekil mutleqdir");
            }
            else if (team.FormFile.Length > 2097152)
            {
                ModelState.AddModelError("FormFile", "Sekil 2Mbdan cox ola bilmez");
            }
            else if (team.FormFile.ContentType != "image/png" && team.FormFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("FormFile", "Sekil formati yanlisdir");
            }
            team.Image = FileManager.Save(_env.WebRootPath, "uploads/teams", team.FormFile);
            _context.Teams.Add(team);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit (Team team)
        {
            if (!ModelState.IsValid) return View();
            Team existTeam = _context.Teams.FirstOrDefault(x => x.Id == team.Id);
            if (existTeam == null) return NotFound();
            existTeam.FullName = team.FullName;
            existTeam.Profession = team.Profession;
            existTeam.Image = team.Image;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Team team)
        {
           
            Team existTeam = _context.Teams.Include(x=>x.SocialMedias).FirstOrDefault(x => x.Id == team.Id);
            if (existTeam == null) return NotFound();
            if (!string.IsNullOrWhiteSpace(team.Image))
            {
                FileManager.Delete(_env.WebRootPath, "uploads/teams", team.Image);
            }
            _context.Teams.Remove(existTeam);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
             

    }
}

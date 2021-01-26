using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Training.Data;
using Training.Models;

namespace Training.Controllers
{
    public class ProfilingController : Controller
    {

        private readonly ApplicationDbContext applicationDbContext;

        public ProfilingController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            var profiling = applicationDbContext.Profilings.ToList();
            return View();
        }
        public IActionResult Create()
        {
            List<Education> educations = new List<Education>();

            educations = (from education in applicationDbContext.Educations
                            select education).ToList();
            educations.Insert(0, new Education { Id = 0, Degree = "Select" });

            ViewBag.ListofEducations = educations;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Profiling profiling, Account account)
        {
            applicationDbContext.Add(profiling);
            applicationDbContext.Add(account);
            var result = applicationDbContext.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Edit(int? Id)
        {

            List<Education> educations = new List<Education>();

            educations = (from education in applicationDbContext.Educations
                          select education).ToList();
            educations.Insert(0, new Education { Id = 0, Degree = "Select" });

            ViewBag.ListofEducations = educations;

            if (Id == null)
            {
                return View();
            }
            var result = applicationDbContext.Educations.Find(Id);
            if (result == null)
            {
                return View();
            }
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(int? Id, Profiling profiling)
        {
            if (Id == null)
            {
                return View();
            }
            var get = applicationDbContext.Profilings.Find(Id);
            if (get != null)
            {
                get.Education_Id = profiling.Education_Id;
                applicationDbContext.Entry(get).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                var result = applicationDbContext.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            return View();
        }

        public IActionResult Delete(int Id)
        {
            var get = applicationDbContext.Educations.Find(Id);
            if (get != null)
            {
                applicationDbContext.Educations.Remove(get);
                var result = applicationDbContext.SaveChanges();
                if (result > 0)
                {
                    return Json(result);
                }
            }
            return Json(0);
        }
    }
}

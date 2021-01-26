using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Training.Data;
using Training.Models;

namespace Training.Controllers
{
    public class EducationController : Controller
    {

        private readonly ApplicationDbContext applicationDbContext;

        public EducationController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public IActionResult Index()
        {
            var educations = applicationDbContext.Educations.ToList();
            return View(educations);
        }

        public IActionResult Create()
        {
            List<University> universities = new List<University>();

            universities = (from university in applicationDbContext.Universities
                            select university).Where(x => x.IsAvailable == true).ToList();
            universities.Insert(0, new University { Id = 0, Name = "Select"});

            ViewBag.ListofUniversities = universities;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Education education)
        {
            applicationDbContext.Add(education);
            var result = applicationDbContext.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Edit(int? Id)
        {

            List<University> universities = new List<University>();

            universities = (from university in applicationDbContext.Universities
                            select university).Where(x => x.IsAvailable == true).ToList();
            universities.Insert(0, new University { Id = 0, Name = "Select" });

            ViewBag.ListofUniversities = universities;

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
        public IActionResult Edit(int? Id, Education education)
        {
            if (Id == null)
            {
                return View();
            }
            var get = applicationDbContext.Educations.Find(Id);
            if (get != null)
            {
                get.GPA = education.GPA;
                get.Univiersity_Id = education.Univiersity_Id;
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

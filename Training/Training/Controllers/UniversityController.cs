using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Training.Data;
using Training.Models;

namespace Training.Controllers
{
    public class UniversityController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UniversityController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            var universities = applicationDbContext.Universities.Where(x => x.IsAvailable == true).ToList();
            return View(universities);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(University university)
        {
            applicationDbContext.Add(university);
            var result = applicationDbContext.SaveChanges();
            if(result > 0)
            {
             return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Edit(int? Id)
        {
            if(Id == null)
            {
                return View();
            }
            var result = applicationDbContext.Universities.Find(Id);
            if(result == null)
            {
                return View();
            }
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(int? Id, University university)
        {
            if(Id == null)
            {
                return View();
            }
            var get = applicationDbContext.Universities.Find(Id);
            if(get != null)
            {
                get.Name = university.Name;
                get.IsAvailable = university.IsAvailable;
                applicationDbContext.Entry(get).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                var result = applicationDbContext.SaveChanges();
                if(result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            return View();
        }

        public IActionResult Delete(int Id)
        {
            var get = applicationDbContext.Universities.Find(Id);
            if(get != null)
            {
                applicationDbContext.Universities.Remove(get);
                var result = applicationDbContext.SaveChanges();
                if(result > 0)
                {
                    return Json(result);
                }
            }
            return Json(0);
        }
    }
}

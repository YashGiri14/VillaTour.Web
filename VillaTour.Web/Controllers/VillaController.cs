﻿using Microsoft.AspNetCore.Mvc;
using VillaTour.Domain.Entities;
using VillaTour.Infrastructure.Data;
using static System.Collections.Specialized.BitVector32;

namespace VillaTour.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _db;
        public VillaController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var villas = _db.Villas.ToList();
            return View(villas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa obj)
        {
            if (obj.Name == obj.Description)
            {
                ModelState.AddModelError("name", "The description cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {

                _db.Villas.Add(obj);
                _db.SaveChanges();
                //_villaService.CreateVilla(obj);
                TempData["success"] = "The villa has been created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Update(int villaId)
        {
            Villa? obj = _db.Villas.FirstOrDefault(x => x.Id == villaId);
            //Villa? obj = _villaService.GetVillaById(villaId);
            if (obj == null)
            {      
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(Villa obj)
        {
            if (ModelState.IsValid && obj.Id > 0)
            {
                _db.Villas.Update(obj);
                _db.SaveChanges();
                //_villaService.UpdateVilla(obj);
                TempData["success"] = "The villa has been updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(int villaId)
        {
            Villa? obj = _db.Villas.FirstOrDefault(x => x.Id == villaId);
            //Villa? obj = _villaService.GetVillaById(villaId);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? objFormDb = _db.Villas.FirstOrDefault(u=>u.Id == obj.Id);
            if (objFormDb is not null)
            {
                _db.Villas.Remove(objFormDb);
                _db.SaveChanges();
                //_villaService.UpdateVilla(obj);
                TempData["success"] = "The villa has been Deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "The villa could not be deleted.";
            return View();
        }

    }
}


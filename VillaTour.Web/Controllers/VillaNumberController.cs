using Microsoft.AspNetCore.Mvc;
using VillaTour.Domain.Entities;
using VillaTour.Infrastructure.Data;


namespace VillaTour.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _db;
        public VillaNumberController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var villaNumbers = _db.VillaNumbers.ToList();
            return View(villaNumbers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(VillaNumber obj)
        {
            
            if (ModelState.IsValid)
            {

                _db.VillaNumbers.Add(obj);
                _db.SaveChanges();
                //_villaService.CreateVilla(obj);
                TempData["success"] = "The villa Number has been created successfully.";
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


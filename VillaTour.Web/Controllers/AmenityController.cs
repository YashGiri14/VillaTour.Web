using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VillaTour.Application.Common.Interfaces;
using VillaTour.Domain.Entities;
using VillaTour.Infrastructure.Data;
using VillaTour.Web.ViewModels;


namespace VillaTour.Web.Controllers
{
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var amenities = _unitOfWork.Amenity.GetAll(includeProperties: "Villa");
            return View(amenities);
        }

        public IActionResult Create()
        {
            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Create(AmenityVM obj)
        {

           // bool roomNumbersExists = _unitOfWork.Amenity.Any(u=>u.Villa_Number == obj.Amenity.Villa_Number);
            // ModelState.Remove("Villa");
            if (ModelState.IsValid)
            {

                _unitOfWork.Amenity.Add(obj.Amenity);
                _unitOfWork.Save();
                //_villaService.CreateVilla(obj);
                TempData["success"] = "The amenity has been created successfully.";
                return RedirectToAction(nameof(Index));
            }
            obj.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(obj);
        }

        public IActionResult Update(int amenityId)
        {

            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Amenity = _unitOfWork.Amenity.Get(u => u.Id==amenityId)
            };
           
            if (amenityVM.Amenity == null)
            {      
                return RedirectToAction("Error", "Home");
            }
            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Update(AmenityVM amenityVM)
        {
            if (ModelState.IsValid)
            {

                _unitOfWork.Amenity.Update(amenityVM.Amenity);
                _unitOfWork.Save();
                //_villaService.CreateVilla(obj);
                TempData["success"] = "The amenity has been updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            amenityVM.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(amenityVM);
        }

        public IActionResult Delete(int amenityId)
        {

            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Amenity = _unitOfWork.Amenity.Get(u => u.Id == amenityId)
            };

            if (amenityVM.Amenity == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Delete(AmenityVM AmenityVM)
        {
            Amenity? objFormDb = _unitOfWork.Amenity.Get(u => u.Id == AmenityVM.Amenity.Id);
            if (objFormDb is not null)
            {
                _unitOfWork.Amenity.Remove(objFormDb);
                _unitOfWork.Save();
                //_villaService.UpdateVilla(obj);
                TempData["success"] = "The amenity has been Deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "The amenity could not be deleted.";
            return View();
        }

    }
}


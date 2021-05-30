using BookLibrary.DataAccess.Repository.IRepository;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitofWork _unitofWork;

        public CoverTypeController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();
            if (id == null)
            {
                //This is for Create
                return View(coverType);
            }
            //This is for Create
            coverType = _unitofWork.CoverType.Get(id.GetValueOrDefault());
            if (coverType == null)
            {
                return NotFound();

            }
            return View(coverType);

        }

        
        #region API Calls


        [HttpGet]
        public IActionResult GetAll()
        {
            var allobj = _unitofWork.CoverType.GetAll();
            return Json(new { data = allobj });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                if (coverType.Id == 0)
                {
                    _unitofWork.CoverType.Add(coverType);
                }
                else
                {
                    _unitofWork.CoverType.Update(coverType);
                }
                _unitofWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(coverType);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDB = _unitofWork.CoverType.Get(id);
            if (objFromDB == null)
            {
                return Json(new {success=false,message="Error While Deleting" });
            }
            _unitofWork.CoverType.Remove(objFromDB);
            _unitofWork.Save();
            return Json(new { success = true, message = "Delete Success" });
        }
        #endregion




    }
}

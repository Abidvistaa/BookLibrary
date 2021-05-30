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
    public class CategoryController : Controller
    {
        private readonly IUnitofWork _unitofWork;

        public CategoryController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            if (id == null)
            {
                //This is for Create
                return View(category);
            }
            //This is for Create
            category = _unitofWork.Category.Get(id.GetValueOrDefault());
            if (category == null)
            {
                return NotFound();

            }
            return View(category);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id==0)
                {
                    _unitofWork.Category.Add(category);
                }
                else
                {
                    _unitofWork.Category.Update(category);
                }
                _unitofWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }


        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            var allobj = _unitofWork.Category.GetAll();
            return Json(new { data = allobj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDB = _unitofWork.Category.Get(id);
            if (objFromDB == null)
            {
                return Json(new {success=false,message="Error While Deleting" });
            }
            _unitofWork.Category.Remove(objFromDB);
            _unitofWork.Save();
            return Json(new { success = true, message = "Delete Success" });
        }
        #endregion




    }
}

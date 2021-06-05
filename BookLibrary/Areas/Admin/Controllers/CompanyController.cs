using BookLibrary.DataAccess.Repository.IRepository;
using BookLibrary.Models;
using BookLibrary.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin+","+SD.Role_User_Employee)]
    public class CompanyController : Controller
    {
        private readonly IUnitofWork _unitofWork;

        public CompanyController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Company company = new Company();
            if (id == null)
            {
                //This is for Create
                return View(company);
            }
            //This is for Create
            company = _unitofWork.Company.Get(id.GetValueOrDefault());
            if (company == null)
            {
                return NotFound();

            }
            return View(company);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.Id == 0)
                {
                    _unitofWork.Company.Add(company);
                }
                else
                {
                    _unitofWork.Company.Update(company);
                }
                _unitofWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }


        #region API Calls


        [HttpGet]
        public IActionResult GetAll()
        {
            var allobj = _unitofWork.Company.GetAll();
            return Json(new { data = allobj });
        }

        
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDB = _unitofWork.Company.Get(id);
            if (objFromDB == null)
            {
                return Json(new {success=false,message="Error While Deleting" });
            }
            _unitofWork.Company.Remove(objFromDB);
            _unitofWork.Save();
            return Json(new { success = true, message = "Delete Success" });
        }
        #endregion




    }
}

using BookLibrary.DataAccess.Repository.IRepository;
using BookLibrary.Models;
using BookLibrary.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using BookLibrary.Utility;
using Microsoft.AspNetCore.Authorization;

namespace BookLibrary.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitofWork unitofWork, IWebHostEnvironment hostEnvironment)
        {
            _unitofWork = unitofWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _unitofWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _unitofWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })

            };

            if (id == null)
            {
                //This is for Create
                return View(productVM);
            }

            //This is for Create
            productVM.Product = _unitofWork.Product.Get(id.GetValueOrDefault());
            if (productVM.Product == null)
            {
                return NotFound();

            }
            return View(productVM);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {

            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    var uploads = Path.Combine(webRootPath, @"images\products");
                    string filename = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(files[0].FileName);

                    if (productVM.Product.ImageUrl != null)
                    {
                        //this is an edit and we need to remove old image
                        
                        var imagePath = Path.Combine(webRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    productVM.Product.ImageUrl = @"\images\products\" + filename + extension;
                }
                else
                {
                    //Update when they do not change the image
                    if (productVM.Product.Id != 0)
                    {
                        Product objFromDB = _unitofWork.Product.Get(productVM.Product.Id);
                        productVM.Product.ImageUrl = objFromDB.ImageUrl;
                    }
                }


                if (productVM.Product.Id == 0)
                {
                    _unitofWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitofWork.Product.Update(productVM.Product);
                }
                _unitofWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(productVM);
        }

    
        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            var allobj = _unitofWork.Product.GetAll(includeProperties:"Category,CoverType");
            return Json(new { data = allobj });
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDB = _unitofWork.Product.Get(id);
            if (objFromDB == null)
            {
                return Json(new {success=false,message="Error While Deleting" });
            }
            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, objFromDB.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _unitofWork.Product.Remove(objFromDB);
            _unitofWork.Save();
            return Json(new { success = true, message = "Delete Success" });
        }
        #endregion

    }
}

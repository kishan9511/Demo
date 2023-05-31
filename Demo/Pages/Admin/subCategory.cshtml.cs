using Demo.CommonModel;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Demo.Pages.Admin
{
    public class subCategoryModel : PageModel
    {
        public readonly DemoProjectContext _db;
        public readonly IHostEnvironment _hostEnvironment;
        public subCategoryModel(DemoProjectContext db, IHostEnvironment hostEnvironment_environment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment_environment;
        }


        public IEnumerable<SubCategory_Model> subdatalist { get; set; }
        public List<SelectListItem> selectProCatOptions { get; set; }

        [BindProperty]
        public SubCategory subCategory { get; set; }

        [BindProperty]
        public IFormFile Imagee { get; set; }



        [BindProperty]
        public int catID { get; set; }

        public IActionResult OnGet(int? id)
        {
            subdatalist = _db.SubCategories.Where(p => p.Isdeleted == false).Select(x => new SubCategory_Model
            {

                Id = x.Id,
                Icon = x.Icon,
                CategoryID = x.ProductCategoryId,
                CategoryName = x.ProductCategory.Name,
                name = x.Name

            }).ToList();

            subCategory = new SubCategory();


            if (id.HasValue)
            {
                var po = _db.SubCategories.Where(a => a.Id == id && a.Isdeleted == false).FirstOrDefault();

                if (po == null)
                {
                    return NotFound();
                }

                subCategory = po;

            }




            #region----------DDL get Method for categories---------
            //for  categories list
            selectProCatOptions = _db.ProductCategories.Where(g => g.Isdeleted == false).Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            }).ToList();

            // for update id 
            if (subCategory?.ProductCategoryId > 0)
            {
                var catResult = _db.ProductCategories.Where(g => g.Id == subCategory.ProductCategoryId).Select(g => g).FirstOrDefault();
                selectProCatOptions.Insert(0, new SelectListItem()
                {
                    Text = catResult.Name,
                    Value = catResult.Id.ToString()

                });
            }
            else
            {
                selectProCatOptions.Insert(0, new SelectListItem()
                {
                    Text = "----Select----",
                    Value = string.Empty
                });
            }
            //for selectd categories

            List<ProductCategory> listobjcat = new List<ProductCategory>();
            listobjcat = (from catdobj in _db.ProductCategories where catdobj.Id == catID select catdobj).ToList();


            #endregion




            return Page();
        }
        public IActionResult OnPost()
        {
            try
            {


                if (subCategory.Id > 0)
                {
                    var duplicatechk = _db.SubCategories.Where(x => x.Isdeleted == false && x.Name.Trim().ToLower() == subCategory.Name.Trim().ToLower() && x.Id != subCategory.Id).Count();

                    if (duplicatechk == 0)
                    {
                        var pro = _db.SubCategories.AsNoTracking().Where(c => subCategory.Id == subCategory.Id && c.Isdeleted == false).FirstOrDefault();



                        if (Imagee != null)
                        {
                            subCategory.Icon = ProcessUploadedFile();
                        }
                        else
                        {
                            subCategory.Icon = pro.Icon;
                        }



                        subCategory.ProductCategoryId = catID;
                        subCategory.Isdeleted = pro.Isdeleted;

                        _db.SubCategories.Update(subCategory);
                        _db.SaveChanges();
                        TempData["info"] = "Your Data update Successfully";
                    }



                    else
                    {
                        TempData["error"] = "Duplicate entry not allowed";

                    }
                }

                else
                {

                    var duplicatechk = _db.SubCategories.Where(x => x.Isdeleted == false && x.Name.Trim().ToLower() == subCategory.Name.Trim().ToLower()).Count();

                    if (duplicatechk == 0)
                    {



                        if (Imagee != null)
                        {
                            subCategory.Icon = ProcessUploadedFile();
                        }
                        else
                        {
                            subCategory.Icon = "noimage.webp";
                        }

                        subCategory.ProductCategoryId = catID;
                        subCategory.Isdeleted = false;


                        _db.SubCategories.Add(subCategory);
                        _db.SaveChanges();
                        TempData["success"] = "data saved";

                    }

                    else
                    {
                        TempData["error"] = "Duplicate entry not allowed";

                    }
                }

                return RedirectToPage();
            }

            catch (Exception)
            {
                TempData["error"] = "Something went wrong";
                return RedirectToPage();
            }
        }



        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;

            if (Imagee != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot\\Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Imagee.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Imagee.CopyTo(fileStream);
                }
            }
            return uniqueFileName;

        }






        public IActionResult OnPostDelete(int id)
        {
            var po = _db.SubCategories.Where(e => e.Id == id && e.Isdeleted == false).FirstOrDefault();
            if (po == null)
            {
                return NotFound();
            }
            po.Isdeleted = true;
            _db.SaveChanges();

            TempData["success"] = "data deleted";
            return RedirectToPage();

        }


    }
}

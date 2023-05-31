using Demo.CommonModel;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Demo.Pages.Admin
{
    public class ProductBrandModel : PageModel
    {
        public readonly DemoProjectContext _db;

        public ProductBrandModel(DemoProjectContext db)
        {
            _db = db;
        }

        public IEnumerable<Brand_Model> Branddatalist { get; set; }


        public List<SelectListItem> SelectSubCategoryOption { get; set; }
        public List<SelectListItem> selectProCatOptions { get; set; }




        [BindProperty]
        public int SubCategoryID { get; set; }

        [BindProperty]
        public int catID { get; set; }

        [BindProperty]
        public ProductBrand Brand { get; set; }

        public IActionResult OnGet(int? id)
        {

            Branddatalist = _db.ProductBrands.Where(x => x.Isdeleted == false).Select(x => new Brand_Model
            {

                brandid = x.BrandId,
                subCategoryId = x.SubCategoryId,
                CategoryID = x.ProductCategoryId,
                CategoryName = x.ProductCategory.Name,

                brandname = x.BrandName,
                subCategoryname = x.SubCategory.Name
            }).ToList();

            Brand = new ProductBrand();


            if (id.HasValue)
            {
                var ProBrand = _db.ProductBrands.Where(b => b.BrandId == id && b.Isdeleted == false).FirstOrDefault();
                if (ProBrand == null)
                {
                    return NotFound();
                }
                Brand = ProBrand;

            }


            #region----------DDL get Method for categories---------
            //for  categories list
            selectProCatOptions = _db.ProductCategories.Where(g => g.Isdeleted == false).Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            }).ToList();

            // for update id 
            if (Brand?.ProductCategoryId > 0)
            {
                var catResult = _db.ProductCategories.Where(g => g.Id == Brand.ProductCategoryId).Select(g => g).FirstOrDefault();
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



            #region----------DDL get Method for SubCategory---------

            //for  SubCategory list
            SelectSubCategoryOption = _db.SubCategories.Where(g => g.Isdeleted == false).Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            }).ToList();

            // for update id 
            if (Brand?.SubCategoryId > 0)
            {
                var subcategoriesResult = _db.SubCategories.Where(g => g.Id == Brand.SubCategoryId).Select(g => g).FirstOrDefault();
                SelectSubCategoryOption.Insert(0, new SelectListItem()
                {
                    Text = subcategoriesResult.Name,
                    Value = subcategoriesResult.Id.ToString()

                });
            }
            else
            {
                SelectSubCategoryOption.Insert(0, new SelectListItem()
                {
                    Text = "----Select----",
                    Value = string.Empty
                });
            }
            //for selectd subcategories

            List<SubCategory> listobjsub = new List<SubCategory>();
            listobjsub = (from subidobj in _db.SubCategories where subidobj.Id == SubCategoryID select subidobj).ToList();







            #endregion







            return Page();

        }

        public IActionResult OnPost()
        {
            try
            {

                if (Brand.BrandId > 0)
                {
                    var duplicatechk = _db.ProductBrands.Where(b => b.Isdeleted == false && b.BrandName.Trim().ToLower() == Brand.BrandName.Trim().ToLower() && b.BrandId != Brand.BrandId).Count();
                    if (duplicatechk == 0)
                    {
                        var ProBrand = _db.ProductBrands.AsNoTracking().Where(n => n.BrandId == Brand.BrandId && n.Isdeleted == false).FirstOrDefault();

                        Brand.ProductCategoryId = catID;
                        Brand.SubCategoryId = SubCategoryID;
                        Brand.Isdeleted = ProBrand.Isdeleted;

                        _db.ProductBrands.Update(Brand);
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
                    var duplicatechk = _db.ProductBrands.Where(b => b.Isdeleted == false && b.BrandName.Trim().ToLower() == Brand.BrandName.Trim().ToLower()).Count();
                    if (duplicatechk == 0)
                    {
                        Brand.ProductCategoryId = catID;
                        Brand.SubCategoryId = SubCategoryID;
                        Brand.Isdeleted = false;

                        _db.ProductBrands.Add(Brand);
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



        public IActionResult OnPostDelete(int id)
        {
            var ProBrandd = _db.ProductBrands.Where(e => e.BrandId == id && e.Isdeleted == false).FirstOrDefault();
            if (ProBrandd == null)
            {
                return NotFound();
            }
            ProBrandd.Isdeleted = true;
            _db.SaveChanges();

            TempData["success"] = "data deleted";
            return RedirectToPage();

        }




    }
}

using Demo.CommonModel;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Demo.Pages.Admin
{
    public class Product_ModelModel : PageModel
    {
        public readonly DemoProjectContext _db;
        public Product_ModelModel(DemoProjectContext db)
        {
            _db = db;
        }


        public IEnumerable<Prodm_Model> ProDatalist { get; set; }




        public List<SelectListItem> SelectSubCategoryOption { get; set; }
        public List<SelectListItem> SelectBrandOptions { get; set; }




        [BindProperty]
        public int SubCategoryID { get; set; }


        [BindProperty]
        public int BrandID { get; set; }

        [BindProperty]
        public ProductModel ProModel { get; set; }


        public IActionResult OnGet(int? id)

        {



            ProDatalist = _db.ProductModels.Where(x => x.Isdeleted == false).Select(x => new Prodm_Model

            {
                modelid = x.Modelid,
                brandid = x.BrandId,
                subCategoryId = x.SubCategoryId,

                modelname = x.ModelName,
                brandname = x.Brand.BrandName,
                subCategoryname = x.SubCategory.Name
            }).ToList();


            ProModel = new ProductModel();
            if (id.HasValue)
            {
                var Promod = _db.ProductModels.Where(p => p.Modelid == id && p.Isdeleted == false).FirstOrDefault();
                if (Promod == null)
                {
                    return Page();
                }
                ProModel = Promod;
            }



            #region----------DDL get Method for SubCategory---------
            //for  SubCategory list
            SelectSubCategoryOption = _db.SubCategories.Where(g => g.Isdeleted == false).Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            }).ToList();

            // for update id 
            if (ProModel?.SubCategoryId > 0)
            {
                var subcategoriesResult = _db.SubCategories.Where(g => g.Id == ProModel.SubCategoryId).Select(g => g).FirstOrDefault();
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




            #region----------DDL get Method for Brand---------
            //for  brand list
            SelectBrandOptions = _db.ProductBrands.Where(g => g.Isdeleted == false).Select(g => new SelectListItem
            {
                Value = g.BrandId.ToString(),
                Text = g.BrandName
            }).ToList();

            // for update id 
            if (ProModel?.BrandId > 0)
            {
                var BrandResult = _db.ProductBrands.Where(g => g.BrandId == ProModel.BrandId).Select(g => g).FirstOrDefault();

                SelectBrandOptions.Insert(0, new SelectListItem()
                {
                    Text = BrandResult.BrandName,
                    Value = BrandResult.BrandId.ToString()

                });
            }
            else
            {
                SelectBrandOptions.Insert(0, new SelectListItem()
                {
                    Text = "----Select----",
                    Value = string.Empty
                });
            }
            //for selectd Brand

            List<ProductBrand> listobjbrand = new List<ProductBrand>();
            listobjbrand = (from bidobj in _db.ProductBrands where bidobj.BrandId == BrandID select bidobj).ToList();

            #endregion

            return Page();
        }


        public IActionResult OnPost()

        {
            try
            {
                if (ProModel.Modelid > 0)
                {
                    var duplicatechk = _db.ProductModels.Where(x => x.Isdeleted == false && x.BrandId == BrandID && x.ModelName.Trim().ToLower() == ProModel.ModelName.Trim().ToLower() && x.Modelid != ProModel.Modelid).Count();

                    if (duplicatechk == 0)
                    {
                        var Promod = _db.ProductModels.AsNoTracking().Where(p => p.Modelid == ProModel.Modelid && p.Isdeleted == false).FirstOrDefault();

                        ProModel.BrandId = BrandID;
                        ProModel.SubCategoryId = SubCategoryID;

                        ProModel.Isdeleted = Promod.Isdeleted;

                        _db.ProductModels.Update(ProModel);
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
                    var duplicatechk = _db.ProductModels.Where(x => x.Isdeleted == false && x.BrandId == BrandID && x.ModelName.Trim().ToLower() == ProModel.ModelName.Trim().ToLower()).Count();
                    if (duplicatechk == 0)
                    {
                        ProModel.BrandId = BrandID;
                        ProModel.SubCategoryId = SubCategoryID;

                        ProModel.Isdeleted = false;

                        _db.ProductModels.Add(ProModel);
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
            var Promood = _db.ProductModels.Where(e => e.Modelid == id && e.Isdeleted == false).FirstOrDefault();
            if (Promood == null)
            {
                return NotFound();
            }
            Promood.Isdeleted = true;
            _db.SaveChanges();

            TempData["success"] = "data deleted";
            return RedirectToPage();

        }
    }
}

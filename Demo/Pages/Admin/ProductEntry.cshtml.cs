using Demo.CommonModel;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Demo.Pages.Admin
{
    public class ProductEntryModel : PageModel
    {
        public readonly DemoProjectContext _db;
        public ProductEntryModel(DemoProjectContext db)
        {
            _db = db;
        }


        public IEnumerable<entry_Model> EntryDatalist { get; set; }


        public List<SelectListItem> SelectSubCategoryOption { get; set; }
        public List<SelectListItem> selectProCatOptions { get; set; }

        public List<SelectListItem> SelectBrandOptions { get; set; }


        public List<SelectListItem> SelectModelOptions { get; set; }

        public List<SelectListItem> selectseriOptions { get; set; }




        [BindProperty]
        public int SubCategoryID { get; set; }

        [BindProperty]
        public int catID { get; set; }

        [BindProperty]
        public int BrandID { get; set; }

        [BindProperty]
        public int Mid { get; set; }

        [BindProperty]
        public int seriID { get; set; }

        [BindProperty]
        public ProductEntry ProductEnt { get; set; }


        public IActionResult OnGet(int? id)
        {
            EntryDatalist = _db.ProductEntries.Where(x => x.Isdeleted == false).Select(x => new entry_Model
            {
                productEntryid = x.ProductEntryid,
                productEntryName = x.ProductName,

                productBrandid = x.BrandId,
                productBrandName = x.Brand.BrandName,

                productModelid = x.Modelid,
                productModelName = x.Model.ModelName,

                productSericeid = x.Seriseid,
                productSericenumber = x.Serise.SeriseNumber,

                productCategoryid = x.Id,
                productCategoryName = x.IdNavigation.Name,

                subCategoryId = x.SubCategoryId,
                subCategoryname = x.SubCategory.Name,

                price = (int?)x.CurrentPrice,
                Available = x.IsAvailable

            }).ToList();

            ProductEnt = new ProductEntry();
            if (id.HasValue)
            {
                var proEnt = _db.ProductEntries.Where(e => e.ProductEntryid == id && e.Isdeleted == false).FirstOrDefault();

                if (proEnt == null)
                {
                    return NotFound();

                }

                ProductEnt = proEnt;

            }



            #region----------DDL get Method for ProCat---------
            //for  Model list
            selectProCatOptions = _db.ProductCategories.Where(g => g.Isdeleted == false).Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            }).ToList();

            // for update id 
            if (ProductEnt?.Id > 0)
            {
                var catResult = _db.ProductCategories.Where(g => g.Id == ProductEnt.Id).Select(g => g).FirstOrDefault();
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
            //for selectd Procat

            List<ProductCategory> listobjcat = new List<ProductCategory>();
            listobjcat = (from catdobj in _db.ProductCategories where catdobj.Id == catID select catdobj).ToList();


            #endregion


            #region----------DDL get Method for SubCategory---------
            //for  brand list
            SelectSubCategoryOption = _db.SubCategories.Where(g => g.Isdeleted == false).Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            }).ToList();

            // for update id 
            if (ProductEnt?.SubCategoryId > 0)
            {
                var subcategoriesResult = _db.SubCategories.Where(g => g.Id == ProductEnt.SubCategoryId).Select(g => g).FirstOrDefault();
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
            if (ProductEnt?.BrandId > 0)
            {
                var BrandResult = _db.ProductBrands.Where(g => g.BrandId == ProductEnt.BrandId).Select(g => g).FirstOrDefault();
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


            #region----------DDL get Method for Model---------
            //for  Model list
            SelectModelOptions = _db.ProductModels.Where(g => g.Isdeleted == false).Select(g => new SelectListItem
            {
                Value = g.Modelid.ToString(),
                Text = g.ModelName
            }).ToList();

            // for update id 
            if (ProductEnt?.Modelid > 0)
            {
                var ModelResult = _db.ProductModels.Where(g => g.Modelid == ProductEnt.Modelid).Select(g => g).FirstOrDefault();
                SelectModelOptions.Insert(0, new SelectListItem()
                {
                    Text = ModelResult.ModelName,
                    Value = ModelResult.Modelid.ToString()

                });
            }
            else
            {
                SelectModelOptions.Insert(0, new SelectListItem()
                {
                    Text = "----Select----",
                    Value = string.Empty
                });
            }
            //for selectd model

            List<ProductModel> listobjmodel = new List<ProductModel>();
            listobjmodel = (from midobj in _db.ProductModels where midobj.Modelid == Mid select midobj).ToList();


            #endregion


            #region----------DDL get Method for Proseri---------
            //for  seri list
            selectseriOptions = _db.ProductSerises.Where(g => g.Isdeleted == false).Select(g => new SelectListItem
            {
                Value = g.Seriseid.ToString(),
                Text = g.SeriseNumber
            }).ToList();

            // for update id 
            if (ProductEnt?.Seriseid > 0)
            {
                var seriResult = _db.ProductSerises.Where(g => g.Seriseid == ProductEnt.Seriseid).Select(g => g).FirstOrDefault();
                selectseriOptions.Insert(0, new SelectListItem()
                {
                    Text = seriResult.SeriseNumber,
                    Value = seriResult.Seriseid.ToString()

                });
            }
            else
            {
                selectseriOptions.Insert(0, new SelectListItem()
                {
                    Text = "----Select----",
                    Value = string.Empty
                });
            }
            //for selectd seri

            List<ProductSerise> listobjseri = new List<ProductSerise>();
            listobjseri = (from seridobj in _db.ProductSerises where seridobj.Seriseid == seriID select seridobj).ToList();


            #endregion


            return Page();
        }

        public IActionResult OnPost()

        {
            try
            {


                if (ProductEnt.ProductEntryid > 0)
                {

                    var duplicatechk = _db.ProductEntries.Where(e => e.Isdeleted == false && e.Id == catID && e.BrandId == BrandID && e.Modelid == Mid && e.Seriseid == seriID && e.ProductName.Trim().ToLower() == ProductEnt.ProductName.Trim().ToLower() && e.ProductEntryid != ProductEnt.ProductEntryid).Count();
                    if (duplicatechk == 0)
                    {


                        var proEnt = _db.ProductEntries.AsNoTracking().Where(p => p.ProductEntryid == ProductEnt.ProductEntryid && p.Isdeleted == false).FirstOrDefault();



                        ProductEnt.Id = catID;
                        ProductEnt.SubCategoryId = SubCategoryID;
                        ProductEnt.BrandId = BrandID;

                        var Categoryinfo = _db.SubCategories.Where(x => x.Isdeleted == false && x.Id == SubCategoryID && x.IsModelOrSerise == true).Count();
                        if (Categoryinfo > 0)
                        {
                            ProductEnt.Modelid = Mid;
                            ProductEnt.Seriseid = seriID;

                        }
                        else
                        {
                            ProductEnt.Modelid = null;
                            ProductEnt.Seriseid = null;
                        }

                        ProductEnt.Isdeleted = proEnt.Isdeleted;

                        _db.ProductEntries.Update(ProductEnt);
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
                    var duplicatechk = _db.ProductEntries.Where(e => e.Isdeleted == false && e.Id == catID && e.BrandId == BrandID && e.Modelid == Mid && e.Seriseid == seriID && e.ProductName.Trim().ToLower() == ProductEnt.ProductName.Trim().ToLower()).Count();
                    if (duplicatechk == 0)
                    {
                        ProductEnt.Id = catID;
                        ProductEnt.SubCategoryId = SubCategoryID;
                        ProductEnt.BrandId = BrandID;

                        var Categoryinfo = _db.SubCategories.Where(x => x.Isdeleted == false && x.Id == SubCategoryID && x.IsModelOrSerise == true).Count();
                        if (Categoryinfo > 0)
                        {
                            ProductEnt.Modelid = Mid;
                            ProductEnt.Seriseid = seriID;
                        }
                        else
                        {
                            ProductEnt.Modelid = null;
                            ProductEnt.Seriseid = null;
                        }
                        ProductEnt.Isdeleted = false;

                        _db.ProductEntries.Add(ProductEnt);
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
            var proEnt = _db.ProductEntries.Where(e => e.ProductEntryid == id && e.Isdeleted == false).FirstOrDefault();
            if (proEnt == null)
            {
                return NotFound();
            }
            proEnt.Isdeleted = true;
            _db.SaveChanges();

            TempData["success"] = "data deleted";
            return RedirectToPage();

        }

    }
}

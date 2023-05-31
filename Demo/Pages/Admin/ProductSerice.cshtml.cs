using Demo.CommonModel;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Demo.Pages.Admin
{
    public class ProductSericeModel : PageModel
    {
        public readonly DemoProjectContext _db;
        public ProductSericeModel(DemoProjectContext db)
        {
            _db = db;
        }


        public IEnumerable<Proseri_Model> ProSeriDatalist { get; set; }
        public List<SelectListItem> SelectBrandOptions { get; set; }
        public List<SelectListItem> SelectModelOptions { get; set; }
        public List<SelectListItem> SelectSubCategoryOption { get; set; }
        public List<SelectListItem> selectProCatOptions { get; set; }



        [BindProperty]
        public int SubCategoryID { get; set; }


        [BindProperty]
        public int catID { get; set; }


        [BindProperty]
        public int seriID { get; set; }




        [BindProperty]
        public int BrandID { get; set; }

        [BindProperty]
        public int Mid { get; set; }

        [BindProperty]
        public ProductSerise ProSerice { get; set; }

        public IActionResult OnGet(int? id)
        {

            ProSeriDatalist = _db.ProductSerises.Where(x => x.Isdeleted == false).Select(x => new Proseri_Model
            {
                modelid = x.Modelid,
                brandid = x.BrandId,
                Seriid = x.Seriseid,
                seirnumber = x.SeriseNumber,

                categoryid = x.ProductCategoryId,
                categoryname = x.ProductCategory.Name,

                subCategoryId = x.SubCategoryId,
                subCategoryname = x.SubCategory.Name,

                modelname = x.Model.ModelName,
                brandname = x.Brand.BrandName
            }).ToList();


            ProSerice = new ProductSerise();

            if (id.HasValue)
            {
                var ProSeri = _db.ProductSerises.Where(s => s.Seriseid == id && s.Isdeleted == false).FirstOrDefault();

                if (ProSeri == null)
                {
                    return NotFound();
                }
                ProSerice = ProSeri;

            }





            #region----------DDL get Method for ProCat---------
            //for  Model list
            selectProCatOptions = _db.ProductCategories.Where(g => g.Isdeleted == false).Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            }).ToList();

            // for update id 
            if (ProSerice?.ProductCategoryId > 0)
            {
                var catResult = _db.ProductCategories.Where(g => g.Id == ProSerice.ProductCategoryId).Select(g => g).FirstOrDefault();
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


            #region----------DDL get Method for SubCategory-------
            //for  brand list
            SelectSubCategoryOption = _db.SubCategories.Where(g => g.Isdeleted == false).Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            }).ToList();

            // for update id 
            if (ProSerice?.SubCategoryId > 0)
            {
                var subcategoriesResult = _db.SubCategories.Where(g => g.Id == ProSerice.SubCategoryId).Select(g => g).FirstOrDefault();
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
            if (ProSerice?.BrandId > 0)
            {
                var BrandResult = _db.ProductBrands.Where(g => g.BrandId == ProSerice.BrandId).Select(g => g).FirstOrDefault();
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
            if (ProSerice?.Modelid > 0)
            {
                var ModelResult = _db.ProductModels.Where(g => g.Modelid == ProSerice.Modelid).Select(g => g).FirstOrDefault();
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


            return Page();
        }


        public IActionResult OnPost()
        {
            try
            {

                if (ProSerice.Seriseid > 0)
                {

                    var duplicatechk = _db.ProductSerises.Where(x => x.Isdeleted == false && x.Modelid == Mid && x.SeriseNumber == ProSerice.SeriseNumber && x.Seriseid != ProSerice.Seriseid).Count();
                    if (duplicatechk == 0)
                    {
                        var Proseri = _db.ProductSerises.AsNoTracking().Where(p => p.Seriseid == ProSerice.Seriseid && p.Isdeleted == false).FirstOrDefault();


                        ProSerice.ProductCategoryId = catID;
                        ProSerice.SubCategoryId = SubCategoryID;
                        ProSerice.BrandId = BrandID;

                        var Categoryinfo = _db.SubCategories.Where(x => x.Isdeleted == false && x.Id == SubCategoryID && x.IsModelOrSerise == true).Count();

                        if (Categoryinfo > 0)
                        {
                            ProSerice.Modelid = Mid;  //duplicatechk -> validation for Mid b/t ProSerice
                            ProSerice.Seriseid = seriID;
                        }

                        else

                        {
                            ProSerice.Modelid = null;
                            ProSerice.Seriseid = 0;


                        }

                        ProSerice.Isdeleted = Proseri.Isdeleted;

                        _db.ProductSerises.Update(ProSerice);
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
                    var duplicatechk = _db.ProductSerises.Where(x => x.Isdeleted == false && x.Modelid == Mid && x.SeriseNumber == ProSerice.SeriseNumber).Count();
                    if (duplicatechk == 0)
                    {
                        ProSerice.ProductCategoryId = catID;
                        ProSerice.SubCategoryId = SubCategoryID;
                        ProSerice.BrandId = BrandID;
                        var Categoryinfo = _db.SubCategories.Where(x => x.Isdeleted == false && x.Id == SubCategoryID && x.IsModelOrSerise == true).Count();

                        if (Categoryinfo > 0)
                        {
                            ProSerice.Modelid = Mid;
                            ProSerice.Seriseid = seriID;
                        }

                        else

                        {
                            ProSerice.Modelid = null;
                            ProSerice.Seriseid = 0;


                        }

                        ProSerice.Isdeleted = false;

                        _db.ProductSerises.Add(ProSerice);
                        _db.SaveChanges();
                        TempData["success"] = "data saved";
                    }

                    else
                    {
                        TempData["error"] = "Duplicate entry not allowed";

                    }

                }
            }
            catch (Exception)
            {
                TempData["error"] = "Something went wrong";
                return RedirectToPage();
            }
            return RedirectToPage();
        }



        public IActionResult OnPostDelete(int? id)
        {
            var ProSerice = _db.ProductSerises.Where(e => e.Seriseid == id && e.Isdeleted == false).FirstOrDefault();
            if (ProSerice == null)
            {
                return NotFound();
            }
            ProSerice.Isdeleted = true;
            _db.SaveChanges();

            TempData["success"] = "data deleted";
            return RedirectToPage();

        }

    }
}


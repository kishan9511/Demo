using Demo.CommonModel;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Demo.Pages.Admin
{
    public class ProductColorModel : PageModel
    {
        public readonly DemoProjectContext _db;
        public ProductColorModel(DemoProjectContext db)
        {
            _db = db;
        }


        public IEnumerable<ProductColor_Model> DatalistProductColor { get; set; }
        public List<SelectListItem> SelectEntryOptions { get; set; }
        public List<SelectListItem> selectColorOptions { get; set; }

        [BindProperty]
        public int ProductEntryID { get; set; }

        [BindProperty]
        public int ColorID { get; set; }

        [BindProperty]
        public ProductColor ColorProduct { get; set; }
        public IActionResult OnGet(int? id)
        {
            DatalistProductColor = _db.ProductColors.Where(x => x.Isdeleted == false).Select(x => new ProductColor_Model
            {
                productcoloreid = x.Productcolorid,
                colorid = x.Colorid,
                productEntryid = x.ProductEntryid,
                productname = x.ProductEntry.ProductName,
                colorname = x.Color.ColorName,

            }).ToList();

            ColorProduct = new ProductColor();

            if (id.HasValue)
            {
                var pc = _db.ProductColors.Where(x => x.Productcolorid == id && x.Isdeleted == false).FirstOrDefault();

                if (pc == null)
                {
                    return Page();
                }
                ColorProduct = pc;
            }


            #region----------DDL get Method for Entry---------
            //for  Entry list
            SelectEntryOptions = _db.ProductEntries.Where(g => g.Isdeleted == false).Select(g => new SelectListItem
            {
                Value = g.ProductEntryid.ToString(),
                Text = g.ProductName
            }).ToList();

            // for update id 
            if (ColorProduct.ProductEntryid > 0)
            {
                var EntryResult = _db.ProductEntries.Where(g => g.ProductEntryid == ColorProduct.ProductEntryid).Select(g => g).FirstOrDefault();
                SelectEntryOptions.Insert(0, new SelectListItem()
                {
                    Text = EntryResult?.ProductName,
                    Value = EntryResult?.ProductEntryid.ToString()

                });
            }
            else
            {
                SelectEntryOptions.Insert(0, new SelectListItem()
                {
                    Text = "----Select----",
                    Value = string.Empty
                });
            }
            //for selectd entry

            List<ProductEntry> listobjEntry = new List<ProductEntry>();
            listobjEntry = (from entryidobj in _db.ProductEntries where entryidobj.ProductEntryid == ProductEntryID select entryidobj).ToList();







            #endregion



            #region----------DDL get Method for color---------
            //for  color list
            selectColorOptions = _db.Colors.Where(g => g.Isdeleted == false).Select(g => new SelectListItem
            {
                Value = g.Colorid.ToString(),
                Text = g.ColorName
            }).ToList();

            // for update id 
            if (ColorProduct.Colorid > 0)
            {
                var ColorResult = _db.Colors.Where(g => g.Colorid == ColorProduct.Colorid).Select(g => g).FirstOrDefault();
                selectColorOptions.Insert(0, new SelectListItem()
                {
                    Text = ColorResult.ColorName,
                    Value = ColorResult.Colorid.ToString()

                });
            }
            else
            {
                selectColorOptions.Insert(0, new SelectListItem()
                {
                    Text = "----Select----",
                    Value = string.Empty
                });
            }
            //for selectd color

            List<Color> listobjcolor = new List<Color>();
            listobjcolor = (from coloridobj in _db.Colors where coloridobj.Colorid == ColorID select coloridobj).ToList();


            #endregion

            return Page();
        }

        public IActionResult OnPost()
        {
            try
            {


                if (ColorProduct.Productcolorid > 0)
                {
                    var duplicatechk = _db.ProductColors.Where(c => c.Isdeleted == false && c.ProductEntryid == ColorProduct.ProductEntryid && ProductEntryID != ColorProduct.Productcolorid).Count();
                    if (duplicatechk == 0)
                    {
                        var color = _db.ProductColors.AsNoTracking().Where(p => p.Productcolorid == ColorProduct.Productcolorid && p.Isdeleted == false).FirstOrDefault();

                        ColorProduct.ProductEntryid = ProductEntryID;
                        ColorProduct.Colorid = ColorID;

                        ColorProduct.Isdeleted = color.Isdeleted;

                        _db.ProductColors.Update(ColorProduct);
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
                    var duplicatechk = _db.ProductColors.Where(c => c.Isdeleted == false && c.ProductEntryid == ProductEntryID && c.Colorid == ColorID).Count();

                    if (duplicatechk == 0)
                    {
                        ColorProduct.ProductEntryid = ProductEntryID;
                        ColorProduct.Colorid = ColorID;

                        ColorProduct.Isdeleted = false;

                        _db.ProductColors.Add(ColorProduct);
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
            var color = _db.ProductColors.Where(e => e.Productcolorid == id && e.Isdeleted == false).FirstOrDefault();
            if (color == null)
            {
                return NotFound();
            }
            color.Isdeleted = true;
            _db.SaveChanges();

            TempData["success"] = "data deleted";
            return RedirectToPage();

        }



    }
}

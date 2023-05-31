using Demo.CommonModel;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Demo.Pages.Admin
{
    public class ProductSizeModel : PageModel
    {
        public readonly DemoProjectContext _db;
        public ProductSizeModel(DemoProjectContext db)
        {
            _db = db;
        }

        public IEnumerable<ProductSize_Model> DatalistProductSize { get; set; }


        [BindProperty]
        public int ProductEntryID { get; set; }

        public List<SelectListItem> SelectEntryOptions { get; set; }


        [BindProperty]
        public int SizeID { get; set; }




        public List<SelectListItem> selectSizeOptions { get; set; }

        [BindProperty]
        public ProductSize SizeProduct { get; set; }



        public IActionResult OnGet(int? id)
        {
            DatalistProductSize = _db.ProductSizes.Where(x => x.Isdeleted == false).Select(x => new ProductSize_Model
            {
                productsizeid = x.ProductSizeid,
                sizeid = x.Sizeid,
                productEntryid = x.ProductEntryid,

                sizename = x.Size.SizeName,
                productname = x.ProductEntry.ProductName
            }).ToList();

            SizeProduct = new ProductSize();

            if (id.HasValue)
            {
                var ps = _db.ProductSizes.Where(x => x.ProductSizeid == id && x.Isdeleted == false).FirstOrDefault();

                if (ps == null)
                {
                    return Page();
                }
                SizeProduct = ps;
            }



            #region----------DDL get Method for Entry---------
            //for  Entry list
            SelectEntryOptions = _db.ProductEntries.Where(g => g.Isdeleted == false).Select(g => new SelectListItem
            {
                Value = g.ProductEntryid.ToString(),
                Text = g.ProductName
            }).ToList();

            // for update id 
            if (SizeProduct.ProductEntryid > 0)
            {
                var EntryResult = _db.ProductEntries.Where(g => g.ProductEntryid == SizeProduct.ProductEntryid).Select(g => g).FirstOrDefault();
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
            //for selectd Entry

            List<ProductEntry> listobjEntry = new List<ProductEntry>();
            listobjEntry = (from entryidobj in _db.ProductEntries where entryidobj.ProductEntryid == ProductEntryID select entryidobj).ToList();







            #endregion




            #region----------DDL get Method for size---------
            //for  size list
            selectSizeOptions = _db.Sizes.Where(g => g.Isdeleted == false).Select(g => new SelectListItem
            {
                Value = g.Sizeid.ToString(),
                Text = g.SizeName
            }).ToList();

            // for update id 
            if (SizeProduct.Sizeid > 0)
            {
                var sizeResult = _db.Sizes.Where(g => g.Sizeid == SizeProduct.Sizeid).Select(g => g).FirstOrDefault();
                selectSizeOptions.Insert(0, new SelectListItem()
                {
                    Text = sizeResult.SizeName,
                    Value = sizeResult.Sizeid.ToString()

                });
            }
            else
            {
                selectSizeOptions.Insert(0, new SelectListItem()
                {
                    Text = "----Select----",
                    Value = string.Empty
                });
            }
            //for selectd size

            List<Size> listobjSize = new List<Size>();
            listobjSize = (from sizeidobj in _db.Sizes where sizeidobj.Sizeid == SizeID select sizeidobj).ToList();


            #endregion

            return Page();
        }

        public IActionResult OnPost()
        {
            try
            {


                if (SizeProduct.ProductSizeid > 0)
                {
                    var duplicatechk = _db.ProductSizes.Where(s => s.Isdeleted == false && s.ProductEntryid == SizeProduct.ProductEntryid && ProductEntryID != SizeProduct.ProductSizeid).Count();
                    if (duplicatechk == 0)
                    {

                        var Proentry = _db.ProductSizes.AsNoTracking().Where(p => p.ProductSizeid == SizeProduct.ProductSizeid && p.Isdeleted == false).FirstOrDefault();




                        SizeProduct.ProductEntryid = ProductEntryID;
                        SizeProduct.Sizeid = SizeID;

                        SizeProduct.Isdeleted = Proentry.Isdeleted;

                        _db.ProductSizes.Update(SizeProduct);
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
                    var duplicatechk = _db.ProductSizes.Where(s => s.Isdeleted == false && s.ProductEntryid == ProductEntryID).Count();

                    if (duplicatechk == 0)
                    {

                        SizeProduct.ProductEntryid = ProductEntryID;
                        SizeProduct.Sizeid = SizeID;

                        SizeProduct.Isdeleted = false;

                        _db.ProductSizes.Add(SizeProduct);
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
            var Proentry = _db.ProductSizes.Where(e => e.ProductSizeid == id && e.Isdeleted == false).FirstOrDefault();
            if (Proentry == null)
            {
                return NotFound();
            }
            Proentry.Isdeleted = true;
            _db.SaveChanges();

            TempData["success"] = "data deleted";
            return RedirectToPage();

        }
    }
}

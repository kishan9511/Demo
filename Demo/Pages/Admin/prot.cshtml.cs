using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Demo.Pages.Admin
{
    public class protModel : PageModel
    {
        public readonly DemoProjectContext _db;
        public protModel(DemoProjectContext db)
        {
            _db = db;
        }

        public IEnumerable<ProductCategory> datalist { get; set; }

        [BindProperty]
        public ProductCategory proCat { get; set; }

        public IActionResult OnGet(int? id)
        {
            datalist = _db.ProductCategories.Where(p => p.Isdeleted == false).ToList();
            if (id.HasValue)
            {
                var po = _db.ProductCategories.Where(a => a.Id == id && a.Isdeleted == false).FirstOrDefault();

                if (po == null)
                {
                    return NotFound();
                }
                proCat = po;
                return Page();
            }
            proCat = new ProductCategory();
            return Page();
        }
        public IActionResult OnPost()
        {
            try
            {


                if (proCat.Id > 0)
                {
                    var duplicatechk = _db.ProductCategories.Where(x => x.Isdeleted == false && x.Name.Trim().ToLower() == proCat.Name.Trim().ToLower() && x.Id != proCat.Id).Count();

                    if (duplicatechk == 0)
                    {
                        var pro = _db.ProductCategories.AsNoTracking().Where(c => proCat.Id == proCat.Id && c.Isdeleted == false).FirstOrDefault();


                        proCat.Isdeleted = pro.Isdeleted;


                        _db.ProductCategories.Update(proCat);
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

                    var duplicatechk = _db.ProductCategories.Where(x => x.Isdeleted == false && x.Name.Trim().ToLower() == proCat.Name.Trim().ToLower()).Count();

                    if (duplicatechk == 0)
                    {
                        proCat.Isdeleted = false;


                        _db.ProductCategories.Add(proCat);
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
            var po = _db.ProductCategories.Where(e => e.Id == id && e.Isdeleted == false).FirstOrDefault();
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

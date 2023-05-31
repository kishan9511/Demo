using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Demo.Pages.Admin
{
    public class SizeModel : PageModel
    {
        public readonly DemoProjectContext _db;
        public SizeModel(DemoProjectContext db)
        {
            _db = db;
        }
        public IEnumerable<Size> sizeDatalist { get; set; }

        [BindProperty]
        public Size sizee { get; set; }

        public IActionResult OnGet(int? id)
        {
            sizeDatalist = _db.Sizes.Where(s => s.Isdeleted == false).ToList();
            if (id.HasValue)
            {
                var Sz = _db.Sizes.Where(s => s.Sizeid == id && s.Isdeleted == false).FirstOrDefault();
                if (Sz == null)
                {
                    return NotFound();
                }
                sizee = Sz;
                return Page();
            }
            sizee = new Size();
            return Page();
        }

        public IActionResult OnPost()

        {
            try
            {


                if (sizee.Sizeid > 0)
                {
                    var duplicatechk = _db.Sizes.Where(s => s.Isdeleted == false && s.SizeName.Trim().ToLower() == sizee.SizeName.Trim().ToLower() && s.Sizeid != sizee.Sizeid).Count();
                    if (duplicatechk == 0)
                    {
                        var po = _db.Sizes.AsNoTracking().Where(c => c.Sizeid == sizee.Sizeid && c.Isdeleted == false).FirstOrDefault();

                        sizee.Isdeleted = po.Isdeleted;


                        _db.Sizes.Update(sizee);
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
                    var duplicatechk = _db.Sizes.Where(s => s.Isdeleted == false && s.SizeName.Trim().ToLower() == sizee.SizeName.Trim().ToLower()).Count();
                    if (duplicatechk == 0)
                    {
                        sizee.Isdeleted = false;
                        _db.Sizes.Add(sizee);
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
            var sizee = _db.Sizes.Where(e => e.Sizeid == id && e.Isdeleted == false).FirstOrDefault();
            if (sizee == null)
            {
                return NotFound();
            }
            sizee.Isdeleted = true;
            _db.SaveChanges();

            TempData["success"] = "data deleted";
            return RedirectToPage();

        }




    }

}



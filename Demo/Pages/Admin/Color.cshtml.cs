using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Demo.Pages.Admin
{
    public class ColorModel : PageModel
    {
        public readonly DemoProjectContext _db;
        public ColorModel(DemoProjectContext db)
        {
            _db = db;
        }
        public IEnumerable<Color> ColorDatalist { get; set; }

        [BindProperty]
        public Color colorrr { get; set; }
        public IActionResult OnGet(int? id)
        {


            ColorDatalist = _db.Colors.Where(s => s.Isdeleted == false).ToList();
            if (id.HasValue)
            {
                var colo = _db.Colors.Where(s => s.Colorid == id && s.Isdeleted == false).FirstOrDefault();
                if (colo == null)
                {
                    return NotFound();
                }
                colorrr = colo;
                return Page();
            }
            colorrr = new Color();
            return Page();
        }


        public IActionResult OnPost()

        {
            try
            {


                if (colorrr.Colorid > 0)
                {

                    var duplicatechk = _db.Colors.Where(c => c.Isdeleted == false && c.ColorName.Trim().ToLower() == colorrr.ColorName.Trim().ToLower() && c.Colorid != colorrr.Colorid).Count();
                    if (duplicatechk == 0)
                    {


                        var colo = _db.Colors.AsNoTracking().Where(c => c.Colorid == colorrr.Colorid && c.Isdeleted == false).FirstOrDefault();

                        colorrr.Isdeleted = colo.Isdeleted;


                        _db.Colors.Update(colorrr);
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

                    var duplicatechk = _db.Colors.Where(c => c.Isdeleted == false && c.ColorName.Trim().ToLower() == colorrr.ColorName.Trim().ToLower()).Count();
                    if (duplicatechk == 0)
                    {


                        colorrr.Isdeleted = false;
                        _db.Colors.Add(colorrr);
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
            var colorrr = _db.Colors.Where(e => e.Colorid == id && e.Isdeleted == false).FirstOrDefault();
            if (colorrr == null)
            {
                return NotFound();
            }
            colorrr.Isdeleted = true;
            _db.SaveChanges();

            TempData["success"] = "data deleted";
            return RedirectToPage();

        }




    }
}

using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Demo.Pages.Admin
{
    public class HomeinfoModel : PageModel
    {
        public readonly DemoProjectContext _db;

        public HomeinfoModel(DemoProjectContext db)
        {
            _db = db;

        }

        public IEnumerable<Frui> fruiDatalist { get; set; }



        [BindProperty]
        public Frui frui { get; set; }



        public IActionResult OnGet(int? id)
        {
            fruiDatalist = _db.Fruis.Where(p => p.Isdeleted == false).ToList();
            if (id.HasValue)
            {
                var newss = _db.Fruis.Where(p => p.Id == id && p.Isdeleted == false).FirstOrDefault();
                if (newss == null)
                {
                    return Page();
                }
                frui = newss;
                return Page();
            }
            frui = new Frui();
            return Page();

        }



        public IActionResult OnPost()

        {
            if (frui.Id > 0)
            {
                var newss = _db.Fruis.AsNoTracking().Where(p => p.Id == frui.Id && p.Isdeleted == false).FirstOrDefault();
                if (newss == null)
                {
                    return Page();

                }




                frui.Isdeleted = newss.Isdeleted;

                _db.Fruis.Update(frui);
                _db.SaveChanges();
                TempData["info"] = "Your Data update Successfully";
            }

            else


            {

                frui.Isdeleted = false;

                _db.Fruis.Add(frui);
                _db.SaveChanges();

                TempData["success"] = "data saved";
            }
            return RedirectToPage();

        }



        public IActionResult OnPostDelete(int? id)

        {
            var newss = _db.Fruis.Where(p => p.Id == id && p.Isdeleted == false).FirstOrDefault();
            if (newss == null)
            {
                return Page();
            }

            newss.Isdeleted = true;
            _db.SaveChanges();
            TempData["success"] = "data deleted";

            return RedirectToPage();
        }
    }
}

using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Demo.Pages.Admin
{
    public class generalNewsModel : PageModel
    {
        public readonly DemoProjectContext _db;

        public generalNewsModel(DemoProjectContext db)
        {
            _db = db;

        }

        public IEnumerable<GeneralNews> generalNewsDatalist { get; set; }



        [BindProperty]
        public GeneralNews gnews { get; set; }



        public IActionResult OnGet(int? id)
        {
            generalNewsDatalist = _db.GeneralNews.Where(p => p.Isdeleted == false).ToList();
            if (id.HasValue)
            {
                var newss = _db.GeneralNews.Where(p => p.Id == id && p.Isdeleted == false).FirstOrDefault();
                if (newss == null)
                {
                    return Page();
                }
                gnews = newss;
                return Page();
            }
            gnews = new GeneralNews();
            return Page();

        }



        public IActionResult OnPost()

        {
            if (gnews.Id > 0)
            {
                var newss = _db.GeneralNews.AsNoTracking().Where(p => p.Id == gnews.Id && p.Isdeleted == false).FirstOrDefault();
                if (newss == null)
                {
                    return Page();

                }




                gnews.Isdeleted = newss.Isdeleted;

                _db.GeneralNews.Update(gnews);
                _db.SaveChanges();
                TempData["info"] = "Your Data update Successfully";
            }

            else


            {

                gnews.Isdeleted = false;

                _db.GeneralNews.Add(gnews);
                _db.SaveChanges();

                TempData["success"] = "data saved";
            }
            return RedirectToPage();

        }



        public IActionResult OnPostDelete(int? id)

        {
            var newss = _db.GeneralNews.Where(p => p.Id == id && p.Isdeleted == false).FirstOrDefault();
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

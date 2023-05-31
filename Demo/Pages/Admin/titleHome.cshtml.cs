using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Demo.Pages.Admin
{

    public class titleHomeModel : PageModel
    {
        public readonly DemoProjectContext _db;
        public titleHomeModel(DemoProjectContext db)
        {
            _db = db;
        }



        public IEnumerable<ProductTitle> titleDatalist { get; set; }

        [BindProperty]
        public ProductTitle title { get; set; }


        public IActionResult OnGet(int? id)
        {
            titleDatalist = _db.ProductTitles.Where(p => p.Isdeleted == false).ToList();
            if (id.HasValue)
            {
                var titlee = _db.ProductTitles.Where(p => p.Id == id && p.Isdeleted == false).FirstOrDefault();
                if (titlee == null)
                {
                    return Page();
                }
                title = titlee;
                return Page();
            }

            title = new ProductTitle();

            return Page();
        }


        public IActionResult OnPost()
        {
            if (title.Id > 0)
            {
                var titlee = _db.ProductTitles.AsNoTracking().Where(p => p.Id == title.Id && p.Isdeleted == false).FirstOrDefault();
                if (titlee == null)
                {
                    return Page();
                }
                title.Isdeleted = titlee.Isdeleted;

                _db.ProductTitles.Update(title);
                _db.SaveChanges();
                TempData["info"] = "Your Data update Successfully";
            }


            else
            {
                title.Isdeleted = false;

                _db.ProductTitles.Add(title);
                _db.SaveChanges();
                TempData["success"] = "data saved";
            }

            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int? id)
        {
            var titlee = _db.ProductTitles.Where(p => p.Id == id && p.Isdeleted == false).FirstOrDefault();

            if (titlee == null)
            {
                return Page();
            }

            titlee.Isdeleted = true;
            _db.SaveChanges();
            TempData["success"] = "data deleted";


            return RedirectToPage();
        }


    }
}

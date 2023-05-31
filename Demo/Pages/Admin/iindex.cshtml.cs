using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Demo.Pages.Admin
{
    public class iindexModel : PageModel
    {
        public readonly DemoProjectContext _db;
        public iindexModel(DemoProjectContext db)
        {
            _db = db;
        }

        public IEnumerable<IndexHome> indexDatalist { get; set; }

        [BindProperty]
        public IndexHome index { get; set; }

        public IActionResult OnGet(int? id)
        {

            indexDatalist = _db.IndexHomes.Where(i => i.Isdeleted == false).ToList();
            if (id.HasValue)
            {
                var idx = _db.IndexHomes.Where(i => i.Id == id && i.Isdeleted == false).FirstOrDefault();
                if (idx == null)
                {
                    return Page();
                }
                index = idx;
                return Page();
            }
            index = new IndexHome();

            return Page();
        }


        public IActionResult OnPost()
        {
            if (index.Id > 0)
            {
                var idx = _db.IndexHomes.AsNoTracking().Where(i => i.Id == index.Id && i.Isdeleted == false).FirstOrDefault();
                if (idx == null)
                {
                    return Page();

                }
                index.Isdeleted = idx.Isdeleted;

                _db.IndexHomes.Update(index);
                _db.SaveChanges();
                TempData["info"] = "Your Data update Successfully";

            }
            else
            {
                index.Isdeleted = false;

                _db.IndexHomes.Add(index);
                _db.SaveChanges();
                TempData["success"] = "data saved";
            }

            return RedirectToPage();
        }


        public IActionResult OnPostDelete(int? id)
        {
            var idxx = _db.IndexHomes.Where(i => i.Id == id && i.Isdeleted == false).FirstOrDefault();
            if (idxx == null)
            {
                return Page();
            }
            idxx.Isdeleted = true;
            _db.SaveChanges();

            TempData["success"] = "data deleted";
            return RedirectToPage();
        }

    }
}

using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Demo.Pages.Admin
{
    public class AdminLoginModel : PageModel
    {
        public readonly DemoProjectContext _db;
        public AdminLoginModel(DemoProjectContext db)
        {
            _db = db;
        }
        public IEnumerable<AdminLogin> logindatalist { get; set; }



        [BindProperty]
        public AdminLogin Alogin { get; set; }
        public IActionResult OnGet(int? id)
        {

            logindatalist = _db.AdminLogins.Where(l => l.Isdeleted == false).ToList();

            if (id.HasValue)
            {
                var loginn = _db.AdminLogins.Where(l => l.Id == id && l.Isdeleted == false).FirstOrDefault();
                if (loginn == null)
                {
                    return Page();
                }
                Alogin = loginn;
                return Page();
            }

            Alogin = new AdminLogin();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (Alogin.Id > 0)
            {
                var log = _db.AdminLogins.AsNoTracking().Where(l => l.Id == Alogin.Id && l.Isdeleted == false).FirstOrDefault();
                if (log == null)
                {
                    return Page();
                }

                Alogin.Isdeleted = log.Isdeleted;
                _db.AdminLogins.Update(Alogin);
                _db.SaveChanges();
                TempData["info"] = "Your Data update Successfully";

            }
            else
            {
                Alogin.Isdeleted = false;
                _db.AdminLogins.Add(Alogin);
                _db.SaveChanges();
                TempData["success"] = "data saved";

            }
            return RedirectToPage();

        }

        public IActionResult OnPostDelete(int? id)
        {
            var log = _db.AdminLogins.Where(o => o.Id == id && o.Isdeleted == false).FirstOrDefault();
            if (log == null)
            {
                return Page();
            }
            log.Isdeleted = true;

            _db.SaveChanges();


            TempData["success"] = "data deleted";
            return RedirectToPage();


        }
    }
}

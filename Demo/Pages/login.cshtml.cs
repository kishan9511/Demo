using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Demo.Pages
{
    public class loginModel : PageModel
    {
        public readonly DemoProjectContext _db;
        public loginModel(DemoProjectContext db)
        {
            _db = db;
        }
        public IEnumerable<Login> logindatalist { get; set; }

        [BindProperty]
        public Login login { get; set; }
        public IActionResult OnGet(int? id)
        {

            logindatalist = _db.Logins.Where(l => l.Isdeleted == false).ToList();

            if (id.HasValue)
            {
                var loginn = _db.Logins.Where(l => l.Id == id && l.Isdeleted == false).FirstOrDefault();
                if (loginn == null)
                {
                    return Page();
                }
                login = loginn;
                return Page();
            }

            login = new Login();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (login.Id > 0)
            {
                var log = _db.Logins.AsNoTracking().Where(l => l.Id == login.Id && l.Isdeleted == false).FirstOrDefault();
                if (log == null)
                {
                    return Page();
                }

                login.Isdeleted = log.Isdeleted;
                _db.Logins.Update(login);
                _db.SaveChanges();
                TempData["info"] = "Your Data update Successfully";

            }
            else
            {
                login.Isdeleted = false;
                _db.Logins.Add(login);
                _db.SaveChanges();
                TempData["success"] = "data saved";

            }
            return RedirectToPage();
        }


        public IActionResult OnPostDelete(int? id)
        {
            var log = _db.Logins.Where(o => o.Id == id && o.Isdeleted == false).FirstOrDefault();
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

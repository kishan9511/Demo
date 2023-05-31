using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Demo.Pages.Admin
{
    public class contactModel : PageModel
    {
        public readonly DemoProjectContext _db;
        public contactModel(DemoProjectContext db)
        {
            _db = db;
        }
        public IEnumerable<Contactu> contactDatalist { get; set; }




        [BindProperty]
        public Contactu Contact { get; set; }

        public IActionResult OnGet(int? id)
        {




            contactDatalist = _db.Contactus.Where(c => c.Isdeleted == false).ToList();
            if (id.HasValue)
            {
                var contactinfo = _db.Contactus.Where(c => c.Id == id && c.Isdeleted == false).FirstOrDefault();
                if (contactinfo == null)
                {
                    return Page();
                }
                Contact = contactinfo;
                return Page();
            }
            Contact = new Contactu();






            return Page();
        }

        public IActionResult OnPost()
        {

            if (Contact.Id > 0)
            {
                var contactinfo = _db.Contactus.AsNoTracking().Where(c => c.Id == Contact.Id && c.Isdeleted == false).FirstOrDefault();
                if (contactinfo == null)
                {
                    return Page();
                }


                Contact.Isdeleted = contactinfo.Isdeleted;

                _db.Contactus.Update(Contact);
                _db.SaveChanges();
                TempData["info"] = "Your Data update Successfully";
            }

            else
            {
                Contact.Isdeleted = false;

                _db.Contactus.Add(Contact);
                _db.SaveChanges();
                TempData["success"] = "data saved";
            }

            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int? id)
        {
            var contactinfo = _db.Contactus.Where(c => c.Id == id && c.Isdeleted == false).FirstOrDefault();
            if (contactinfo == null)
            {
                return NotFound();
            }
            contactinfo.Isdeleted = true;

            _db.SaveChanges();
            TempData["success"] = "data deleted";

            return RedirectToPage();
        }
    }
}

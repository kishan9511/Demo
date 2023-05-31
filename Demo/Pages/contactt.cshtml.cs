using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo.Pages
{
    public class contacttModel : PageModel
    {
        public readonly DemoProjectContext _db;
        public contacttModel(DemoProjectContext db)
        {
            _db = db;
        }
        public IEnumerable<Contactu> contacttDatalist { get; set; }

        public IEnumerable<About> aboutlist { get; set; }

        [BindProperty]
        public About contactobj { get; set; }


        [BindProperty]
        public Contactu fruitkha { get; set; }

        public IActionResult OnGet(int? id)
        {

            contactobj = _db.Abouts.FirstOrDefault(x => x.Isdeleted == false);

            aboutlist = _db.Abouts.Where(a => a.Isdeleted == false).ToList();


            contacttDatalist = _db.Contactus.Where(c => c.Isdeleted == false).ToList();

            if (id.HasValue)
            {

                var contact = _db.Contactus.Where(c => c.Id == id && c.Isdeleted == false).FirstOrDefault();

                if (contact == null)
                {
                    return Page();
                }
                fruitkha = contact;
                return Page();
            }
            fruitkha = new Contactu();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (fruitkha.Id > 0)
            {
                var contact = _db.Contactus.Where(c => c.Id == fruitkha.Id && c.Isdeleted == false).FirstOrDefault();
                if (contact == null)
                {
                    return Page();
                }


                fruitkha.Isdeleted = contact.Isdeleted;

                _db.Contactus.Update(fruitkha);
                _db.SaveChanges();
            }

            else
            {
                fruitkha.Isdeleted = false;

                _db.Contactus.Add(fruitkha);
                _db.SaveChanges();
            }

            return RedirectToPage();
        }

    }
}

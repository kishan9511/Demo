using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Demo.Pages.Admin
{
    public class DealModel : PageModel
    {
        public readonly DemoProjectContext _db;
        public readonly IHostEnvironment _hostEnvironment;

        public DealModel(DemoProjectContext db, IHostEnvironment hostEnvironment_environment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment_environment;
        }


        public IEnumerable<Deal> dealDatalist { get; set; }



        [BindProperty]
        public IFormFile Imagee { get; set; }


        [BindProperty]
        public Deal deal { get; set; }



        public IActionResult OnGet(int? id)
        {
            dealDatalist = _db.Deals.Where(p => p.Isdeleted == false).ToList();
            if (id.HasValue)
            {
                var del = _db.Deals.Where(p => p.Id == id && p.Isdeleted == false).FirstOrDefault();
                if (del == null)
                {
                    return Page();
                }
                deal = del;
                return Page();
            }
            deal = new Deal();
            return Page();

        }



        public IActionResult OnPost()

        {
            if (deal.Id > 0)
            {
                var del = _db.Deals.AsNoTracking().Where(p => p.Id == deal.Id && p.Isdeleted == false).FirstOrDefault();
                if (del == null)
                {
                    return Page();

                }

                if (Imagee != null)
                {
                    deal.Image = ProcessUploadedFile();
                }
                else
                {
                    deal.Image = del.Image;
                }



                deal.Isdeleted = del.Isdeleted;

                _db.Deals.Update(deal);
                _db.SaveChanges();
                TempData["info"] = "Your Data update Successfully";
            }

            else


            {

                if (Imagee != null)
                {
                    deal.Image = ProcessUploadedFile();
                }
                else
                {
                    deal.Image = "noimage.webp";
                }



                deal.Isdeleted = false;

                _db.Deals.Add(deal);
                _db.SaveChanges();

                TempData["success"] = "data saved";
            }
            return RedirectToPage();

        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;

            if (Imagee != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot\\Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Imagee.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Imagee.CopyTo(fileStream);
                }
            }
            return uniqueFileName;

        }


        public IActionResult OnPostDelete(int? id)

        {
            var del = _db.Deals.Where(p => p.Id == id && p.Isdeleted == false).FirstOrDefault();
            if (del == null)
            {
                return Page();
            }

            del.Isdeleted = true;
            _db.SaveChanges();
            TempData["success"] = "data deleted";

            return RedirectToPage();
        }







    }
}

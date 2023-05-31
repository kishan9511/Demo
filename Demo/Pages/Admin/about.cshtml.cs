using Demo.CommonModel;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Demo.Pages.Admin
{
    public class aboutModel : PageModel
    {
        public readonly DemoProjectContext _db;

        public readonly IHostEnvironment _hostEnvironment;
        public aboutModel(DemoProjectContext db, IHostEnvironment hostEnvironment_environment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment_environment;
        }



        public IEnumerable<About> aboutDatalist { get; set; }


        [BindProperty]
        public IFormFile Imagee { get; set; }


        [BindProperty]
        public About about { get; set; }



        public IActionResult OnGet(int? id)
        {
            aboutDatalist = _db.Abouts.Where(a => a.Isdeleted == false).ToList();
            if (id.HasValue)
            {
                var aboutt = _db.Abouts.Where(a => a.Id == id && a.Isdeleted == false).FirstOrDefault();
                if (aboutt == null)
                {
                    return Page();
                }
                about = aboutt;
                return Page();
            }
            about = new About();
            return Page();
        }


        public IActionResult OnPost()
        {
            if (about.Id > 0)
            {
                var aboutt = _db.Abouts.AsNoTracking().Where(a => a.Id == about.Id && a.Isdeleted == false).FirstOrDefault();

                if (aboutt == null)
                {
                    return Page();
                }

                if (Imagee != null)
                {
                    about.ServiceImage = ProcessUploadedFile();
                }

                else
                {
                    about.ServiceImage = aboutt.ServiceImage;
                }

                about.Isdeleted = aboutt.Isdeleted;

                _db.Abouts.Update(about);
                _db.SaveChanges();
                TempData["info"] = "Your Data update Successfully";
            }


            else
            {

                if (Imagee != null)
                {
                    about.ServiceImage = ProcessUploadedFile();
                }
                else
                {
                    about.ServiceImage = "noimage.webp";
                }

                about.Isdeleted = false;
                _db.Abouts.Add(about);
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
            var aboutt = _db.Abouts.Where(c => c.Id == id && c.Isdeleted == false).FirstOrDefault();
            if (aboutt == null)
            {
                return NotFound();
            }
            aboutt.Isdeleted = true;

            _db.SaveChanges();
            TempData["success"] = "data deleted";

            return RedirectToPage();
        }
    }
}

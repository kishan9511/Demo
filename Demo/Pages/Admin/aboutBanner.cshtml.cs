using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using static System.Net.Mime.MediaTypeNames;

namespace Demo.Pages.Admin
{
    public class aboutBannerModel : PageModel
    {
        public readonly DemoProjectContext _db;
        public readonly IHostEnvironment _hostEnvironment;


        public aboutBannerModel(DemoProjectContext db, IHostEnvironment hostEnvironment_environment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment_environment;
        }

        public IEnumerable<AboutBanner> bannerDatalist { get; set; }


        [BindProperty]
        public IFormFile Imagee { get; set; }


        [BindProperty]
        public AboutBanner banner { get; set; }

        public IActionResult OnGet(int? id)
        {

            bannerDatalist = _db.AboutBanners.Where(b => b.Isdeleted == false).ToList();
            if (id.HasValue)
            {
                var bnr = _db.AboutBanners.Where(b => b.Id == id && b.Isdeleted == false).FirstOrDefault();
                if (bnr == null)
                {
                    return Page();
                }
                banner = bnr;
                return Page();
            }
            banner = new AboutBanner();
            return Page();
        }





        public IActionResult OnPost()
        {
            if (banner.Id > 0)
            {
                var bnr = _db.AboutBanners.AsNoTracking().Where(b => b.Id == banner.Id && b.Isdeleted == false).FirstOrDefault();
                if (bnr == null)
                {
                    return Page();
                }


                if (Imagee != null)
                {
                    banner.Image = ProcessUploadedFile();
                }
                else
                {
                    banner.Image = bnr.Image;
                }



                banner.Isdeleted = bnr.Isdeleted;
                _db.AboutBanners.Update(banner);
                _db.SaveChanges();
                TempData["info"] = "Your Data update Successfully";
            }
            else
            {

                if (Imagee != null)
                {
                    banner.Image = ProcessUploadedFile();
                }
                else
                {
                    banner.Image = "noimage.webp";
                }





                banner.Isdeleted = false;
                _db.AboutBanners.Add(banner);
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
            var bnr = _db.AboutBanners.Where(b => b.Id == id && b.Isdeleted == false).FirstOrDefault();
            if (bnr == null)
            {
                return NotFound();
            }
            bnr.Isdeleted = true;

            _db.SaveChanges();

            TempData["success"] = "data deleted";
            return RedirectToPage();
        }


    }
}

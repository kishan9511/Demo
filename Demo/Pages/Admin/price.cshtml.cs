using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Demo.Pages.Admin
{
    public class priceModel : PageModel
    {


        public readonly DemoProjectContext _db;
        public readonly IHostEnvironment _hostEnvironment;

        public priceModel(DemoProjectContext db, IHostEnvironment hostEnvironment_environment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment_environment;
        }


        public IEnumerable<Product> homeProductDatalist { get; set; }



        [BindProperty]
        public IFormFile Imagee { get; set; }


        [BindProperty]
        public Product homeproduct { get; set; }



        public IActionResult OnGet(int? id)
        {
            homeProductDatalist = _db.Products.Where(p => p.Isdeleted == false).ToList();
            if (id.HasValue)
            {
                var homeprt = _db.Products.Where(p => p.Id == id && p.Isdeleted == false).FirstOrDefault();
                if (homeprt == null)
                {
                    return Page();
                }
                homeproduct = homeprt;
                return Page();
            }
            homeproduct = new Product();
            return Page();

        }


        public IActionResult OnPost()

        {
            if (homeproduct.Id > 0)
            {
                var homeprt = _db.Products.AsNoTracking().Where(p => p.Id == homeproduct.Id && p.Isdeleted == false).FirstOrDefault();
                if (homeprt == null)
                {
                    return Page();

                }

                if (Imagee != null)
                {
                    homeproduct.Image = ProcessUploadedFile();
                }
                else
                {
                    homeproduct.Image = homeprt.Image;
                }



                homeproduct.Isdeleted = homeprt.Isdeleted;

                _db.Products.Update(homeproduct);
                _db.SaveChanges();
                TempData["info"] = "Your Data update Successfully";
            }

            else


            {

                if (Imagee != null)
                {
                    homeproduct.Image = ProcessUploadedFile();
                }
                else
                {
                    homeproduct.Image = "noimage.webp";
                }



                homeproduct.Isdeleted = false;

                _db.Products.Add(homeproduct);
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
            var homeprt = _db.Products.Where(p => p.Id == id && p.Isdeleted == false).FirstOrDefault();
            if (homeprt == null)
            {
                return Page();
            }

            homeprt.Isdeleted = true;
            _db.SaveChanges();
            TempData["success"] = "data deleted";

            return RedirectToPage();
        }



    }
}

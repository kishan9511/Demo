using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Demo.Pages.Admin
{
    public class serviceModel : PageModel
    {
        public readonly DemoProjectContext _db;
        public readonly IHostEnvironment _hostEnvironment;
        public serviceModel(DemoProjectContext db, IHostEnvironment hostEnvironment_environment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment_environment;
        }
        public IEnumerable<AboutService> DatalistService { get; set; }

        //[BindProperty]
        //public IFormFile Imagee { get; set; }

        [BindProperty]
        public AboutService Service { get; set; }




        public IActionResult OnGet(int? id)
        {

            DatalistService = _db.AboutServices.Where(a => a.Isdeleted == false).ToList();
            if (id.HasValue)
            {
                var srcv = _db.AboutServices.Where(a => a.Id == id && a.Isdeleted == false).FirstOrDefault();
                if (srcv == null)
                {
                    return Page();
                }
                Service = srcv;
                return Page();

            }
            Service = new AboutService();
            return Page();
        }




        public IActionResult OnPost()
        {
            if (Service.Id > 0)
            {
                var srcv = _db.AboutServices.AsNoTracking().Where(a => a.Id == Service.Id && a.Isdeleted == false).FirstOrDefault();

                if (srcv == null)
                {
                    return Page();
                }



                //if (Imagee != null)
                //{
                //    Service.Logo = ProcessUploadedFile();
                //}
                //else
                //{
                //    Service.Logo = srcv.Logo;
                //}



                Service.Isdeleted = srcv.Isdeleted;

                _db.AboutServices.Update(Service);
                _db.SaveChanges();
                TempData["info"] = "Your Data update Successfully";

            }
            else
            {

                //if (Imagee != null)
                //{
                //    Service.Logo = ProcessUploadedFile();
                //}
                //else
                //{
                //    Service.Logo = "noimage.webp";
                //}

                Service.Isdeleted = false;
                _db.AboutServices.Add(Service);
                _db.SaveChanges();
                TempData["success"] = "data saved";
            }


            return RedirectToPage();
        }



        //private string ProcessUploadedFile()
        //{
        //    string uniqueFileName = null;

        //    if (Imagee != null)
        //    {
        //        string uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot\\Images");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + Imagee.FileName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            Imagee.CopyTo(fileStream);
        //        }
        //    }
        //    return uniqueFileName;

        //}






        public IActionResult OnPostDelete(int? id)
        {
            var srcv = _db.AboutServices.Where(a => a.Id == id && a.Isdeleted == false).FirstOrDefault();
            if (srcv == null)
            {
                return NotFound();
            }
            srcv.Isdeleted = true;
            _db.SaveChanges();

            TempData["success"] = "data deleted";
            return RedirectToPage();


        }


    }
}

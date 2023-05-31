using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Demo.Pages.Admin
{
    public class ourTeamModel : PageModel
    {
        public readonly DemoProjectContext _db;
        public readonly IHostEnvironment _hostEnvironment;
        public ourTeamModel(DemoProjectContext db, IHostEnvironment hostEnvironment_environment)

        {
            _db = db;
            _hostEnvironment = hostEnvironment_environment;
        }

        public IEnumerable<OurTeam> ourTeamDataList { get; set; }


        [BindProperty]
        public OurTeam ourTeam { get; set; }


        [BindProperty]
        public IFormFile Imagee { get; set; }

        public IActionResult OnGet(int? id)
        {
            ourTeamDataList = _db.OurTeams.Where(o => o.Isdeleted == false).ToList();
            if (id.HasValue)
            {
                var ortem = _db.OurTeams.Where(o => o.Id == id && o.Isdeleted == false).FirstOrDefault();
                if (ortem == null)
                {
                    return Page();
                }
                ourTeam = ortem;
                return Page();
            }
            ourTeam = new OurTeam();
            return Page();

        }

        public IActionResult OnPost()
        {
            if (ourTeam.Id > 0)
            {
                var ortem = _db.OurTeams.AsNoTracking().Where(o => o.Id == ourTeam.Id && o.Isdeleted == false).FirstOrDefault();
                if (ortem == null)
                {
                    return NotFound();
                }




                if (Imagee != null)
                {
                    ourTeam.SlidShowLogo = ProcessUploadedFile();
                }
                else
                {
                    ourTeam.SlidShowLogo = ortem.SlidShowLogo;
                }

                ourTeam.Isdeleted = ortem.Isdeleted;

                _db.OurTeams.Update(ourTeam);
                _db.SaveChanges();
                TempData["info"] = "Your Data update Successfully";

            }

            else
            {

                if (Imagee != null)
                {
                    ourTeam.SlidShowLogo = ProcessUploadedFile();
                }
                else
                {
                    ourTeam.SlidShowLogo = "noimage.webp";
                }







                ourTeam.Isdeleted = false;

                _db.OurTeams.Add(ourTeam);
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
            var ortem = _db.OurTeams.Where(o => o.Id == id && o.Isdeleted == false).FirstOrDefault();
            if (ortem == null)
            {
                return Page();
            }
            ortem.Isdeleted = true;

            _db.SaveChanges();


            TempData["success"] = "data deleted";
            return RedirectToPage();


        }




    }
}

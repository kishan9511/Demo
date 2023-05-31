using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Demo.Pages.Admin
{
    public class countryModel : PageModel
    {
        public readonly DemoProjectContext _db;

        public countryModel(DemoProjectContext db)
        {
            _db = db;
        }

        //datalist
        public IEnumerable<Country> datalist { get; set; }
        [BindProperty]

        public Country CountryModel { get; set; }





        public IActionResult OnGet(int? id)
        {
            datalist = _db.Countries.Where(e => e.IsDeleted == false).ToList();
            if (id.HasValue)
            {
                var co = _db.Countries.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }

                CountryModel = co;
                return Page();
            }
            CountryModel = new Country();
            return Page();

        }






        public IActionResult OnPost()
        {

            //if (CountryModel.ShortName == "ind")
            //    TempData["error"] = "this short name exists";

            if (CountryModel.Id > 0)//Update
            {
                var co = _db.Countries.AsNoTracking().Where(e => e.Id == CountryModel.Id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }
                CountryModel.IsDeleted = co.IsDeleted;

                _db.Countries.Update(CountryModel);
                _db.SaveChanges();
                TempData["info"] = "Your Data update Successfully";
            }
            else
            {
                try
                {
                    CountryModel.IsDeleted = false;
                    _db.Countries.Add(CountryModel);
                    _db.SaveChanges();
                    TempData["success"] = "data saved";

                }
                catch (Exception ex) { }
            }



            return RedirectToPage();

        }



        public IActionResult OnPostDelete(int id)
        {
            var co = _db.Countries.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
            if (co == null)
            {
                return NotFound();

            }
            co.IsDeleted = true;
            _db.SaveChanges();

            TempData["success"] = "data deleted";
            return RedirectToPage();

        }
    }
}



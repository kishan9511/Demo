using Demo.CommonModel;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Demo.Pages.Admin

{
    public class cityModel : PageModel
    {
        public readonly DemoProjectContext _db;

        public cityModel(DemoProjectContext db)
        {
            _db = db;
        }



        public IEnumerable<City_Model> datalistmodel { get; set; }

        [BindProperty]
        public int SelectCountryId { get; set; }


        public List<SelectListItem> SelectCountry { get; set; }


        [BindProperty]
        public int SelectedId { get; set; }


        public List<SelectListItem> SelectOptions { get; set; }


        [BindProperty]
        public City CityModel { get; set; }



        public IActionResult OnGet(int? id)
        {
            //datalist = _db.Cities.Where(e => e.IsDeleted == false).ToList();
            datalistmodel = _db.Cities.Where(x => x.IsDeleted == false).Select(x => new City_Model
            {
                Id = x.Id,
                StateId = x.StateId,
                counteryname = x.State.Country.FullName,
                countryid = x.State.CountryId,
                stateNameT = x.State.FullName,
                FullName = x.FullName,
                Pincode = x.Pincode,
            }).ToList();

            CityModel = new City();
            if (id.HasValue)
            {
                var co = _db.Cities.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
                var countryco = _db.States.Where(e => e.Id == co.StateId && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return Page();
                }
                CityModel = co;
                SelectCountryId = Convert.ToInt32(countryco.CountryId);

            }
            #region----------DDL get Method for country---------
            //------For list COUNTRUES records-----

            SelectCountry = _db.Countries.Where(a => a.IsDeleted == false).Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.FullName

            }).ToList();

            //-----------for update by id------------

            if (CityModel?.State?.CountryId > 0)
            {
                var result = _db.Countries.Where(x => x.Id == CityModel.State.CountryId).Select(x => x).FirstOrDefault();
                SelectCountry.Insert(0, new SelectListItem()
                {
                    Text = result.FullName,
                    Value = result.Id.ToString()
                });

                SelectCountryId = result.Id;
            }
            else
            {
                SelectCountry.Insert(0, new SelectListItem()
                {
                    Text = "----Select----",
                    Value = string.Empty
                });
            }

            //-------for selected COUNTRUES id--------------
            List<Country> listCountry = new List<Country>();
            listCountry = (from Countryidobj in _db.Countries where Countryidobj.Id == SelectCountryId select Countryidobj).ToList();


            #endregion



            #region----------DDL get Method for State---------

            //------For list States records-----
            SelectOptions = _db.States.Where(a => a.IsDeleted == false).Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.FullName
            }).ToList();

            //-----------for update by id------------
            if (CityModel?.StateId > 0)
            {
                var result = _db.States.Where(x => x.Id == CityModel.StateId).Select(x => x).FirstOrDefault();
                SelectOptions.Insert(0, new SelectListItem()
                {
                    Text = result.FullName,
                    Value = result.Id.ToString()
                });

                SelectedId = result.Id;
            }
            else
            {
                SelectOptions.Insert(0, new SelectListItem()
                {
                    Text = "----Select----",
                    Value = string.Empty
                });
            }

            List<State> liststatee = new List<State>();
            liststatee = (from stateidobj in _db.States where stateidobj.Id == SelectedId select stateidobj).ToList();


            #endregion


            return Page();

        }

        public IActionResult OnPost()
        {

            if (CityModel.Id > 0)//Update
            {
                var co = _db.Cities.AsNoTracking().Where(e => e.Id == CityModel.Id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }
                CityModel.StateId = SelectedId;
                CityModel.IsDeleted = co.IsDeleted;

                _db.Cities.Update(CityModel);
                _db.SaveChanges();
                TempData["info"] = "Your Data update Successfully";

            }
            else
            {
                try
                {
                    CityModel.StateId = SelectedId;
                    CityModel.IsDeleted = false;

                    _db.Cities.Add(CityModel);
                    _db.SaveChanges();
                    TempData["success"] = "data saved";
                }
                catch (Exception ex) { }
            }

            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            var co = _db.Cities.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
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

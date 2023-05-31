using Demo.CommonModel;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Demo.Pages.Admin
{
    public class RegModel : PageModel
    {
        public readonly DemoProjectContext _db;
        public readonly IHostEnvironment _hostEnvironment;

        public RegModel(DemoProjectContext db, IHostEnvironment hostEnvironment_environment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment_environment;
        }
        //public IEnumerable<Registration> datalist { get; set; }

        public IEnumerable<Reg_Model> datalistModel { get; set; }

        [BindProperty]
        public int SelectCountryId { get; set; }
        public List<SelectListItem> SelectCountry { get; set; }

        [BindProperty]
        public int SelectRoleId { get; set; }
        public List<SelectListItem> SelectRole { get; set; }
        [BindProperty]
        public int SelectStateId { get; set; }
        public List<SelectListItem> SelectState { get; set; }
        [BindProperty]
        public int SelectCityId { get; set; }
        public List<SelectListItem> SelectCity { get; set; }
        [BindProperty]
        public int SelectImageId { get; set; }
        public List<SelectListItem> SelectImage { get; set; }
        [BindProperty]
        public IFormFile Imagee { get; set; }
        [BindProperty]
        public Registration RegistrationModel { get; set; }


        public IActionResult OnGet(int? id)
        {
            //datalist= _db.Registrations.Where(e=>e.IsDeleted==false).ToList();

            datalistModel = _db.Registrations.Where(e => e.IsDeleted == false).Select(e => new Reg_Model
            {
                id = e.Id,

                countryid = e.CountryId,
                countryname = e.Country.FullName,

                stateid = e.StateId,
                statename = e.State.FullName,

                cityid = e.CityId,
                cityname = e.City.FullName,

                roleid = e.RoleId,
                rolename = e.Role.RoleName,

                fistname = e.FirstName,
                lastname = e.LastName,
                mobile = e.PhoneNumber,
                image = e.Image,


            }
            ).ToList();

            RegistrationModel = new Registration();
            if (id.HasValue)
            {
                var co = _db.Registrations.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
                var countryco = _db.States.Where(e => e.Id == co.StateId && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return Page();
                }

                RegistrationModel = co;
                SelectCountryId = Convert.ToInt32(countryco.CountryId);
            }

            #region----------DDL get Method for Role---------
            //------For list Role records-----

            SelectRole = _db.Roles.Where(a => a.Isdeleted == false).Select(a => new SelectListItem
            {
                Value = a.RoleId.ToString(),
                Text = a.RoleName
            }).ToList();

            //-----------for update by id------------
            if (RegistrationModel?.RoleId > 0)
            {
                var up = _db.Roles.Where(x => x.RoleId == RegistrationModel.RoleId).Select(x => x).FirstOrDefault();
                SelectRole.Insert(0, new SelectListItem()
                {
                    Text = up?.RoleName,
                    Value = up?.RoleId.ToString()
                });
            }
            else
            {
                SelectRole.Insert(0, new SelectListItem()
                {
                    Text = "----Select----",
                    Value = string.Empty
                });
            }
            //-------for selected role id--------------
            List<Role> listRole = new List<Role>();
            listRole = (from Roleidobj in _db.Roles where Roleidobj.RoleId == SelectRoleId select Roleidobj).ToList();


            #endregion



            #region----------DDL get Method for country---------
            //------For list COUNTRUES records-----

            SelectCountry = _db.Countries.Where(a => a.IsDeleted == false).Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.FullName

            }).ToList();

            //-----------for update by id------------
            if (RegistrationModel?.CountryId > 0)
            {
                var result = _db.Countries.Where(x => x.Id == RegistrationModel.CountryId).Select(x => x).FirstOrDefault();
                SelectCountry.Insert(0, new SelectListItem()
                {
                    Text = result.FullName,
                    Value = result.Id.ToString()
                });
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



            #region----------DDL get Method for states---------




            //------For list STATES records-----

            SelectState = _db.States.Where(aa => aa.IsDeleted == false && aa.CountryId == RegistrationModel.CountryId).Select(aa => new SelectListItem
            {
                Value = aa.Id.ToString(),
                Text = aa.FullName

            }).ToList();

            //-----------for update by id------------
            if (RegistrationModel?.StateId > 0)
            {
                var result = _db.States.Where(xm => xm.Id == RegistrationModel.StateId).Select(xm => xm).FirstOrDefault();
                SelectState.Insert(0, new SelectListItem()
                {
                    Text = result.FullName,
                    Value = result.Id.ToString()
                });
            }
            else
            {
                SelectState.Insert(0, new SelectListItem()
                {
                    Text = "----Select----",
                    Value = string.Empty
                });
            }
            //-------for selected STATES id--------------
            List<State> listState = new List<State>();
            listState = (from Stateidobj in _db.States where Stateidobj.Id == SelectStateId select Stateidobj).ToList();


            #endregion



            #region----------DDL get Method for city---------
            //------For list Cities records-----

            SelectCity = _db.Cities.Where(c => c.IsDeleted == false && c.StateId == RegistrationModel.StateId).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.FullName

            }).ToList();

            //-----------for update by id------------
            if (RegistrationModel?.CityId > 0)
            {
                var result = _db.Cities.Where(xy => xy.Id == RegistrationModel.CityId).Select(xy => xy).FirstOrDefault();
                SelectCity.Insert(0, new SelectListItem()
                {
                    Text = result.FullName,
                    Value = result.Id.ToString()
                });
            }
            else
            {
                SelectCity.Insert(0, new SelectListItem()
                {
                    Text = "----Select----",
                    Value = string.Empty
                });
            }
            //-------for selected CITY id--------------
            List<City> listCity = new List<City>();
            listCity = (from Cityidobj in _db.Cities where Cityidobj.Id == SelectCityId select Cityidobj).ToList();


            #endregion







            return Page();

        }

        public IActionResult OnPost()
        {

            if (RegistrationModel.Id > 0)
            {
                var re = _db.Registrations.AsNoTracking().Where(p => p.Id == RegistrationModel.Id && p.IsDeleted == false).FirstOrDefault();
                if (re == null)
                {
                    return NotFound();
                }
                if (Imagee != null)
                {
                    RegistrationModel.Image = ProcessUploadedFile();
                }
                else
                {
                    RegistrationModel.Image = re.Image;
                }


                RegistrationModel.RoleId = SelectRoleId;
                RegistrationModel.CountryId = SelectCountryId;
                RegistrationModel.StateId = SelectStateId;
                RegistrationModel.CityId = SelectCityId;


                RegistrationModel.IsDeleted = re.IsDeleted;
                _db.Registrations.Update(RegistrationModel);
                _db.SaveChanges();
                TempData["info"] = "Your Data update Successfully";
            }
            else
            {
                try
                {

                    if (Imagee != null)
                    {
                        RegistrationModel.Image = ProcessUploadedFile();
                    }
                    else
                    {
                        RegistrationModel.Image = "noimage.webp";
                    }

                    RegistrationModel.RoleId = SelectRoleId;
                    RegistrationModel.CountryId = SelectCountryId;
                    RegistrationModel.StateId = SelectStateId;
                    RegistrationModel.CityId = SelectCityId;


                    RegistrationModel.IsDeleted = false;
                    _db.Registrations.Add(RegistrationModel);
                    _db.SaveChanges();
                    TempData["success"] = "data saved";

                }
                catch (Exception ex) { }
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

        public IActionResult OnPostDelete(int id)

        {
            var ree = _db.Registrations.Where(eee => eee.Id == id && eee.IsDeleted == false).FirstOrDefault();
            if (ree == null)
            {
                return NotFound();
            }
            ree.IsDeleted = true;
            _db.SaveChanges();
            TempData["success"] = "data deleted";
            return RedirectToPage();

        }

    }

}


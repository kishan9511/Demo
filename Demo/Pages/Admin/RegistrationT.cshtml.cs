//using Demo.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Hosting;
//using Demo.CommonModel;

//namespace Demo.Pages.Admin
//{
//    public class RegistrationTModel : PageModel
//    {
//        public readonly DemoProjectContext _db;
//        public readonly IHostEnvironment _hostEnvironment;
//        public RegistrationTModel(DemoProjectContext db, IHostEnvironment hostEnvironment_environment)
//        {
//            _db = db;
//            _hostEnvironment = hostEnvironment_environment;
//        }
//        public IEnumerable<Regist> datalist { get; set; }
//        public IEnumerable<RegistrationM_Model> datalistModel { get; set; }

//        [BindProperty]
//        public int SelectCountryId { get; set; }
//        public List<SelectListItem> SelectCountry { get; set; }


//        [BindProperty]
//        public int SelectStateId { get; set; }
//        public List<SelectListItem> SelectState { get; set; }

//        [BindProperty]
//        public int SelectCityId { get; set; }
//        public List<SelectListItem> SelectCity { get; set; }
//        //-----CITIES option ends

//        [BindProperty]
//        public int SelectImageId { get; set; }
//        public List<SelectListItem> SelectImage { get; set; }



//        [BindProperty]
//        public IFormFile Image { get; set; }


//        [BindProperty]

//        public Regist RegistModel { get; set; }


//        public IActionResult OnGet(int? id)
//        {
//            //datalist = _db.Regists.Where(e => e.IsDeleted == false).ToList();
//            datalistModel = _db.Regists.Where(e => e.IsDeleted == false).Select(e => new RegistrationM_Model

//            {
//                id = e.Id,

//                countryid = e.CountryId,
//                countrynameTwo = e.Country.FullName,

//                stateidTwo = e.StateId,
//                statenameTwo = e.State.FullName,

//                cityidTwo = e.CityId,
//                citynameTwo = e.City.FullName,

//                fistnameTwo = e.FirstName,
//                lastnameTwo = e.LastName,

//                mobileTwo = e.PhoneNumber,
//                imageTwo = e.Image
//            }).ToList();


//            RegistModel = new Regist();
//            if (id.HasValue)
//            {
//                var co = _db.Regists.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
//                if (co == null)
//                {
//                    return Page();
//                }

//                RegistModel = co;
//            }


//            #region----------DDL get Method for country---------
//            //------For list COUNTRUES records-----

//            SelectCountry = _db.Countries.Where(a => a.IsDeleted == false).Select(a => new SelectListItem
//            {
//                Value = a.Id.ToString(),
//                Text = a.FullName

//            }).ToList();

//            //-----------for update by id------------
//            if (RegistModel?.CountryId > 0)
//            {
//                var result = _db.Countries.Where(x => x.Id == RegistModel.CountryId).Select(x => x).FirstOrDefault();
//                SelectCountry.Insert(0, new SelectListItem()
//                {
//                    Text = result.FullName,
//                    Value = result.Id.ToString()
//                });
//            }
//            else
//            {
//                SelectCountry.Insert(0, new SelectListItem()
//                {
//                    Text = "----Select----",
//                    Value = string.Empty
//                });
//            }
//            //-------for selected COUNTRUES id--------------

//            List<Country> listCountry = new List<Country>();
//            listCountry = (from Countryidobj in _db.Countries where Countryidobj.Id == SelectCountryId select Countryidobj).ToList();


//            #endregion


//            //-----------list for STATES records-------------


//            #region----------DDL get Method for state---------




//            //------For list STATES records-----

//            SelectState = _db.States.Where(a => a.IsDeleted == false && a.CountryId == RegistModel.CountryId).Select(b => new SelectListItem
//            {
//                Value = b.Id.ToString(),
//                Text = b.FullName

//            }).ToList();

//            //-----------for update by id------------
//            if (RegistModel?.StateId > 0)
//            {
//                var result = _db.States.Where(xm => xm.Id == RegistModel.StateId).Select(xm => xm).FirstOrDefault();
//                SelectState.Insert(0, new SelectListItem()
//                {
//                    Text = result.FullName,
//                    Value = result.Id.ToString()
//                });
//            }
//            else
//            {
//                SelectState.Insert(0, new SelectListItem()
//                {
//                    Text = "----Select----",
//                    Value = string.Empty
//                });
//            }
//            //-------for selected STATES id--------------
//            List<State> listState = new List<State>();
//            listState = (from Stateidobj in _db.States where Stateidobj.Id == SelectStateId select Stateidobj).ToList();


//            #endregion


//            #region----------DDL get Method for city---------
//            //------For list Cities records-----

//            SelectCity = _db.Cities.Where(c => c.IsDeleted == false && c.StateId == RegistModel.StateId).Select(c => new SelectListItem
//            {
//                Value = c.Id.ToString(),
//                Text = c.FullName

//            }).ToList();

//            //-----------for update by id------------
//            if (RegistModel?.CityId > 0)
//            {
//                var result = _db.Cities.Where(xy => xy.Id == RegistModel.CityId).Select(xy => xy).FirstOrDefault();
//                SelectCity.Insert(0, new SelectListItem()
//                {
//                    Text = result.FullName,
//                    Value = result.Id.ToString()
//                });
//            }
//            else
//            {
//                SelectCity.Insert(0, new SelectListItem()
//                {
//                    Text = "----Select----",
//                    Value = string.Empty
//                });
//            }
//            //-------for selected CITY id--------------
//            List<City> listCity = new List<City>();
//            listCity = (from Cityidobj in _db.Cities where Cityidobj.Id == SelectCityId select Cityidobj).ToList();


//            #endregion


//            return Page();

//        }




//        public IActionResult OnPost()
//        {
//            if (RegistModel.Id > 0)
//            {
//                var re = _db.Regists.AsNoTracking().Where(p => p.Id == RegistModel.Id && p.IsDeleted == false).FirstOrDefault();
//                if (re == null)
//                {
//                    return NotFound();
//                }

//                if (Image != null)
//                {
//                    RegistModel.Image = ProcessUploadedFile();
//                }
//                else
//                {
//                    RegistModel.Image = re.Image;
//                }






//                RegistModel.CountryId = SelectCountryId;
//                RegistModel.StateId = SelectStateId;
//                RegistModel.CityId = SelectCityId;


//                RegistModel.IsDeleted = re.IsDeleted;
//                _db.Regists.Update(RegistModel);
//                _db.SaveChanges();
//                TempData["info"] = "Your Data update Successfully";
//            }


//            else
//            {
//                try


//                {


//                    if (Image != null)
//                    {
//                        RegistModel.Image = ProcessUploadedFile();
//                    }
//                    else
//                    {
//                        RegistModel.Image = "noimage.webp";
//                    }





//                    RegistModel.CountryId = SelectCountryId;
//                    RegistModel.StateId = SelectStateId;
//                    RegistModel.CityId = SelectCityId;

//                    RegistModel.IsDeleted = false;
//                    _db.Regists.Add(RegistModel);
//                    _db.SaveChanges();
//                    TempData["success"] = "data saved";

//                }


//                catch (Exception ex) { }
//            }


//            return RedirectToPage();
//        }




//        private string ProcessUploadedFile()
//        {
//            string uniqueFileName = null;

//            if (Image != null)
//            {
//                string uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot\\Images");
//                uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
//                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
//                using (var fileStream = new FileStream(filePath, FileMode.Create))
//                {
//                    Image.CopyTo(fileStream);
//                }
//            }
//            return uniqueFileName;

//        }

//        public IActionResult OnPostDelete(int id)

//        {
//            var ree = _db.Regists.Where(eee => eee.Id == id && eee.IsDeleted == false).FirstOrDefault();
//            if (ree == null)
//            {
//                return NotFound();
//            }
//            ree.IsDeleted = true;
//            _db.SaveChanges();
//            TempData["success"] = "data deleted";
//            return RedirectToPage();

//        }
//    }
//}


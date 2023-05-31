using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using Demo.CommonModel;

namespace Demo.Pages.Admin
{
    public class stateModel : PageModel
    {

        public readonly DemoProjectContext _db;

        public stateModel(DemoProjectContext db)
        {
            _db = db;
        }



        public IEnumerable<State_Model> datalistmodel { get; set; }

        //----------Objects for drop down--------
        [BindProperty]
        public int SelectedId { get; set; }


        public List<SelectListItem> SelectOptions { get; set; }


        [BindProperty]
        public State StateModel { get; set; }


        public IActionResult OnGet(int? id)
        {
            //datalist = _db.States.Where(e => e.IsDeleted == false).ToList();

            datalistmodel = _db.States.Where(x => x.IsDeleted == false).Select(x => new State_Model
            {
                id = x.Id,
                countryid = x.CountryId,
                countryname = x.Country.FullName,
                fullname = x.FullName,
                shortname = x.ShortName,

            }).ToList();


            StateModel = new State();


            if (id.HasValue)
            {
                var co = _db.States.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }
                StateModel = co;



            }


            #region----------DDL get Method---------

            //------For list country records-----
            SelectOptions = _db.Countries.Where(a => a.IsDeleted == false).Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.FullName
            }).ToList();

            //-----------for update by id------------
            if (StateModel?.CountryId > 0)
            {
                var result = _db.Countries.Where(x => x.Id == StateModel.CountryId).Select(x => x).FirstOrDefault();
                SelectOptions.Insert(0, new SelectListItem()
                {
                    Text = result.FullName,
                    Value = result.Id.ToString()
                });
            }
            else
            {
                SelectOptions.Insert(0, new SelectListItem()
                {
                    Text = "----Select----",
                    Value = string.Empty
                });
            }
            //-------for selected country id--------------
            List<Country> listbj = new List<Country>();
            listbj = (from idobj in _db.Countries where idobj.Id == SelectedId select idobj).ToList();


            #endregion





            return Page();

        }


        public IActionResult OnPost()
        {
            if (StateModel.Id > 0)//Update
            {
                var co = _db.States.AsNoTracking().Where(e => e.Id == StateModel.Id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }

                StateModel.CountryId = SelectedId;


                StateModel.IsDeleted = co.IsDeleted;

                _db.States.Update(StateModel);
                _db.SaveChanges();
                TempData["info"] = "Your Data update Successfully";

            }
            else
            {
                try
                {
                    StateModel.CountryId = SelectedId;
                    StateModel.IsDeleted = false;
                    _db.States.Add(StateModel);
                    _db.SaveChanges();
                    TempData["success"] = "data saved";
                }
                catch (Exception ex) { }
            }
            return RedirectToPage();

        }

        public IActionResult OnPostDelete(int id)
        {
            var co = _db.States.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
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

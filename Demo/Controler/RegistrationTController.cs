using Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Controler
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegistrationTController : ControllerBase
    {
        private readonly DemoProjectContext _db;
        public RegistrationTController(DemoProjectContext db)

        {
            _db = db;
        }

        //-------------------------state dropdown with respect to country---------------------------

        public async Task<IActionResult> StateDropdown(int countryID)
        {
            var st = await _db.States.Where(s => s.IsDeleted == false && s.CountryId == countryID).Select(s => new
            {
                id = s.Id,
                fullname = s.FullName
            }).ToListAsync();

            return new JsonResult(st);
        }

        //-------------------------city dropdown with respect to state ---------------------------


        public async Task<IActionResult> CityDropdown(int StateID)
        {
            var ct = await _db.Cities.Where(c => c.IsDeleted == false && c.StateId == StateID).Select(c => new
            {
                ID = c.Id,
                Fullname = c.FullName
            }).ToListAsync();
            return new JsonResult(ct);
        }
    }
}
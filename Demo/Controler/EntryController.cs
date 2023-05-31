using Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Demo.Controler
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EntryController : ControllerBase
    {
        public readonly DemoProjectContext _db;
        public EntryController(DemoProjectContext db) 
        {
            _db = db;
        }


        //-----------------------ModelDropdown with rescept to Brand----------------------
        public async Task<IActionResult> GetModelDropdown(int brandid)
        {
            var mo = await _db.ProductModels.Where(p => p.Isdeleted == false && p.BrandId == brandid).Select(p => new
            {
                modelid = p.Modelid,
                modelname = p.ModelName,

            }).ToListAsync();
            return new JsonResult(mo);
        }






        //-----------------------SericeDropdown with rescept to Model----------------------


        public async Task<IActionResult> GetSericeDropdown(int modelid)
        {
            var serice = await _db.ProductSerises.Where(p => p.Isdeleted == false && p.Modelid == modelid).Select(p => new
            {
                Seriid = p.Seriseid,
                seirnumber = p.SeriseNumber,

            }).ToArrayAsync();
            return new JsonResult(serice);
        }







    }
}

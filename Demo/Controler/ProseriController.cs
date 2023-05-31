using Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Demo.Controler
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProseriController : ControllerBase 
    {
        public readonly DemoProjectContext _db;   
        public ProseriController (DemoProjectContext db)
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

        //-----------------------BrandDropdown with rescept to Sub-category.----------------------


        //public async Task<IActionResult> GetBrandDropdown(int subCategoryId)
        //{
        //    var sub = await _db.ProductBrands.Where(b => b.Isdeleted == false && b.SubCategoryId == subCategoryId).Select(b => new
        //    {
        //        brandid = b.BrandId,
        //        brandname = b.BrandName


        //    }).ToListAsync();


        //          return new JsonResult(sub);
        //}



        //-----------------------SubcategoryDropdown with rescept to category.----------------------


        public async Task<IActionResult> GetSubcategoryDropdown(int CategoryId)
        {
            var Category = await _db.SubCategories.Where(b => b.Isdeleted == false && b.ProductCategoryId == CategoryId).Select(b => new
            {
                subCategoryId = b.Id,
                subCategoryname = b.Name


            }).ToListAsync();


            return new JsonResult(Category);
        }

        //---------------Check Sub category IsModelSerise is true or false----

        public async Task<IActionResult> GetSubcategoryInfo(int id)
        {
            var sub = await _db.SubCategories.Where(b => b.Isdeleted == false && b.Id == id).Select(b => new
            {
                brandid = b.Id,
                brandname = b.Name,
                isModelirserise = b.IsModelOrSerise,

            }).FirstOrDefaultAsync();


            return new JsonResult(sub);
        }
    }
}








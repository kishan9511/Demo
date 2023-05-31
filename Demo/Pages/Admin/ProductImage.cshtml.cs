using Demo.CommonModel;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Demo.Pages.Admin
{
    public class ProductImageModel : PageModel
    {
        public readonly DemoProjectContext _db;
        public readonly IHostEnvironment _hostEnvironment;
        public ProductImageModel(DemoProjectContext db, IHostEnvironment hostEnvironment_environment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment_environment;
        }

        public IEnumerable<ProImage_Model> imgDatalist { get; set; }



        [BindProperty]
        public IFormFile Imagee { get; set; }

        [BindProperty]
        public int ProductEntryID { get; set; }


        public List<SelectListItem> SelectEntryOptions { get; set; }

        [BindProperty]
        public ProductImage ProImg { get; set; }




        public IActionResult OnGet(int? id)
        {
            imgDatalist = _db.ProductImages.Where(x => x.Isdeleted == false).Select(x => new ProImage_Model
            {
                imageid = x.ProductImageid,
                imagename = x.ImageName,


                ProductEntryid = x.ProductEntryid,

                productname = x.ProductEntry.ProductName
            }).ToList();

            ProImg = new ProductImage();

            if (id.HasValue)
            {
                var pi = _db.ProductImages.Where(x => x.ProductImageid == id && x.Isdeleted == false).FirstOrDefault();

                if (pi == null)
                {
                    return Page();
                }
                ProImg = pi;
            }



            #region----------DDL get Method for Entry---------
            //for  Entry list
            SelectEntryOptions = _db.ProductEntries.Where(g => g.Isdeleted == false).Select(g => new SelectListItem
            {
                Value = g.ProductEntryid.ToString(),
                Text = g.ProductName
            }).ToList();

            // for update id 
            if (ProImg.ProductEntryid > 0)
            {
                var imgResult = _db.ProductEntries.Where(g => g.ProductEntryid == ProImg.ProductEntryid).Select(g => g).FirstOrDefault();

                SelectEntryOptions.Insert(0, new SelectListItem()
                {
                    Text = imgResult?.ProductName,
                    Value = imgResult?.ProductEntryid.ToString()

                });

                ProductEntryID = imgResult.ProductEntryid; //for selectd Entry(2nd method).


            }
            else
            {
                SelectEntryOptions.Insert(0, new SelectListItem()
                {
                    Text = "----Select----",
                    Value = string.Empty
                });
            }
            //for selectd Entry(1st method).

            //List<ProductEntry> listobjEntry = new List<ProductEntry>();
            //listobjEntry = (from entryidobj in _db.ProductEntries where entryidobj.ProductEntryid == ProductEntryID select entryidobj).ToList();

            #endregion



            return Page();

        }

        public IActionResult OnPost()
        {
            if (ProImg.ProductImageid > 0)
            {
                var pi = _db.ProductImages.AsNoTracking().Where(x => x.ProductImageid == ProImg.ProductImageid && x.Isdeleted == false).FirstOrDefault();

                if (pi == null)
                {
                    return NotFound();
                }

                if (Imagee != null)
                {
                    ProImg.ImageName = ProcessUploadedFile();
                }
                else
                {
                    ProImg.ImageName = pi.ImageName;
                }


                ProImg.ProductEntryid = ProductEntryID;
                ProImg.Isdeleted = pi.Isdeleted;

                _db.ProductImages.Update(ProImg);
                _db.SaveChanges();
                TempData["info"] = "Your Data update Successfully";

            }
            else
            {
                try
                {
                    if (Imagee != null)
                    {
                        ProImg.ImageName = ProcessUploadedFile();
                    }

                    else
                    {
                        ProImg.ImageName = "noimage.webp";

                    }

                    ProImg.ProductEntryid = ProductEntryID;
                    ProImg.Isdeleted = false;
                    _db.ProductImages.Add(ProImg);
                    _db.SaveChanges();
                    TempData["info"] = "Your Data update Successfully";

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
            var im = _db.ProductImages.Where(eee => eee.ProductImageid == id && eee.Isdeleted == false).FirstOrDefault();
            if (im == null)
            {
                return NotFound();
            }
            im.Isdeleted = true;
            _db.SaveChanges();
            TempData["success"] = "data deleted";
            return RedirectToPage();

        }
    }
}

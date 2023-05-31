using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo.Pages;

public class shopModel : PageModel
{
	public readonly DemoProjectContext _db;
	public readonly IHostEnvironment _hostEnvironment;
	public shopModel(DemoProjectContext db, IHostEnvironment hostEnvironment_environment)
	{
		_db = db;
		_hostEnvironment = hostEnvironment_environment;
	}


	public IEnumerable<ProductCategory> Categorylist { get; set; }


	[BindProperty]
	public ProductCategory Categoryobj { get; set; }
	public void OnGet()
	{

		Categoryobj = _db.ProductCategories.FirstOrDefault(x => x.Isdeleted == false);

		Categorylist = _db.ProductCategories.Where(t => t.Isdeleted == false).ToList();

	}
}

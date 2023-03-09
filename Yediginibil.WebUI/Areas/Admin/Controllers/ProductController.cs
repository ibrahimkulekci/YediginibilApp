using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Yediginibil.WebUI.Areas.Admin.Models.Product;
using YediginiBil.Business.Abstract;
using YediginiBil.Business.Common;

namespace Yediginibil.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IProductService _productService;
        private IBrandService _brandService;
        private IIngredientService _ingredientService;

        public ProductController(IIngredientService ingredientService,IBrandService brandService, IProductService productService)
        {
            _ingredientService = ingredientService;
            _brandService = brandService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            AddViewModel model = new AddViewModel();
            model.Brands = _brandService.GetAll();
            model.SelectIngredients = GetIngredientSelectList();

            return View(model);
        }
        [HttpPost]
        public IActionResult Add(AddViewModel model)
        {
            if (model.File != null)
            {
                var extension = Path.GetExtension(model.File.FileName);
                var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(model.Title) + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Product/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                model.File.CopyTo(stream);
                model.ImageUrl = "img/Product/" + newImageName;
            }
            else
            {
                model.ImageUrl = "img/nullimage.jpg";
            }
            YediginiBil.Entities.Product record = new YediginiBil.Entities.Product();

            record.Title = model.Title;
            record.Barcode = model.Barcode;
            record.ShortDescription = model.ShortDescription;
            record.LongDescription = model.LongDescription;
            record.ImageUrl = model.ImageUrl;
            record.Status = model.Status;
            record.SeoTitle = model.SeoTitle;
            record.SeoUrl = SeoHelper.ConvertToValidUrl(model.Title);
            record.SeoDescription = model.SeoDescription;
            record.CreatingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            record.UpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());

            _productService.Create(record);
            TempData["message"] = "SuccessAdd";
            return Redirect("~/Admin/Product");
        }

        [NonAction]
        private List<SelectListItem> GetIngredientSelectList()
        {
            return _ingredientService.GetAll().Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = string.Format("{0}", r.Title) }).ToList();
        }
    }
}

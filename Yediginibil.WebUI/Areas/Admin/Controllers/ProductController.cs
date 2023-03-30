using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yediginibil.WebUI.Areas.Admin.Models;
using Yediginibil.WebUI.Areas.Admin.Models.Product;
using YediginiBil.Business.Abstract;
using YediginiBil.Business.Common;
using YediginiBil.Business.ValidationRules;
using YediginiBil.Entities;

namespace Yediginibil.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        YediginiBil.DataAccess.Concrete.EfCore.YediginibilDbContext context = new YediginiBil.DataAccess.Concrete.EfCore.YediginibilDbContext();

        private IProductService _productService;
        private IBrandService _brandService;
        private IIngredientService _ingredientService;
        private IProductIngredientService _productIngredientService;

        public ProductController(IProductIngredientService productIngredientService,IIngredientService ingredientService,IBrandService brandService, IProductService productService)
        {
            _productIngredientService = productIngredientService;
            _ingredientService = ingredientService;
            _brandService = brandService;
            _productService = productService;
        }

        public IActionResult Index(int page=1)
        {
            const int pageSize = 10;
            return View(new ListViewModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = _productService.GetAllCount(),
                    CurrentPage=page,
                    ItemsPerPage=pageSize
                },
                Products = _productService.GetAll(page, pageSize).OrderByDescending(x=>x.Id).ToList()
            }); 
        }
        [HttpGet]
        public IActionResult Add()
        {
            AddViewModel model = new AddViewModel();
            model.Status = true;
            model.BrandSelectList = GetBrandSelectList();
            model.IngredientSelectList = GetIngredientSelectList();

            return View(model);
        }
        [HttpPost]
        public IActionResult Add(AddViewModel model)
        {
            if (model.File != null)
            {
                var extension = Path.GetExtension(model.File.FileName);
                var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(model.Title) + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/product/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                model.File.CopyTo(stream);
                model.ImageUrl = "img/product/" + newImageName;
            }
            else
            {
                model.ImageUrl = "img/nullimage.jpg";
            }

            YediginiBil.Entities.Product record = new YediginiBil.Entities.Product();

            record.Title = model.Title;
            record.Barcode = model.Barcode;
            record.BrandId = model.BrandId;
            record.ShortDescription = model.ShortDescription;
            record.LongDescription = model.LongDescription;
            record.ImageUrl = model.ImageUrl;
            record.Status = model.Status;

            if (model.SeoTitle == null) { model.SeoTitle = model.Title; }
            if(model.SeoUrl==null) { model.SeoUrl = SeoHelper.ConvertToValidUrl(model.Title); }
            if (model.SeoDescription == null) { model.SeoDescription = model.ShortDescription; }

            record.SeoTitle = model.SeoTitle;
            record.SeoUrl = model.SeoUrl;
            record.SeoDescription = model.SeoDescription;
            record.CreatingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            record.UpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());

            _productService.Create(record);

            int productId = record.Id;

            ProductIngredient recordPI = new ProductIngredient();

            for (int i = 0; i < model.IngredientsIds.Count(); i++)
            {
                recordPI.Id = 0;
                recordPI.IngredientId = Convert.ToInt32(model.IngredientsIds[i].ToString());
                recordPI.ProductId = productId;

                _productIngredientService.Create(recordPI);
            }

            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Ürün başarıyla eklendi.";
            return Redirect("~/Admin/Product");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            AddViewModel model = new AddViewModel();

            List<int> ingredinetsIds = new List<int>();

            var record = context.Products.Include("ProductIngredients").FirstOrDefault(x => x.Id == id);
            record.ProductIngredients.ToList().ForEach(result => ingredinetsIds.Add(result.IngredientId));

            model.drpIngredients = context.Ingredients.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
            model.IngredientsIds = ingredinetsIds.ToArray();


            model.BrandSelectList = GetBrandSelectList();

            if (record == null)
            {
                TempData["message"] = "Error";
                return Redirect("~/Admin/Product");
            }
            
            model.Barcode = record.Barcode;
            model.BrandId = record.BrandId;
            model.CreatingDate = record.CreatingDate;
            model.Id = record.Id;
            model.ImageUrl = record.ImageUrl;
            model.LongDescription = record.LongDescription;
            model.SeoDescription = record.SeoDescription;
            model.SeoTitle = record.SeoTitle;
            model.SeoUrl = record.SeoUrl;
            model.ShortDescription = record.ShortDescription;
            model.Title = record.Title;
            model.Status = record.Status;
            model.UpdatedDate = record.UpdatedDate;


            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(AddViewModel model)
        {
            Product product = new Product();
            List<ProductIngredient> productIngredients = new List<ProductIngredient>();

            if (model.Id > 0)
            {
                product = context.Products.Include("ProductIngredients").FirstOrDefault(x => x.Id == model.Id);
                product.ProductIngredients.ToList().ForEach(result => productIngredients.Add(result));
                context.ProductIngredient.RemoveRange(productIngredients);
                context.SaveChanges();
                product.Title = model.Title;
                product.Barcode = model.Barcode;
                product.BrandId = model.BrandId;
                product.ShortDescription = model.ShortDescription;
                if (model.IngredientsIds.Length > 0)
                {
                    productIngredients = new List<ProductIngredient>();
                    foreach (var ingredientId in model.IngredientsIds)
                    {
                        productIngredients.Add(new ProductIngredient { IngredientId = ingredientId, ProductId = model.Id });
                    }
                    product.ProductIngredients = productIngredients;
                }
                if (model.File != null)
                {
                    var extension = Path.GetExtension(model.File.FileName);
                    var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(model.Title) + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/product/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    model.File.CopyTo(stream);
                    model.ImageUrl = "img/product/" + newImageName;
                    product.ImageUrl = model.ImageUrl;
                }
                if (model.SeoTitle == null) { model.SeoTitle = model.Title; }
                if (model.SeoUrl == null) { model.SeoUrl = SeoHelper.ConvertToValidUrl(model.Title); }
                if (model.SeoDescription == null) { model.SeoDescription = model.ShortDescription; }

                product.SeoTitle = model.SeoTitle;
                product.SeoUrl = model.SeoUrl;
                product.SeoDescription = Regex.Replace(model.SeoDescription, "<.*?>", string.Empty);
                product.UpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                context.SaveChanges();
            }
            else
            {
                if (model.IngredientsIds.Length > 0)
                {
                    foreach (var ingredientid in model.IngredientsIds)
                    {
                        productIngredients.Add(new ProductIngredient { IngredientId = ingredientid, ProductId = model.Id });
                    }
                    product.ProductIngredients = productIngredients;
                }
                context.Products.Add(product);
                context.SaveChanges();
            }

            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Ürün başarıyla güncellendi.";
            return Redirect("~/Admin/Product/Edit/" + model.Id);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            List<ProductIngredient> productIngredients = new List<ProductIngredient>();
            Product product = new Product();
            AddViewModel model = new AddViewModel();


            product = context.Products.Include("ProductIngredients").FirstOrDefault(x => x.Id == id);
            product.ProductIngredients.ToList().ForEach(result => productIngredients.Add(result));
            context.ProductIngredient.RemoveRange(productIngredients);
            context.SaveChanges();


            YediginiBil.Entities.Product record = _productService.GetById(id);
            if (record == null)
            {
                TempData["Message"] = "Error";
                TempData["Message_Detail"] = "Ürün bulunamadı.";
                return Redirect("~/Admin/Product");
            }

            product.Barcode = record.Barcode;
            product.BrandId = record.BrandId;
            product.CreatingDate = record.CreatingDate;
            product.Id = record.Id;
            product.ImageUrl = record.ImageUrl;
            product.LongDescription = record.LongDescription;
            product.SeoDescription = record.SeoDescription;
            product.SeoTitle = record.SeoTitle;
            product.SeoUrl = record.SeoUrl;
            product.ShortDescription = record.ShortDescription;
            product.Status = record.Status;
            product.Title = record.Title;
            product.UpdatedDate = record.UpdatedDate;

            _productService.Delete(product);

            if (record.ImageUrl != "img/nullimage.jpg")
            {
                string ExitingFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/", product.ImageUrl);
                System.IO.File.Delete(ExitingFile);
            }




            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Ürün silindi.";
            return Redirect("~/Admin/Product/");
        }





        [NonAction]
        private List<SelectListItem> GetIngredientSelectedList(int id)
        {
            return _productIngredientService.GetByProductId(id).Select(r => new SelectListItem() {Value=r.IngredientId.ToString(), Text = string.Format("{0}", r.Product.Title.ToString()) }).ToList();
        }
        [NonAction]
        private List<SelectListItem> GetIngredientSelectList()
        {
            return _ingredientService.GetAll().Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = string.Format("{0}", r.Title) }).ToList();
        }
        [NonAction]
        private List<SelectListItem> GetBrandSelectList()
        {
            return _brandService.GetAll().Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = string.Format("{0}", r.Title) }).ToList();
        }
    }
}

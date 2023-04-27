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
        private IBadgeService _badgeService;
        private IProductBadgeService _productBadgeService;
        private INutritiveService _nutritiveService;

        public ProductController(
            IProductIngredientService productIngredientService,
            IIngredientService ingredientService,
            IBrandService brandService, 
            IProductService productService,
            IBadgeService badgeService,
            IProductBadgeService productBadgeService,
            INutritiveService nutritiveService)
        {
            _productIngredientService = productIngredientService;
            _ingredientService = ingredientService;
            _brandService = brandService;
            _productService = productService;
            _badgeService = badgeService;
            _productBadgeService = productBadgeService;
            _nutritiveService = nutritiveService;
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
            model.BadgeSelectList = GetBadgeSelectList();

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
            if (model.SeoUrl == null) { model.SeoUrl = SeoHelper.ConvertToValidUrl(model.Title); }
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

            ProductBadge recordPB = new ProductBadge();
            for (int i = 0; i < model.BadgesIds.Count(); i++)
            {
                recordPB.Id = 0;
                recordPB.BadgeId = Convert.ToInt32(model.BadgesIds[i].ToString());
                recordPB.ProductId = productId;

                _productBadgeService.Create(recordPB);
            }

            Nutritive recordN = new Nutritive();

            for (int i = 0; i < model.nutritiveName.Count(); i++)
            {
                recordN.Id = 0;
                recordN.Name = model.nutritiveName[i].ToString();
                recordN.Value = model.nutritiveValue[i].ToString();
                recordN.ProductId = productId;
                recordN.Status = true;
                recordN.CreatingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                recordN.UpdatingDate = DateTime.Parse(DateTime.Now.ToShortDateString());

                _nutritiveService.Create(recordN);
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
            List<int> badgesIds = new List<int>();

            var record = context.Products.Include("ProductIngredients").FirstOrDefault(x => x.Id == id);
            record.ProductIngredients.ToList().ForEach(result => ingredinetsIds.Add(result.IngredientId));

            model.drpIngredients = context.Ingredients.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
            model.IngredientsIds = ingredinetsIds.ToArray();

            //
            record = context.Products.Include("ProductBadges").FirstOrDefault(x => x.Id == id);
            record.ProductBadges.ToList().ForEach(result => badgesIds.Add(result.BadgeId));

            model.drpBadges = context.Badges.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
            model.BadgesIds = badgesIds.ToArray();

            model.BrandSelectList = GetBrandSelectList();
            model.Nutritives = _nutritiveService.GetByProductId(id);

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
            List<ProductBadge> productBadges = new List<ProductBadge>();
            List<Nutritive> nutritives = new List<Nutritive>();

            if (model.Id > 0)
            {
                product = context.Products.Include("ProductIngredients").FirstOrDefault(x => x.Id == model.Id);
                product.ProductIngredients.ToList().ForEach(result => productIngredients.Add(result));
                context.ProductIngredient.RemoveRange(productIngredients);
                context.SaveChanges();

                product = context.Products.Include("ProductBadges").FirstOrDefault(x => x.Id == model.Id);
                product.ProductBadges.ToList().ForEach(result => productBadges.Add(result));
                context.ProductBadges.RemoveRange(productBadges);
                context.SaveChanges();

                product = context.Products.Include("Nutritives").FirstOrDefault(x => x.Id == model.Id);
                product.Nutritives.ToList().ForEach(result => nutritives.Add(result));
                context.Nutritives.RemoveRange(nutritives);
                context.SaveChanges();


                product.Title = model.Title;
                product.Barcode = model.Barcode;
                product.BrandId = model.BrandId;
                product.ShortDescription = model.ShortDescription;

                if (model.IngredientsIds != null)
                {
                    productIngredients = new List<ProductIngredient>();
                    foreach (var ingredientId in model.IngredientsIds)
                    {
                        productIngredients.Add(new ProductIngredient { IngredientId = ingredientId, ProductId = model.Id });
                    }
                    product.ProductIngredients = productIngredients;
                }

                if (model.BadgesIds != null)
                {
                    productBadges = new List<ProductBadge>();
                    foreach (var badgeId in model.BadgesIds)
                    {
                        productBadges.Add(new ProductBadge { BadgeId = badgeId, ProductId = model.Id });
                    }
                    product.ProductBadges = productBadges;
                }

                if (model.nutritiveName != null)
                {
                    nutritives = new List<Nutritive>();
                    for (int i = 0; i < model.nutritiveName.Count(); i++)
                    {
                        nutritives.Add(new Nutritive { 
                            Name = model.nutritiveName[i], 
                            CreatingDate= DateTime.Parse(DateTime.Now.ToShortDateString()),
                            ProductId=model.Id,
                            Status=true,
                            UpdatingDate=DateTime.Parse(DateTime.Now.ToShortDateString()),
                            Value=model.nutritiveValue[i]
                        });
                    }
                    product.Nutritives = nutritives;
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

                if (model.BadgesIds.Length > 0)
                {
                    foreach (var badgesId in model.BadgesIds)
                    {
                        productBadges.Add(new ProductBadge { BadgeId = badgesId, ProductId = model.Id });
                    }
                    product.ProductBadges = productBadges;
                }
                if (model.nutritiveName != null)
                {
                    nutritives = new List<Nutritive>();
                    for (int i = 0; i < model.nutritiveName.Count(); i++)
                    {
                        nutritives.Add(new Nutritive
                        {
                            Name = model.nutritiveName[i],
                            CreatingDate = DateTime.Parse(DateTime.Now.ToShortDateString()),
                            ProductId = model.Id,
                            Status = true,
                            UpdatingDate = DateTime.Parse(DateTime.Now.ToShortDateString()),
                            Value = model.nutritiveValue[i]
                        });
                    }
                    product.Nutritives = nutritives;
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
            List<ProductBadge> productBadges = new List<ProductBadge>();
            List<Nutritive> nutritives = new List<Nutritive>();


            product = context.Products.Include("ProductIngredients").FirstOrDefault(x => x.Id == id);
            product.ProductIngredients.ToList().ForEach(result => productIngredients.Add(result));
            context.ProductIngredient.RemoveRange(productIngredients);
            context.SaveChanges();

            product = context.Products.Include("ProductBadges").FirstOrDefault(x => x.Id == model.Id);
            product.ProductBadges.ToList().ForEach(result => productBadges.Add(result));
            context.ProductBadges.RemoveRange(productBadges);
            context.SaveChanges();

            product = context.Products.Include("Nutritives").FirstOrDefault(x => x.Id == model.Id);
            product.Nutritives.ToList().ForEach(result => nutritives.Add(result));
            context.Nutritives.RemoveRange(nutritives);
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
        private List<SelectListItem> GetIngredientSelectList()
        {
            return _ingredientService.GetAll().Where(x => x.Status == true).Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = string.Format("{0}", r.Title) }).ToList();
        }
        [NonAction]
        private List<SelectListItem> GetBrandSelectList()
        {
            return _brandService.GetAll().Where(x => x.Status == true).Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = string.Format("{0}", r.Title) }).ToList();
        }
        [NonAction]
        private List<SelectListItem> GetBadgeSelectList()
        {
            return _badgeService.GetAll().Where(x=>x.Status==true).Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = string.Format("{0}", r.Title) }).ToList();
        }
    }
}

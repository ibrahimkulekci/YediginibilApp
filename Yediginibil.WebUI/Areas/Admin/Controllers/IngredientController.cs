using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yediginibil.WebUI.Areas.Admin.Models;
using Yediginibil.WebUI.Areas.Admin.Models.Ingredient;
using YediginiBil.Business.Abstract;
using YediginiBil.Business.Common;
using YediginiBil.Business.Model;
using YediginiBil.Entities;

namespace Yediginibil.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IngredientController : Controller
    {
        private IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }
        [HttpGet]
        public IActionResult Index(int page=1)
        {
            const int pageSize = 20;
            return View(new ListViewModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = _ingredientService.GetAllCount(),
                    CurrentPage = page,
                    ItemsPerPage = pageSize
                },
                Ingredients = _ingredientService.GetAll(page, pageSize).OrderByDescending(x => x.Id).ToList()
            });
        }
        [HttpGet]
        public IActionResult Add()
        {
            AddViewModel model = new AddViewModel();
            model.Status = true;

            return View(model);
        }
        [HttpPost]
        public IActionResult Add(AddViewModel model)
        {
            if (model.File != null)
            {
                var extension = Path.GetExtension(model.File.FileName);
                var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(model.Title) + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Ingredient/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                model.File.CopyTo(stream);
                model.Image = "img/Ingredient/" + newImageName;
            }
            else
            {
                model.Image = "img/nullimage.jpg";
            }

            YediginiBil.Entities.Ingredient record = new YediginiBil.Entities.Ingredient();

            record.Title = model.Title;
            record.Description = model.Description;
            record.Image = model.Image;
            record.Status = model.Status;

            if (model.SeoTitle == null) { model.SeoTitle = model.Title; }
            if (model.SeoUrl == null) { model.SeoUrl = SeoHelper.ConvertToValidUrl(model.Title); }
            if (model.SeoDescription == null) { model.SeoDescription = model.Description; }

            record.SeoTitle = model.SeoTitle;
            record.SeoUrl = model.SeoUrl;
            record.SeoDescription = Regex.Replace(model.SeoDescription, "<.*?>", string.Empty);
            record.CreatingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            record.UpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());

            _ingredientService.Create(record);
            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Malzeme başarıyla eklendi.";
            return Redirect("~/Admin/Ingredient");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            AddViewModel model = new AddViewModel();

            YediginiBil.Entities.Ingredient record = _ingredientService.GetById(id);
            if (record == null)
            {
                TempData["Message"] = "Error";
                TempData["Message_Detail"] = "Malzeme bulunamadı.";
                return Redirect("~/Admin/Ingredient");
            }
                       
            model.Id = record.Id;
            model.Image = record.Image;
            model.Description = record.Description;
            model.SeoDescription = record.SeoDescription;
            model.SeoTitle = record.SeoTitle;
            model.SeoUrl = record.SeoUrl;
            model.Title = record.Title;
            model.Status = record.Status;
            model.CreatingDate = record.CreatingDate;
            model.UpdatedDate = record.UpdatedDate;

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(AddViewModel model)
        {
            YediginiBil.Entities.Ingredient record = _ingredientService.GetById(model.Id);
            if (record == null)
            {
                TempData["Message"] = "Error";
                TempData["Message_Detail"] = "Malzeme bulunamadı.";
                return Redirect("~/Admin/Ingredient");
            }

            if (model.File != null)
            {
                var extension = Path.GetExtension(model.File.FileName);
                var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(model.Title) + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Ingredient/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                model.File.CopyTo(stream);
                model.Image = "img/Ingredient/" + newImageName;
                record.Image = model.Image;
            }

            record.Title = model.Title;
            record.Description = model.Description;
            record.Status = model.Status;

            record.SeoTitle = model.Title;
            record.SeoUrl = SeoHelper.ConvertToValidUrl(model.Title);
            record.SeoDescription = model.SeoDescription;
            record.UpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());

            _ingredientService.Update(record);

            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Malzeme güncellendi.";
            return Redirect("~/Admin/Ingredient/Edit/"+model.Id);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            AddViewModel model = new AddViewModel();

            YediginiBil.Entities.Ingredient record = _ingredientService.GetById(id);
            if (record == null)
            {
                TempData["Message"] = "Error";
                TempData["Message_Detail"] = "Malzeme bulunamadı.";
                return Redirect("~/Admin/Ingredient");
            }

            model.Id = record.Id;
            model.Image = record.Image;
            model.Description = record.Description;
            model.SeoDescription = record.SeoDescription;
            model.SeoTitle = record.SeoTitle;
            model.SeoUrl = record.SeoUrl;
            model.Title = record.Title;
            model.Status = record.Status;
            model.CreatingDate = record.CreatingDate;
            model.UpdatedDate = record.UpdatedDate;

            _ingredientService.Delete(record);

            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Malzeme silindi.";
            return Redirect("~/Admin/Ingredient/");
        }

    }
}

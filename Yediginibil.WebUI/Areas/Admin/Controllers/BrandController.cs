using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yediginibil.WebUI.Areas.Admin.Models;
using Yediginibil.WebUI.Areas.Admin.Models.Brand;
using YediginiBil.Business.Abstract;
using YediginiBil.Business.Common;

namespace Yediginibil.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            const int pageSize = 20;
            return View(new ListViewModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = _brandService.GetAllCount(),
                    CurrentPage = page,
                    ItemsPerPage = pageSize
                },
                Brands = _brandService.GetAll(page, pageSize).OrderByDescending(x => x.Id).ToList()
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
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/brand/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                model.File.CopyTo(stream);
                model.Image = "img/brand/" + newImageName;
            }
            else
            {
                model.Image = "img/nullimage.jpg";
            }

            YediginiBil.Entities.Brand record = new YediginiBil.Entities.Brand();

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

            _brandService.Create(record);
            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Marka başarıyla eklendi.";
            return Redirect("~/Admin/Brand");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            AddViewModel model = new AddViewModel();

            YediginiBil.Entities.Brand record = _brandService.GetById(id);
            if (record == null)
            {
                TempData["Message"] = "Error";
                TempData["Message_Detail"] = "Marka bulunamadı.";
                return Redirect("~/Admin/Brand");
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
            YediginiBil.Entities.Brand record = _brandService.GetById(model.Id);
            if (record == null)
            {
                TempData["Message"] = "Error";
                TempData["Message_Detail"] = "Marka bulunamadı.";
                return Redirect("~/Admin/Brand");
            }

            if (model.File != null)
            {
                var extension = Path.GetExtension(model.File.FileName);
                var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(model.Title) + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/brand/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                model.File.CopyTo(stream);
                model.Image = "img/brand/" + newImageName;
                record.Image = model.Image;
            }

            record.Title = model.Title;
            record.Description = model.Description;
            record.Status = model.Status;

            record.SeoTitle = model.Title;
            record.SeoUrl = SeoHelper.ConvertToValidUrl(model.Title);
            record.SeoDescription = model.SeoDescription;
            record.UpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());

            _brandService.Update(record);

            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Marka güncellendi.";
            return Redirect("~/Admin/Brand/Edit/" + model.Id);
        }
        public IActionResult Delete(int id)
        {
            AddViewModel model = new AddViewModel();

            YediginiBil.Entities.Brand record = _brandService.GetById(id);
            if (record == null)
            {
                TempData["Message"] = "Error";
                TempData["Message_Detail"] = "Marka bulunamadı.";
                return Redirect("~/Admin/Brand");
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

            _brandService.Delete(record);

            if (record.Image != "img/nullimage.jpg")
            {
                string ExitingFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/", record.Image);
                System.IO.File.Delete(ExitingFile);
            }

            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Marka silindi.";
            return Redirect("~/Admin/Brand/");
        }

    }
}

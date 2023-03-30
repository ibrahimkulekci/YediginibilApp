using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yediginibil.WebUI.Areas.Admin.Models;
using Yediginibil.WebUI.Areas.Admin.Models.Page;
using YediginiBil.Business.Abstract;
using YediginiBil.Business.Common;

namespace Yediginibil.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PageController : Controller
    {
        private IPageService _pageService;

        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }

        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            const int pageSize = 20;
            return View(new ListViewModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = _pageService.GetAllCount(),
                    CurrentPage = page,
                    ItemsPerPage = pageSize
                },
                Pages = _pageService.GetAll(page, pageSize).OrderByDescending(x => x.Id).ToList()
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
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/page/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                model.File.CopyTo(stream);
                model.Image = "img/page/" + newImageName;
            }
            else
            {
                model.Image = "img/nullimage.jpg";
            }

            YediginiBil.Entities.Page record = new YediginiBil.Entities.Page();

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

            _pageService.Create(record);
            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Sayfa başarıyla eklendi.";
            return Redirect("~/Admin/Page");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            AddViewModel model = new AddViewModel();

            YediginiBil.Entities.Page record = _pageService.GetById(id);
            if (record == null)
            {
                TempData["Message"] = "Error";
                TempData["Message_Detail"] = "Sayfa bulunamadı.";
                return Redirect("~/Admin/Page");
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
            YediginiBil.Entities.Page record = _pageService.GetById(model.Id);
            if (record == null)
            {
                TempData["Message"] = "Error";
                TempData["Message_Detail"] = "Sayfa bulunamadı.";
                return Redirect("~/Admin/Page");
            }

            if (model.File != null)
            {
                var extension = Path.GetExtension(model.File.FileName);
                var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(model.Title) + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/page/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                model.File.CopyTo(stream);
                model.Image = "img/page/" + newImageName;
                record.Image = model.Image;
            }

            record.Title = model.Title;
            record.Description = model.Description;
            record.Status = model.Status;

            record.SeoTitle = model.Title;
            record.SeoUrl = SeoHelper.ConvertToValidUrl(model.Title);
            record.SeoDescription = model.SeoDescription;
            record.UpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());

            _pageService.Update(record);

            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Sayfa güncellendi.";
            return Redirect("~/Admin/Page/Edit/" + model.Id);
        }
        public IActionResult Delete(int id)
        {
            AddViewModel model = new AddViewModel();

            YediginiBil.Entities.Page record = _pageService.GetById(id);
            if (record == null)
            {
                TempData["Message"] = "Error";
                TempData["Message_Detail"] = "Sayfa bulunamadı.";
                return Redirect("~/Admin/Page");
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

            _pageService.Delete(record);

            if (record.Image != "img/nullimage.jpg")
            {
                string ExitingFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/", record.Image);
                System.IO.File.Delete(ExitingFile);
            }

            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Sayfa silindi.";
            return Redirect("~/Admin/Page/");
        }


    }
}

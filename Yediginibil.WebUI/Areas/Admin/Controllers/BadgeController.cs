using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Yediginibil.WebUI.Areas.Admin.Models;
using Yediginibil.WebUI.Areas.Admin.Models.Badge;
using YediginiBil.Business.Abstract;
using YediginiBil.Business.Common;

namespace Yediginibil.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BadgeController : Controller
    {
        private IBadgeService _badgeService;

        public BadgeController(IBadgeService badgeService)
        {
            _badgeService = badgeService;
        }

        public IActionResult Index(int page=1)
        {
            const int pageSize = 20;
            return View(new ListViewModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = _badgeService.GetAllCount(),
                    CurrentPage = page,
                    ItemsPerPage = pageSize
                },
                Badges = _badgeService.GetAll(page, pageSize).OrderByDescending(x => x.Id).ToList()
            });
        }
        [HttpGet]
        public IActionResult Add()
        {
            Yediginibil.WebUI.Areas.Admin.Models.Badge.AddViewModel model = new AddViewModel();
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
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/badge/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                model.File.CopyTo(stream);
                model.Image = "img/badge/" + newImageName;
            }
            else
            {
                model.Image = "img/nullimage.jpg";
            }

            YediginiBil.Entities.Badge record = new YediginiBil.Entities.Badge();

            record.Title = model.Title;
            record.Description = model.Description;
            record.Image = model.Image;
            record.Status = model.Status;
            record.CreatingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            record.UpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());

            _badgeService.Create(record);
            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Rozet başarıyla eklendi.";
            return Redirect("~/Admin/Badge");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            AddViewModel model = new AddViewModel();

            YediginiBil.Entities.Badge record = _badgeService.GetById(id);
            if (record == null)
            {
                TempData["Message"] = "Error";
                TempData["Message_Detail"] = "Rozet bulunamadı.";
                return Redirect("~/Admin/Badge");
            }

            model.Id = record.Id;
            model.Image = record.Image;
            model.Description = record.Description;
            model.Title = record.Title;
            model.Status = record.Status;
            model.CreatingDate = record.CreatingDate;
            model.UpdatedDate = record.UpdatedDate;

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(AddViewModel model)
        {
            YediginiBil.Entities.Badge record = _badgeService.GetById(model.Id);
            if (record == null)
            {
                TempData["Message"] = "Error";
                TempData["Message_Detail"] = "Rozet bulunamadı.";
                return Redirect("~/Admin/Badge");
            }

            if (model.File != null)
            {
                var extension = Path.GetExtension(model.File.FileName);
                var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(model.Title) + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/badge/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                model.File.CopyTo(stream);
                model.Image = "img/badge/" + newImageName;
                record.Image = model.Image;
            }

            record.Title = model.Title;
            record.Description = model.Description;
            record.Status = model.Status;

            record.UpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());

            _badgeService.Update(record);

            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Rozet güncellendi.";
            return Redirect("~/Admin/Badge/Edit/" + model.Id);
        }
        public IActionResult Delete(int id)
        {
            AddViewModel model = new AddViewModel();

            YediginiBil.Entities.Badge record = _badgeService.GetById(id);
            if (record == null)
            {
                TempData["Message"] = "Error";
                TempData["Message_Detail"] = "Rozet bulunamadı.";
                return Redirect("~/Admin/Blog");
            }

            model.Id = record.Id;
            model.Image = record.Image;
            model.Description = record.Description;
            model.Title = record.Title;
            model.Status = record.Status;
            model.CreatingDate = record.CreatingDate;
            model.UpdatedDate = record.UpdatedDate;

            _badgeService.Delete(record);

            if (record.Image != "img/nullimage.jpg")
            {
                string ExitingFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/", record.Image);
                System.IO.File.Delete(ExitingFile);
            }

            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Rozet silindi.";
            return Redirect("~/Admin/Badge/");
        }
    }
}

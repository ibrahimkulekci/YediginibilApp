﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yediginibil.WebUI.Areas.Admin.Models;
using Yediginibil.WebUI.Areas.Admin.Models.Blog;
using YediginiBil.Business.Abstract;
using YediginiBil.Business.Common;

namespace Yediginibil.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IBlogCategoryService _blogCategoryService;

        public BlogController(IBlogService blogService, IBlogCategoryService blogCategoryService)
        {
            _blogService = blogService;
            _blogCategoryService = blogCategoryService;
        }

        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            const int pageSize = 20;
            return View(new ListViewModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = _blogService.GetAllCount(),
                    CurrentPage = page,
                    ItemsPerPage = pageSize
                },
                Blogs = _blogService.GetAll(page, pageSize).OrderByDescending(x => x.Id).ToList()
            });
        }
        [HttpGet]
        public IActionResult Add()
        {
            AddViewModel model = new AddViewModel();
            model.BlogCategorySelectList = GetBlogCategorySelectList();
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
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/blog/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                model.File.CopyTo(stream);
                model.Image = "img/blog/" + newImageName;
            }
            else
            {
                model.Image = "img/nullimage.jpg";
            }

            YediginiBil.Entities.Blog record = new YediginiBil.Entities.Blog();

            record.Title = model.Title;
            record.CategoryId = model.CategoryId;
            record.Description = model.Description;
            record.Image = model.Image;
            record.Status = model.Status;

            if (model.SeoTitle == null) { model.SeoTitle = model.Title; }
            if (model.SeoUrl == null) { model.SeoUrl = SeoHelper.ConvertToValidUrl(model.Title); }
            if (model.SeoDescription == null) { model.SeoDescription = model.Description.Substring(0,150); }

            record.SeoTitle = model.SeoTitle;
            record.SeoUrl = model.SeoUrl;
            record.SeoDescription = Regex.Replace(model.SeoDescription.Substring(0,150), "<.*?>", string.Empty);
            record.CreatingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            record.UpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());

            _blogService.Create(record);
            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Blog başarıyla eklendi.";
            return Redirect("~/Admin/Blog");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            AddViewModel model = new AddViewModel();
            model.BlogCategorySelectList = GetBlogCategorySelectList();

            YediginiBil.Entities.Blog record = _blogService.GetById(id);
            if (record == null)
            {
                TempData["Message"] = "Error";
                TempData["Message_Detail"] = "Blog bulunamadı.";
                return Redirect("~/Admin/Blog");
            }

            model.Id = record.Id;
            model.Image = record.Image;
            model.Description = record.Description;
            model.SeoDescription = record.SeoDescription;
            model.SeoTitle = record.SeoTitle;
            model.SeoUrl = record.SeoUrl;
            model.Title = record.Title;
            model.CategoryId = record.CategoryId;
            model.Status = record.Status;
            model.CreatingDate = record.CreatingDate;
            model.UpdatedDate = record.UpdatedDate;

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(AddViewModel model)
        {
            YediginiBil.Entities.Blog record = _blogService.GetById(model.Id);
            if (record == null)
            {
                TempData["Message"] = "Error";
                TempData["Message_Detail"] = "Blog bulunamadı.";
                return Redirect("~/Admin/Blog");
            }

            if (model.File != null)
            {
                var extension = Path.GetExtension(model.File.FileName);
                var newImageName = Guid.NewGuid() + "-" + SeoHelper.ConvertToValidUrl(model.Title) + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/blog/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                model.File.CopyTo(stream);
                model.Image = "img/blog/" + newImageName;
                record.Image = model.Image;
            }

            record.Title = model.Title;
            record.CategoryId = model.CategoryId;
            record.Description = model.Description;
            record.Status = model.Status;

            record.SeoTitle = model.Title;
            record.SeoUrl = SeoHelper.ConvertToValidUrl(model.Title);
            record.SeoDescription = model.SeoDescription;
            record.UpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());

            _blogService.Update(record);

            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Blog güncellendi.";
            return Redirect("~/Admin/Blog/Edit/" + model.Id);
        }
        public IActionResult Delete(int id)
        {
            AddViewModel model = new AddViewModel();

            YediginiBil.Entities.Blog record = _blogService.GetById(id);
            if (record == null)
            {
                TempData["Message"] = "Error";
                TempData["Message_Detail"] = "Blog bulunamadı.";
                return Redirect("~/Admin/Blog");
            }

            model.Id = record.Id;
            model.Image = record.Image;
            model.Description = record.Description;
            model.SeoDescription = record.SeoDescription;
            model.SeoTitle = record.SeoTitle;
            model.SeoUrl = record.SeoUrl;
            model.Title = record.Title;
            model.CategoryId = record.CategoryId;
            model.Status = record.Status;
            model.CreatingDate = record.CreatingDate;
            model.UpdatedDate = record.UpdatedDate;

            _blogService.Delete(record);

            if (record.Image != "img/nullimage.jpg")
            {
                string ExitingFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/", record.Image);
                System.IO.File.Delete(ExitingFile);
            }

            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Blog silindi.";
            return Redirect("~/Admin/Blog/");
        }
        [HttpGet]
        public IActionResult CategoryList(int page = 1)
        {
            const int pageSize = 20;
            return View(new CategoryListViewModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = _blogCategoryService.GetAllCount(),
                    CurrentPage = page,
                    ItemsPerPage = pageSize
                },
                BlogCategories = _blogCategoryService.GetAll(page, pageSize).OrderByDescending(x => x.Id).ToList()
            });
        }

        [HttpGet]
        public IActionResult CategoryAdd()
        {
            CategoryAddViewModel model = new CategoryAddViewModel();
            model.Status = true;
            return View(model);
        }
        [HttpPost]
        public IActionResult CategoryAdd(CategoryAddViewModel model)
        {
            if (model.SeoTitle == null) { model.SeoTitle = model.Title; }
            if (model.SeoUrl == null) { model.SeoUrl = SeoHelper.ConvertToValidUrl(model.Title); }

            model.CreatingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            model.UpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());

            _blogCategoryService.Create(model);

            if (model.Id > 0)
            {
                TempData["Message"] = "Success";
                TempData["Message_Detail"] = "Kategori başarıyla eklendi.";
                return Redirect("~/Admin/Blog/CategoryList");
            }
            else
            {
                TempData["Message"] = "Error";
                TempData["Message_Detail"] = "Kategori eklenemedi.";
                return View(model);
            }

        }



        [NonAction]
        private List<SelectListItem> GetBlogCategorySelectList()
        {
            return _blogCategoryService.GetAll().Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = string.Format("{0}", r.Title) }).ToList();
        }


    }
}

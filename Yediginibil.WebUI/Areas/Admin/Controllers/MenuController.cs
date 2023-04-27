using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yediginibil.WebUI.Areas.Admin.Models;
using Yediginibil.WebUI.Areas.Admin.Models.Menu;
using YediginiBil.Business.Abstract;

namespace Yediginibil.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        private IMenuService _menuService;
        private IMenuLinkService _menuLinkService;

        public MenuController(
            IMenuService menuService,
            IMenuLinkService menuLinkService)
        {
            _menuService = menuService;
            _menuLinkService = menuLinkService;
        }

        public IActionResult Index(int page=1)
        {
            const int pageSize = 20;
            return View(new ListViewModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = _menuService.GetAllCount(),
                    CurrentPage = page,
                    ItemsPerPage = pageSize
                },
                Menus = _menuService.GetAll(page, pageSize).OrderByDescending(x => x.Id).ToList()
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
            YediginiBil.Entities.Menu record = new YediginiBil.Entities.Menu();

            record.Status = model.Status;
            record.Title = model.Title;

            _menuService.Create(record);
            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Menü başarıyla eklendi.";
            return Redirect("~/Admin/Menu");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            AddViewModel model = new AddViewModel();
            YediginiBil.Entities.Menu record = _menuService.GetById(id);
            if (record == null)
            {
                TempData["Message"] = "Error";
                TempData["Message_Detail"] = "Menü bulunamadı.";
                return Redirect("~/Admin/Menu");
            }

            model.Title = record.Title;
            model.Status = record.Status;
            model.Id = record.Id;

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(AddViewModel model)
        {
            YediginiBil.Entities.Menu record = _menuService.GetById(model.Id);
            if (record == null)
            {
                TempData["Message"] = "Error";
                TempData["Message_Detail"] = "Menü bulunamadı.";
                return Redirect("~/Admin/Menu");
            }

            record.Status = model.Status;
            record.Title = model.Title;

            _menuService.Update(record);

            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Menü güncellendi.";
            return Redirect("~/Admin/Menu/Edit/" + model.Id);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            AddViewModel model = new AddViewModel();
            YediginiBil.Entities.Menu record = _menuService.GetById(id);
            if (record == null)
            {
                TempData["Message"] = "Error";
                TempData["Message_Detail"] = "Menü bulunamadı.";
                return Redirect("~/Admin/Menu");
            }

            model.Title = record.Title;
            model.Status = record.Status;
            model.Id = record.Id;

            _menuService.Delete(record);

            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Menü silindi.";
            return Redirect("~/Admin/Menu/");
        }
        [HttpGet]
        public IActionResult ListLink(int id,int page=1)
        {

            YediginiBil.Entities.Menu record = _menuService.GetById(id);


            int pageSize = 20;
            return View(new ListViewModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = _menuLinkService.GetAllCount(),
                    CurrentPage = page,
                    ItemsPerPage = pageSize
                },
                MenuLinks = _menuLinkService.GetAll(page, pageSize).Where(x=>x.MenuId==id).OrderByDescending(x => x.Id).ToList(),
                MenuTitle=record.Title,
                MenuId=record.Id
            });
        }
        [HttpGet]
        public IActionResult AddLink(int id)
        {
            AddLinkAddViewModel model = new AddLinkAddViewModel();            
            model.ParentMenuSelectList = GetParentMenuSelectList(id);
            model.ParentMenuSelectList.Add(new SelectListItem() { Text = "Üst Kategori Yok", Value = "0" });
            model.MenuId = id;

            return View(model);
        }
        [HttpPost]
        public IActionResult AddLink(AddLinkAddViewModel model)
        {
            YediginiBil.Entities.MenuLink record = new YediginiBil.Entities.MenuLink();

            record.MenuId = model.MenuId;
            record.ParentId = model.ParentId;
            record.Position = model.Position;
            record.Title = model.Title;
            record.Url = model.Url;

            _menuLinkService.Create(record);

            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Menü elemanı eklendi.";
            return Redirect("~/Admin/Menu/ListLink/"+model.MenuId);

        }
        [HttpGet]
        public IActionResult EditLink(int id)
        {
            var record = _menuLinkService.GetById(id);

            if (record == null)
            {
                TempData["Message"] = "Error";
                TempData["Message_Detail"] = "Menü elemanı bulunamadı.";
                return Redirect("~/Admin/Menu/ListLink/"+id);
            }

            AddLinkAddViewModel model = new AddLinkAddViewModel();
            model.ParentMenuSelectList = GetParentMenuSelectList(record.MenuId);
            model.ParentMenuSelectList.Add(new SelectListItem() { Text = "Üst Kategori Yok", Value = "0" });

            model.Id = record.Id;
            model.MenuId = record.MenuId;
            model.ParentId = record.ParentId;
            model.Position = record.Position;
            model.Title = record.Title;
            model.Url = record.Url;

            return View(model);
        }
        [HttpPost]
        public IActionResult EditLink(AddLinkAddViewModel model)
        {
            YediginiBil.Entities.MenuLink record = _menuLinkService.GetById(model.Id);
            if (record == null)
            {
                TempData["Message"] = "Error";
                TempData["Message_Detail"] = "Menü elemanı bulunamadı.";
                return Redirect("~/Admin/Menu/ListLink/" + model.MenuId);
            }
            record.ParentId = model.ParentId;
            record.Position = model.Position;
            record.Title = model.Title;
            record.Url = model.Url;

            _menuLinkService.Update(record);

            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Menü elemanı güncellendi.";
            return Redirect("~/Admin/Menu/EditLink/" + model.Id);
        }
        [HttpGet]
        public IActionResult DeleteLink(int id)
        {
            AddLinkAddViewModel model = new AddLinkAddViewModel();
            YediginiBil.Entities.MenuLink record = _menuLinkService.GetById(id);
            if (record == null)
            {
                TempData["Message"] = "Error";
                TempData["Message_Detail"] = "Menü elemanı bulunamadı.";
                return Redirect("~/Admin/Menu");
            }

            model.Id = record.Id;
            model.MenuId = record.MenuId;
            model.ParentId = record.ParentId;
            model.Position = record.Position;
            model.Title = record.Title;
            model.Url = record.Url;

            _menuLinkService.Delete(model);

            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Menü elemanı silindi.";
            return Redirect("~/Admin/Menu/ListLink/"+model.MenuId);
        }



        [NonAction]
        private List<SelectListItem> GetParentMenuSelectList(int menuId)
        {
            return _menuLinkService.GetAll().Where(x => x.MenuId == menuId).Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = string.Format("{0}", r.Title) }).ToList();
        }
    }
}

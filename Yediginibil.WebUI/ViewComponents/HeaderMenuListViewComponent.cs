using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YediginiBil.Business.Abstract;

namespace Yediginibil.WebUI.ViewComponents
{
    public class HeaderMenuListViewComponent:ViewComponent
    {
        private IMenuLinkService _menuLinkService;

        public HeaderMenuListViewComponent(IMenuLinkService menuLinkService)
        {
            _menuLinkService = menuLinkService;
        }

        public IViewComponentResult Invoke(int id)
        {
            var values = _menuLinkService.GetAll().OrderBy(x => x.Position).Where(x => x.MenuId == id);
            return View(values);
        }

    }
}

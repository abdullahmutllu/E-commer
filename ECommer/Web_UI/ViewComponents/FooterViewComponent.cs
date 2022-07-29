using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_UI.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        public FooterViewComponent()
        {

        }
        public IViewComponentResult Invoke()
        {


            return View();
        }
    }
}

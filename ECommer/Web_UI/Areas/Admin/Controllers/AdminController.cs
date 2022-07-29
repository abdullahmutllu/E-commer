using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

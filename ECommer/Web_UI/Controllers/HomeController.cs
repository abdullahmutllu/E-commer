using BLL.Abstarct;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_UI.Models;

namespace Web_UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService categoryService;

        public HomeController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        public IActionResult Index()
        {
            HomePageViewModel model
                = new HomePageViewModel();

            var result = categoryService.GetList();
            switch (result.resultType)
            {
                case CORE.Business.ResultTypes.ResultType.Success:
                    model.Categories = result.data.ToList();
                    break;
                case CORE.Business.ResultTypes.ResultType.Notfound:
                    break;
                case CORE.Business.ResultTypes.ResultType.Warning:
                    break;
                case CORE.Business.ResultTypes.ResultType.Error:
                    break;
                case CORE.Business.ResultTypes.ResultType.NotValidaiton:
                    break;
                default:
                    break;
            }


            return View(model);
        }
    }
}

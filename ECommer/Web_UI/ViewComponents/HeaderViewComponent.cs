using BLL.Abstarct;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_UI.Models;

namespace Web_UI.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ICategoryService categoryService;

        public HeaderViewComponent(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        public IViewComponentResult Invoke()
        {
            //ICategoryService categoryService =
            //    new CategoryManager(new EfCategoryRepo(new DAL.Concrete.EntityFramework.Database.GelsinBanaDbContext()));
            //Db işlemleri
            var result = categoryService.GetList();
            switch (result.resultType)
            {
                case CORE.Business.ResultTypes.ResultType.Success:
                    HeaderViewModel model = new HeaderViewModel();
                    model.Categories = result.data.ToList();
                    return View(model);
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
            return View();
        }
    }
}

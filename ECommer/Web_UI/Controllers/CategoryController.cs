using BLL.Abstarct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_UI.Controllers
{

    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public CategoryController(ICategoryService categoryService, IProductService productService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
        }
        public IActionResult Index(int id)
        {
            var result = productService.GetProductListByCategoryId(id);
            switch (result.resultType)
            {
                case CORE.Business.ResultTypes.ResultType.Success:
                    return View(result.data.ToList());
                case CORE.Business.ResultTypes.ResultType.Notfound:
                    return View(result.message); ;
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

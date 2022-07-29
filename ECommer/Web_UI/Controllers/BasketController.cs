using BLL.Abstarct;
using ENTİTY.Concrete.POCO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_UI.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService basketService;

        public BasketController(IBasketService basketService)
        {
            this.basketService = basketService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult AddBasket(int productId, int count = 1)
        {
            if (!User.Identity.IsAuthenticated)
            {

                return Json(new { Hata = "Login ol ", Code = "401" });
            }
            Basket basket = new Basket();
            basket.Count = count;
            basket.ProductId = productId;
            var userid = Request.Cookies["userid"];
            basket.AppUserId = Convert.ToInt32(userid);
            var result = basketService.BasketAddOrUpdate(basket).data;
            if (result)
            {
                var resultBasket = basketService.GetBasketDto(userid);
                switch (resultBasket.resultType)
                {
                    case CORE.Business.ResultTypes.ResultType.Success:
                        return Json(resultBasket.data);
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
                return Json(new { Hata = "Ekleme Başarılı", Code = "200" });
            }
            else
            {
                return Json(new { Hata = "Ekleme Başarısız", Code = "500" });
            }
        }
        [HttpGet]
        public IActionResult GetBasket()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userid = Request.Cookies["userid"];
                if (!string.IsNullOrEmpty(userid))
                {
                    var result = basketService.GetList(x => x.AppUserId == Convert.ToInt32(userid), "Product").data.ToList();
                }
            }

            return null;
        }


        [HttpGet]
        public JsonResult GetListBasket()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { Hata = "Login ol", Code = "401" });
            }

            var userid = Request.Cookies["userid"];
            var resultBasket = basketService.GetBasketDto(userid);
            switch (resultBasket.resultType)
            {
                case CORE.Business.ResultTypes.ResultType.Success:
                    return Json(resultBasket.data);
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
            return null;
        }
    }
}

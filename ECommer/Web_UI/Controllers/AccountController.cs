using BLL.Abstarct;
using ENTİTY.Concrete.POCO;
using ENTİTY.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using Web_UI.Models;

namespace Web_UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly UserManager<AppUser> userManager;

        public AccountController(
            IAccountService accountService,
           UserManager<AppUser> userManager
            )
        {
            this.accountService = accountService;
            this.userManager = userManager;
        }

        public IActionResult LogOut()
        {
            var result = accountService.LoginOut();
            switch (result.resultType)
            {
                case CORE.Business.ResultTypes.ResultType.Success:
                    return RedirectToAction("Index", "Home");
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

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> PasswordReset(ResetPasswordModel model)
        {
            AppUser user = await userManager.FindByEmailAsync(model.EMail);
            if (user != null)
            {
                string resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.To.Add(user.Email);
                mail.From = new MailAddress("*****", "Şifre sıfırlama", System.Text.Encoding.UTF8);
                mail.Subject = "Şifre sıfırlama talebi";
                mail.Body = $"<a target=\"_blank\" href=\"https://localhost:5001{Url.Action("UpdatePassword", "User", new { userId = user.Id, token = HttpUtility.UrlEncode(resetToken) })}\">Yeni şifre talebi için tıklayınız</a>";
                mail.IsBodyHtml = true;
                SmtpClient smp = new SmtpClient();
                smp.Credentials = new NetworkCredential("*******@gmail.com", "şifre");
                smp.Port = 587;
                smp.Host = "smtp.gmail.com";
                smp.EnableSsl = true;
                // smp.UseDefaultCredentials = true;

                smp.Send(mail);

                ViewBag.State = true;

            }
            else
            {
                ViewBag.State = false;
            }
            return View();
        }
        [HttpGet("[action]/{userId}/{token}")]
        public IActionResult UpdatePassword(string userId, string token)
        {
            return View();
        }
        [HttpPost("[action]/{userId}/{token}")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordModel model, string userId, string token)
        {
            AppUser user = await userManager.FindByIdAsync(userId);
            IdentityResult result = await userManager.ResetPasswordAsync(user, HttpUtility.UrlDecode(token), model.Password);
            if (result.Succeeded)
            {
                ViewBag.State = true;
                await userManager.UpdateSecurityStampAsync(user);
            }
            else
                ViewBag.State = false;
            return View();
        }
        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(AppUserloginDto model)
        {
            if (ModelState.IsValid)
            {
                var result = accountService.Login(model);
                switch (result.resultType)
                {
                    case CORE.Business.ResultTypes.ResultType.Success:
                        var userId = (int)accountService.GetUserId(model.UserName).data;
                        Response.Cookies.Append("userid", userId.ToString());
                        return RedirectToAction("Index", "Home");
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
            }
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register([FromForm] AppUserRegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser();
                appUser.Email = model.Email;
                appUser.UserName = model.UserName;
                appUser.PhoneNumber = model.Phone;
                var result = accountService.Register(appUser, model.Password);
                switch (result.resultType)
                {
                    case CORE.Business.ResultTypes.ResultType.Success:
                        return RedirectToAction(actionName: "Login");
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
            }
            else
            {

            }

            return View();
        }
    }
}

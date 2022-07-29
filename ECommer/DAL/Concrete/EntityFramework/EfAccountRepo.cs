using CORE.DAL;
using DAL.Abstract;
using ENTİTY.Concrete.POCO;
using ENTİTY.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete.EntityFramework
{
    public class EfAccountRepo : IAccountDAL
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public EfAccountRepo(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<IdentityResult> Register(AppUser appUser, string password)
        {
            //var result = await userManager.AddPasswordAsync(appUser, password);
            var result = await userManager.CreateAsync(appUser, password);
            if (result.Succeeded)
            {
                var user = await userManager.FindByNameAsync(appUser.UserName);
                var roleResult = await userManager.AddToRoleAsync(user, RoleEnums.User.ToString());
                if (roleResult.Succeeded)
                {
                    return result;
                }
            }

            return null;
        }
        public async Task<SignInResult> Login(AppUserloginDto model)
        {
            return await signInManager.PasswordSignInAsync(model.UserName, model.Password, true, true);
        }

        public async Task LoginOut()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<object> UserIdByUserName(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            return user.Id;
        }
    }
}

using ENTİTY.Concrete.POCO;
using ENTİTY.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IAccountDAL
    {
        Task<IdentityResult> Register(AppUser appUser, string password);
        Task<SignInResult> Login(AppUserloginDto model);
        Task LoginOut();
        Task<object> UserIdByUserName(string username);
    }
}

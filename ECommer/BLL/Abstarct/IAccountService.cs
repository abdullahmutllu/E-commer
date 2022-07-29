using CORE.Business.ResultTypes;
using ENTİTY.Concrete.POCO;
using ENTİTY.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstarct
{
    public interface IAccountService
    {
        ResultMessage<IdentityResult> Register(AppUser appUser, string password);
        ResultMessage<SignInResult> Login(AppUserloginDto model);
        ResultMessage LoginOut();
        ResultMessage<object> GetUserId(string username);
    }
}

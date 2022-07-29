using BLL.Abstarct;
using CORE.Business;
using CORE.Business.ResultTypes;
using DAL.Abstract;
using ENTİTY.Concrete.POCO;
using ENTİTY.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Conctere
{
    public class AccountManager : IAccountService
    {
        private readonly IAccountDAL accountDAL;

        public AccountManager(IAccountDAL accountDAL)
        {
            this.accountDAL = accountDAL;
        }

        public ResultMessage<object> GetUserId(string username)
        {
            try
            {
                var id = accountDAL.UserIdByUserName(username).Result;
                if ((int)id != 0)
                {
                    return new ResultMessage<object>(id, "Success");
                }
                return new ResultMessage<object>(null, "Notfound", ResultType.Notfound);
            }
            catch (Exception ex)
            {
                return new ResultMessage<object>(null, ex.ToInnest().Message, ResultType.Error);
            }
        }

        public ResultMessage<SignInResult> Login(AppUserloginDto model)
        {
            try
            {
                var result = accountDAL.Login(model).Result;
                if (result.Succeeded)
                {
                    return new ResultMessage<SignInResult>(result, "Succues", ResultType.Success);
                }
                return new ResultMessage<SignInResult>(null, "Warinig", ResultType.Warning);
            }
            catch (Exception ex)
            {
                return new ResultMessage<SignInResult>(null, ex.ToInnest().Message, ResultType.Error);
            }
        }

        public ResultMessage LoginOut()
        {
            try
            {
                accountDAL.LoginOut();
                return new ResultMessage("succues");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ResultMessage<IdentityResult> Register(AppUser appUser, string password)
        {
            try
            {
                var result = accountDAL.Register(appUser, password).Result;
                if (result.Succeeded)
                {
                    return new ResultMessage<IdentityResult>(result, "Succues", ResultType.Success);
                }
                return new ResultMessage<IdentityResult>(null, "Warinig", ResultType.Warning);
            }
            catch (Exception ex)
            {
                return new ResultMessage<IdentityResult>(null, ex.ToInnest().Message, ResultType.Error);
            }
        }
    }
}

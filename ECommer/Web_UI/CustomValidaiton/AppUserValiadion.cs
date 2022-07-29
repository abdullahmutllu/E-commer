using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_UI.CustomValidaiton
{
    public class AppUserValiadion : IdentityErrorDescriber
    {
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError { Code = "MevcutEmail", Description = $"{email} Email Adresi Kayıtlı" };
        }
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError { Code = "MevcutUserName", Description = $"{userName} Email Adresi Kayıtlı" };
        }

        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError { Code = "MevcutEmail", Description = $"Geçerli Mail Adresi Yazını!" };
        }
    }
}

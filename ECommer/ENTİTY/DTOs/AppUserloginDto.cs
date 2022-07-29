using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTİTY.DTOs
{
    public class AppUserloginDto
    {
        [Required(ErrorMessage = "Kullanıcı Adı ilşe Giriş Zorunludur")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Şifre alanı Zorunludur!!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

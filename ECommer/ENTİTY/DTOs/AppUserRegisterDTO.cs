using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTİTY.DTOs
{
    public class AppUserRegisterDTO
    {
        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("E-Posta")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Geçerli Mail Adresi Yazınız!")]
        public string Email { get; set; }
        [DisplayName("Telefon")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Şifre:")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Şifre Tekrar:")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}

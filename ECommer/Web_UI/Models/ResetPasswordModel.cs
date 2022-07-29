using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web_UI.Models
{
    public class ResetPasswordModel
    {
        [Display(Name = "E posta adresinizi giriniz")]
        [Required(ErrorMessage = "E posta adresini boş geçmeyiniz.")]
        [EmailAddress(ErrorMessage = "Uygun formatta e posta adresi giriniz.")]
        public string EMail { get; set; }
    }
}

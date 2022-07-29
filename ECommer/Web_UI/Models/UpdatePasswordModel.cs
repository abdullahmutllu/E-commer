using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web_UI.Models
{
    public class UpdatePasswordModel
    {
        [Display(Name = "Yeni şifre")]
        [Required(ErrorMessage = "Şifre giriniz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

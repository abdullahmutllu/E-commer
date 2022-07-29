using DAL.Abstract;
using ENTİTY.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    public class ProductDtoValidation : AbstractValidator<ProductCreateDTO>
    {
        private readonly IProductDAL productDAL;

        public ProductDtoValidation(IProductDAL productDAL)
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Boş Geçilmez!");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Boş Geçilmez!");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Boş Geçilmez!");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Max 500 Length");
            RuleFor(x => x.Name).Must(NameUniq);
            this.productDAL = productDAL;
        }

        public bool NameUniq(string name)
        {
            var result = productDAL.GetList(x => x.Name == name).Result.Count();
            if (result == 0)
            {
                return true;
            }
            return false;
        }
    }
}

using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class CategoryValidator:AbstractValidator<Category>
    {
        //CATEGORY SINIFIMIZIN DOĞRULAMA KURALLARINI,KISITLARINI BURADA OLUŞTURUYORUZ.

        public CategoryValidator() 
        {
            RuleFor(x=> x.CategoryName).NotEmpty().WithMessage("Kategori Adı Boş Geçilemez");
            RuleFor(x => x.CategoryDescription).NotEmpty().WithMessage("Açıklama Boş geçilemez");
            RuleFor(x => x.CategoryName).MinimumLength(3).WithMessage("En az üç karekter girişi yapınız");
        }
    }
}

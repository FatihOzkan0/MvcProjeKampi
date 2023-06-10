using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator() 
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar Adı Boş Geçilemez");
            RuleFor(x => x.WriterSurname).NotEmpty().WithMessage("Yazar Soyadı Boş geçilemez");
            RuleFor(x => x.WriterName).MinimumLength(3).WithMessage("En az iki karekter girişi yapınız");
            RuleFor(x => x.WriterAbout).NotEmpty().WithMessage("Hakkında kısmı Boş geçilemez");
            RuleFor(x => x.WriterTitle).NotEmpty().WithMessage("Yazar Unvan Boş geçilemez");
        }

    }
}

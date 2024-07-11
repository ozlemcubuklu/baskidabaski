using DTO.AppUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.AppUser
{
    public class AppUserRegisterValidator:AbstractValidator<AppUserRegister>
    {
        public AppUserRegisterValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim Alanı boş geçilemez.");
            RuleFor(x => x.SurName).NotEmpty().WithMessage("Soyisim Alanı boş geçilemez.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Alanı boş geçilemez.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password Alanı boş geçilemez.");
            RuleFor(x => x.ConfirmPasword).NotEmpty().WithMessage("Parola Tekrar Alanı boş geçilemez.");
            RuleFor(x => x.Name).MaximumLength(30).WithMessage("Lütfen en fazla 30 karakter giriniz.");
            RuleFor(x => x.Name).MinimumLength(2).WithMessage("Lütfen en az 2 karakter giriniz.");
            RuleFor(x => x.ConfirmPasword).Equal(y => y.Password).WithMessage("Parolalarınız eşleşmiyor.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Lütfen geçerli bir mail adresi giriniz.");




        }
    }
}

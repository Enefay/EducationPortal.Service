using EducationPortal.DtoLayer.UserDto;
using FluentValidation;

namespace EducationPortal.API.Validators.ProfileValidators
{
    public class UpdateProfileUserDtoValidator : AbstractValidator<UpdateProfileUserDto>
    {
        public UpdateProfileUserDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir kullanıcı ID girin");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email alanı boş olamaz")
                .EmailAddress().WithMessage("Geçerli bir email adresi girin");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("İsim alanı boş olamaz");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyisim alanı boş olamaz");

            RuleFor(x => x.Password)
                .MinimumLength(3).When(x => !string.IsNullOrEmpty(x.Password))
                .WithMessage("Şifre en az 3 karakter olmalıdır");
        }
    }
}

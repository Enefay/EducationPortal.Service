using EducationPortal.DtoLayer.EducationDto;
using FluentValidation;

namespace EducationPortal.API.Validators.EducationValidators
{
    public class CreateEducationDtoValidator : AbstractValidator<CreateEducationDto>
    {
        public CreateEducationDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş olamaz")
                .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş olamaz");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Kategori ID boş olamaz")
                .GreaterThan(0).WithMessage("Geçerli bir kategori seçin");

            RuleFor(x => x.InstructorId)
                .NotEmpty().WithMessage("Eğitmen ID boş olamaz")
                .GreaterThan(0).WithMessage("Geçerli bir eğitmen ID girin");

            RuleFor(x => x.Quota)
                .GreaterThan(0).WithMessage("Kontenjan 0 dan büyük olmalıdır");

            RuleFor(x => x.Cost)
                .GreaterThanOrEqualTo(0).WithMessage("Maliyet negatif olamaz");

            RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Başlangıç tarihi boş olamaz.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("Bitiş tarihi boş olamaz.")
                .GreaterThan(x => x.StartDate).WithMessage("Bitiş tarihi, başlangıç tarihinden sonra olmalıdır.");

            RuleFor(x => x)
                .Must(x => (x.EndDate - x.StartDate).Days >= 1)
                .WithMessage("Eğitim süresi en az 1 gün olmalıdır.");

            RuleFor(x => x.Contents)
                .NotEmpty().WithMessage("En az bir içerik girmelisiniz");
        }
    }
}

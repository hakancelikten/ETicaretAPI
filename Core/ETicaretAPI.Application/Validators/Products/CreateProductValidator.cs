using ETicaretAPI.Application.ViewModels.Products;
using FluentValidation;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull().WithMessage("Lütfen ürün adını boş geçmeyiniz")
                .MaximumLength(150)
                .MinimumLength(5).WithMessage("Lütfen ürün adının 5 ile 150 karakter arasında giriniz.");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull().WithMessage("Lütfen stok bilgisini boş geçmeyiniz")
                .Must(s => s >= 0).WithMessage("Lütfen stoğu giriniz");

            RuleFor(p => p.Price)
                 .NotEmpty()
                 .NotNull().WithMessage("Lütfen ücret bilgisini boş geçmeyiniz")
                 .Must(s => s >= 0).WithMessage("Lütfen ücreti giriniz");




        }
    }
}

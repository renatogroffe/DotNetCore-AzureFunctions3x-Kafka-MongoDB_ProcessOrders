using FluentValidation;
using FunctionAppProcessOrders.Models;

namespace FunctionAppProcessOrders.Validators
{
    public class OrderDataValidator : AbstractValidator<OrderData>
    {
        public OrderDataValidator()
        {
            RuleFor(o => o.CorrelationId).NotEmpty()
                .WithMessage("O campo 'CorrelationId' deve ser preenchido");

            RuleFor(o => o.When).NotEmpty()
                .WithMessage("O campo 'When' deve ser preenchido");

            RuleFor(o => o.Payload).NotNull()
                .SetValidator(new PayloadValidator());
        }

        class PayloadValidator : AbstractValidator<Payload>
        {
            public PayloadValidator()
            {
                RuleFor(p => p.CustomerName).NotEmpty()
                    .WithMessage("O campo 'CustomerName' deve ser preenchido");

                RuleFor(p => p.Total).NotEmpty()
                    .WithMessage("O campo 'Total' deve ser preenchido");

                RuleFor(p => p.ID).NotEmpty()
                    .WithMessage("O campo 'ID' deve ser preenchido");
            }
        }
    }
}
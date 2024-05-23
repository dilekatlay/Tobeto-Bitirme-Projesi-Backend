using Domain.Enums;
using FluentValidation;

namespace Application.Features.Orders.Commands.Create;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.MemberId).NotEmpty();
        RuleFor(c => c.UnitPrice).NotEmpty();
        RuleFor(c => c.OrderDate).NotEmpty().Must(orderDate => orderDate.Date == DateTime.Today);
        RuleFor(c => c.PaymentMethod).IsInEnum();
        RuleFor(c => c.OrderStatu).IsInEnum().Must(BePendingByDefault);
    }

    private bool BePendingByDefault(OrderStatu statu)
    {
        if (statu == null)
        {
            statu = OrderStatu.Pending;
            return true;
        }
        return true;
    }
}
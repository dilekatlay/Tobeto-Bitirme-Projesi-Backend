using Domain.Enums;
using FluentValidation;

namespace Application.Features.Orders.Commands.Update;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
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
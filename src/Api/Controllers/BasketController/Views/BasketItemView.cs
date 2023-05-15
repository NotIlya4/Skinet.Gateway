using Api.Swagger.Enrichers;

namespace Api.Controllers.BasketController.Views;

public class BasketItemView
{
    public Guid ProductId { get; }
    [Quantity]
    public int Quantity { get; }

    public BasketItemView(Guid productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }
}
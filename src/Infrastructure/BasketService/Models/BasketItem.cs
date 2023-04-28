namespace Infrastructure.BasketService.Models;

public class BasketItem
{
    public Guid ProductId { get; }
    public int Quantity { get; }

    public BasketItem(Guid productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }
}
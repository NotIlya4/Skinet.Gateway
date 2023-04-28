namespace Infrastructure.BasketService.Models;

public class Basket
{
    public Guid UserId { get; }
    public List<BasketItem> Items { get; }

    public Basket(Guid userId, List<BasketItem> items)
    {
        UserId = userId;
        Items = items;
    }
}
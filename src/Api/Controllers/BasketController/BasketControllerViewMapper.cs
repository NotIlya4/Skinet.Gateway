using Api.Controllers.BasketController.Views;
using Infrastructure.BasketService.Models;

namespace Api.Controllers.BasketController;

public class BasketControllerViewMapper
{
    public BasketItemView MapBasketItem(BasketItem basketItem)
    {
        return new BasketItemView(
            productId: basketItem.ProductId,
            quantity: basketItem.Quantity);
    }

    public List<BasketItemView> MapBasketItem(List<BasketItem> basketItems)
    {
        return basketItems.Select(MapBasketItem).ToList();
    }

    public BasketItem MapBasketItem(BasketItemView basketItemView)
    {
        return new BasketItem(
            productId: basketItemView.ProductId,
            quantity: basketItemView.Quantity);
    }

    public List<BasketItem> MapBasketItem(List<BasketItemView> basketItemViews)
    {
        return basketItemViews.Select(MapBasketItem).ToList();
    }
}
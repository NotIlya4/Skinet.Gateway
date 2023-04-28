using Infrastructure.BasketService.Models;

namespace Infrastructure.BasketService;

public interface IBasketService
{
    public Task Insert(Basket basket);
    public Task<Basket> Get(Guid userId);
}
using Api.Controllers.BasketController.Views;
using Api.Middlewares.JwtParserMiddleware;
using Infrastructure.AccountService;
using Infrastructure.AccountService.Models;
using Infrastructure.BasketService;
using Infrastructure.BasketService.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.BasketController;

[ApiController]
[Route("baskets")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;
    private readonly IAccountService _accountService;
    private readonly BasketControllerViewMapper _mapper;

    public BasketController(IBasketService basketService, IAccountService accountService, BasketControllerViewMapper mapper)
    {
        _basketService = basketService;
        _accountService = accountService;
        _mapper = mapper;
    }

    [HttpGet]
    [ParseJwtHeader]
    public async Task<ActionResult<List<BasketItemView>>> GetBasket()
    {
        var jwtStorage = new JwtTokenRequestStorage(HttpContext);
        UserInfo userInfo = await _accountService.GetUser(jwtStorage.ReadJwtToken());
        
        var basket = await _basketService.Get(userInfo.Id);
        List<BasketItemView> basketItemViews = _mapper.MapBasketItem(basket.Items);
        return Ok(basketItemViews);
    }

    [HttpPost]
    [ParseJwtHeader]
    public async Task<ActionResult> Insert([FromBody] List<BasketItemView> basketItemViews)
    {
        var jwtStorage = new JwtTokenRequestStorage(HttpContext);
        UserInfo userInfo = await _accountService.GetUser(jwtStorage.ReadJwtToken());

        var basket = new Basket(userInfo.Id, _mapper.MapBasketItem(basketItemViews));
        await _basketService.Insert(basket);
        return NoContent();
    }
}
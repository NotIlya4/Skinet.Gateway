using Api.Controllers.BasketController.Views;
using Api.Swagger.ProducesAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.BasketController;

[ApiController]
[Route("baskets")]
[ProducesInternalException]
public class BasketController : ControllerBase
{
    [HttpGet]
    [Authorize]
    [ProducesOk]
    public Task<ActionResult<List<BasketItemView>>> GetBasket()
    {
        return Task.FromResult<ActionResult<List<BasketItemView>>>(NoContent());
    }

    [HttpPost]
    [Authorize]
    [ProducesNoContent]
    public Task<ActionResult> Insert([FromBody] List<BasketItemView> basketItemViews)
    {
        return Task.FromResult<ActionResult>(NoContent());
    }
}
using Api.Controllers.ProductController.View;
using Api.Swagger.ProducesAttributes;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.ProductController;

[ApiController]
[Route("products")]
[ProducesInternalException]
public class ProductController : ControllerBase
{
    [HttpGet]
    [Route("brands")]
    [ProducesOk]
    public Task<ActionResult<List<string>>> GetBrands()
    {
        return Task.FromResult<ActionResult<List<string>>>(NoContent());
    }
    
    [HttpGet]
    [Route("product-types")]
    [ProducesOk]
    public Task<ActionResult<List<string>>> GetProductTypes()
    {
        return Task.FromResult<ActionResult<List<string>>>(NoContent());
    }

    [HttpGet]
    [ProducesOk]
    public Task<ActionResult<GetProductsResponseView>> GetProducts([FromQuery] ProductFilteringAndSortingView productFilteringAndSortingView)
    {
        return Task.FromResult<ActionResult<GetProductsResponseView>>(NoContent());
    }

    [HttpGet]
    [Route("id/{id}")]
    [ProducesOk]
    public Task<ActionResult<ProductView>> GetProductById(Guid id)
    {
        return Task.FromResult<ActionResult<ProductView>>(NoContent());
    }
}
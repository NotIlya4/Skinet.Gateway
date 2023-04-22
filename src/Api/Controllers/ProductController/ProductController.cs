using Api.Controllers.ProductController.Helpers;
using Api.Controllers.ProductController.View;
using Infrastructure.ProductService;
using Infrastructure.ProductService.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.ProductController;

[ApiController]
[Route("products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ProductControllerViewMapper _mapper;

    public ProductController(IProductService productService, ProductControllerViewMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("brands")]
    public async Task<ActionResult<List<string>>> GetBrands()
    {
        List<string> brands = await _productService.GetBrands();
        return Ok(brands);
    }
    
    [HttpGet]
    [Route("product-types")]
    public async Task<ActionResult<List<string>>> GetProductTypes()
    {
        List<string> productTypes = await _productService.GetProductTypes();
        return Ok(productTypes);
    }

    [HttpGet]
    public async Task<ActionResult<GetProductsResponseView>> GetProducts([FromQuery] ProductFilteringAndSortingView productFilteringAndSortingView)
    {
        GetProductsResponse getProductsResponse =
            await _productService.GetProducts(_mapper.MapProductFilteringAndSorting(productFilteringAndSortingView));
        GetProductsResponseView view = _mapper.MapGetProductsResponse(getProductsResponse);
        return Ok(view);
    }

    [HttpGet]
    [Route("id/{id}")]
    public async Task<ActionResult<ProductView>> GetProductById(Guid id)
    {
        Product product = await _productService.GetProduct(id);
        ProductView view = _mapper.MapProduct(product);
        return Ok(view);
    }
}
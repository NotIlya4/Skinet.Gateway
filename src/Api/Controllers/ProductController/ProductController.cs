using Api.Controllers.ProductController.View;
using Infrastructure;
using Infrastructure.HttpMappers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.ProductController;

[ApiController]
[Route("products")]
public class ProductController : ControllerBase
{
    private readonly HttpForwarder _forwarder;
    private readonly string _baseUrl;

    public ProductController(HttpForwarder forwarder, LinkProvider linkProvider)
    {
        _forwarder = forwarder;
        _baseUrl = linkProvider.ProductService;
    }

    [HttpGet]
    [Route("brands")]
    public async Task GetBrand()
    {
        await Forward();
    }
    
    [HttpGet]
    [Route("product-types")]
    public async Task GetProductTypes()
    {
        await Forward();
    }

    [HttpGet]
    public async Task GetProducts([FromQuery] GetProductsRequestView getProductsRequestView)
    {
        await Forward();
    }

    [HttpGet]
    [Route("id/{id}")]
    public async Task GetProductById(string id)
    {
        await Forward();
    }

    private async Task Forward()
    {
        await _forwarder.ForwardAndWriteToContext(HttpContext, _baseUrl);
    }
}
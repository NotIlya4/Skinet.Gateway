using Api.AccountController.Views;
using Infrastructure.HttpMappers;
using Microsoft.AspNetCore.Mvc;

namespace Api.AccountController;

[ApiController]
public class AccountController : ControllerBase
{
    private readonly HttpForwarder _forwarder;

    public AccountController(HttpForwarder forwarder)
    {
        _forwarder = forwarder;
    }
    
    [HttpPost]
    [Route("users/login")]
    public async Task Login(RegisterCredentials registerCredentials)
    {
        await _forwarder.ForwardAndWriteToContext(HttpContext, "http://localhost:5002");
    }

    [HttpGet]
    [Route("users/{propertyName}/{value}")]
    public async Task GetUser(string propertyName, string value)
    {
        await _forwarder.ForwardAndWriteToContext(HttpContext, "http://localhost:5002");
    }

    [HttpGet]
    [HttpPost]
    [HttpDelete]
    [HttpPatch]
    [HttpPut]
    [Route("products/{**catch-all}")]
    public async Task AnyProductServiceEndpoint()
    {
        await _forwarder.ForwardAndWriteToContext(HttpContext, "http://localhost:5001");
    }
}
using Api.Controllers.AccountController.Views;
using Infrastructure;
using Infrastructure.HttpMappers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AccountController;

[ApiController]
[Route("users")]
public class AccountController : ControllerBase
{
    private readonly HttpForwarder _forwarder;
    private readonly string _baseUrl;

    public AccountController(HttpForwarder forwarder, LinkProvider linkProvider)
    {
        _forwarder = forwarder;
        _baseUrl = linkProvider.AccountService;
    }
    
    [HttpPost]
    [Route("login")]
    public async Task Login(RegisterCredentials registerCredentials)
    {
        await Forward();
    }

    [HttpPost]
    [Route("register")]
    public async Task Register(RegisterCredentials registerCredentials)
    {
        await Forward();
    }
    
    [HttpPost]
    [Route("logout")]
    public async Task Logout(RegisterCredentials registerCredentials)
    {
        await Forward();
    }
    
    [HttpPost]
    [Route("updateJwtPair")]
    public async Task UpdateJwtPair(RefreshTokenIdentifierView refreshTokenIdentifierView)
    {
        await Forward();
    }

    [HttpGet]
    [Route("jwt/{value}")]
    public async Task GetUser(string value)
    {
        await Forward();
    }

    private async Task Forward()
    {
        await _forwarder.ForwardAndWriteToContext(HttpContext, _baseUrl);
    }
}
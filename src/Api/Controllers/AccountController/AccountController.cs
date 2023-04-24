using Api.Controllers.AccountController.Helpers;
using Api.Controllers.AccountController.Views;
using Api.Middlewares.JwtParserMiddleware;
using Infrastructure.AccountService;
using Infrastructure.AccountService.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AccountController;

[ApiController]
[Route("users")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly AccountControllerViewMapper _mapper;

    public AccountController(IAccountService accountService, AccountControllerViewMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<JwtTokenPairView>> Login(LoginCredentialsView loginCredentialsView)
    {
        JwtTokenPair jwtTokenPair = await _accountService.Login(_mapper.MapLoginCredentials(loginCredentialsView));
        JwtTokenPairView view = _mapper.MapJwtTokenPair(jwtTokenPair);
        return Ok(view);
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult<JwtTokenPairView>> Register(RegisterCredentialsView registerCredentialsView)
    {
        JwtTokenPair jwtTokenPair = await _accountService.Register(_mapper.MapRegisterCredentials(registerCredentialsView));
        JwtTokenPairView view = _mapper.MapJwtTokenPair(jwtTokenPair);
        return Ok(view);
    }

    [HttpPost]
    [Route("updateJwtPair")]
    public async Task<ActionResult<JwtTokenPairView>> UpdateJwtPair(JwtTokenPairView jwtTokenPairView)
    {
        JwtTokenPair jwtTokenPair = await _accountService.UpdateJwtPair(_mapper.MapJwtTokenPair(jwtTokenPairView));
        JwtTokenPairView view = _mapper.MapJwtTokenPair(jwtTokenPair);
        return Ok(view);
    }

    [HttpPost]
    [Route("logout")]
    public async Task<ActionResult> Logout(JwtTokenPairView jwtTokenPairView)
    {
        await _accountService.Logout(_mapper.MapJwtTokenPair(jwtTokenPairView));
        return NoContent();
    }

    [HttpGet]
    [ParseJwtHeader]
    public async Task<ActionResult<UserInfoView>> GetUser()
    {
        var jwtStorage = new JwtTokenRequestStorage(HttpContext);
        UserInfo userInfo = await _accountService.GetUser(jwtStorage.ReadJwtToken());
        UserInfoView view = _mapper.MapUserInfo(userInfo);
        return Ok(view);
    }
}
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
        var userInfoProvider = new UserInfoProvider(HttpContext);
        UserInfo userInfo = userInfoProvider.ReadUserInfo();
        UserInfoView view = _mapper.MapUserInfo(userInfo);
        
        return Ok(view);
    }

    [HttpGet]
    [Route("email/{email}/busy")]
    public async Task<ActionResult<bool>> IsEmailBusy(string email)
    {
        bool result = await _accountService.IsEmailBusy(email);
        return Ok(result);
    }
    
    [HttpGet]
    [Route("username/{username}/busy")]
    public async Task<ActionResult<bool>> IsUsernameBusy(string username)
    {
        bool result = await _accountService.IsUsernameBusy(username);
        return Ok(result);
    }
}
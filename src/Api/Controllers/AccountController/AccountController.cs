using Api.Controllers.AccountController.Views;
using Api.Swagger.ProducesAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AccountController;

[ApiController]
[Route("users")]
[ProducesInternalException]
public class AccountController : ControllerBase
{
    [HttpPost]
    [ProducesOk]
    [Route("login")]
    public Task<ActionResult<JwtTokenPairView>> Login(LoginCredentialsView loginCredentialsView)
    {
        return Task.FromResult<ActionResult<JwtTokenPairView>>(NoContent());
    }

    [HttpPost]
    [ProducesOk]
    [Route("register")]
    public Task<ActionResult<JwtTokenPairView>> Register(RegisterCredentialsView registerCredentialsView)
    {
        return Task.FromResult<ActionResult<JwtTokenPairView>>(NoContent());
    }

    [HttpPost]
    [ProducesOk]
    [Route("updateJwtPair")]
    public Task<ActionResult<JwtTokenPairView>> UpdateJwtPair(JwtTokenPairView jwtTokenPairView)
    {
        return Task.FromResult<ActionResult<JwtTokenPairView>>(NoContent());
    }

    [HttpPost]
    [ProducesNoContent]
    [Route("logout")]
    public Task<ActionResult> Logout(JwtTokenPairView jwtTokenPairView)
    {
        return Task.FromResult<ActionResult>(NoContent());
    }

    [HttpGet]
    [ProducesOk]
    [Authorize]
    public Task<ActionResult<UserInfoView>> GetUser()
    {
        return Task.FromResult<ActionResult<UserInfoView>>(NoContent());
    }

    [HttpGet]
    [ProducesOk]
    [Route("email/{email}/busy")]
    public Task<ActionResult<bool>> IsEmailBusy(string email)
    {
        return Task.FromResult<ActionResult<bool>>(NoContent());
    }
    
    [HttpGet]
    [ProducesOk]
    [Route("username/{username}/busy")]
    public Task<ActionResult<bool>> IsUsernameBusy(string username)
    {
        return Task.FromResult<ActionResult<bool>>(NoContent());
    }
}
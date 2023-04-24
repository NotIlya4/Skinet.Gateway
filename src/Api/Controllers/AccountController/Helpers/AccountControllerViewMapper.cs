using Api.Controllers.AccountController.Views;
using Infrastructure.AccountService.Models;

namespace Api.Controllers.AccountController.Helpers;

public class AccountControllerViewMapper
{
    public RegisterCredentials MapRegisterCredentials(RegisterCredentialsView view)
    {
        return new RegisterCredentials(
            username: view.Username,
            email: view.Email,
            password: view.Password);
    }

    public LoginCredentials MapLoginCredentials(LoginCredentialsView view)
    {
        return new LoginCredentials(
            email: view.Email,
            password: view.Password);
    }

    public JwtTokenPair MapJwtTokenPair(JwtTokenPairView view)
    {
        return new JwtTokenPair(
            jwtToken: view.JwtToken,
            refreshToken: view.RefreshToken);
    }

    public JwtTokenPairView MapJwtTokenPair(JwtTokenPair jwtTokenPair)
    {
        return new JwtTokenPairView(
            jwtToken: jwtTokenPair.JwtToken,
            refreshToken: jwtTokenPair.RefreshToken.ToString());
    }

    public UserInfoView MapUserInfo(UserInfo userInfo)
    {
        return new UserInfoView(
            id: userInfo.Id.ToString(),
            username: userInfo.Username,
            email: userInfo.Email,
            address: userInfo.Address is null ? null : MapAddress(userInfo.Address));
    }

    private AddressView MapAddress(Address view)
    {
        return new AddressView(
            country: view.Country,
            city: view.City,
            street: view.Street,
            zipcode: view.Zipcode);
    }
}
using Infrastructure.Auther.Models;

namespace Infrastructure.Auther;

public interface IAuther
{
    public Task<UserInfo> GetUserInfo();
}
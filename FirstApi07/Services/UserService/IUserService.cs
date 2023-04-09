using FirstApi07.Authentication;

namespace FirstApi07.Services.UserService
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
    }
}

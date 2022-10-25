using Catagram.Shared.Model;

namespace Catagram.Client.Service;

public interface IAuthService
{
	Task Login(LoginRequest loginRequest);
	Task Register(RegisterRequest registerRequest);
	Task Logout();
	Task<CurrentUser> CurrentUserInfo();
}
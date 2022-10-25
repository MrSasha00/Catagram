using Catagram.Server.Data;
using Catagram.Server.Models;
using Catagram.Shared.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Catagram.Server.Controllers;

[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly SignInManager<ApplicationUser> _signInManager;
	private readonly ApplicationDBContext _applicationDbContext;

	public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDBContext applicationDbContext)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_applicationDbContext = applicationDbContext;
	}

	[HttpPost]
	public async Task<IActionResult> Register([FromBody] RegisterRequest parameters)
	{
		try
		{
			var user = new ApplicationUser
			{
				UserName = parameters.UserName
			};
			var result = await _userManager.CreateAsync(user, parameters.Password);
			if (!result.Succeeded)
				return BadRequest(result.Errors.FirstOrDefault()?.Description);

			await _applicationDbContext.Users.AddAsync(new User()
			{
				Login = parameters.UserName
			});

			await _applicationDbContext.SaveChangesAsync();

			return await Login(new LoginRequest
			{
				UserName = parameters.UserName,
				Password = parameters.Password
			});
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPost]
	public async Task<IActionResult> Login([FromBody] LoginRequest request)
	{
		var user = await _userManager.FindByNameAsync(request.UserName);
		if (user == null)
			return BadRequest("User does not exist");

		var singInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
		if (!singInResult.Succeeded)
			return BadRequest("Invalid password");

		await _signInManager.SignInAsync(user, request.RememberMe);
		return Ok();
	}

	[Authorize]
	[HttpPost]
	public async Task<IActionResult> Logout()
	{
		await _signInManager.SignOutAsync();
		return Ok();
	}

	[HttpGet]
	public CurrentUser CurrentUserInfo()
	{
		return new CurrentUser
		{
			IsAuthenticated = User.Identity.IsAuthenticated,
			UserName = User.Identity.Name,
			Claims = User.Claims
				.ToDictionary(c => c.Type, c => c.Value)
		};
	}
}
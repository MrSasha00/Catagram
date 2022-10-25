using System.ComponentModel.DataAnnotations;

namespace Catagram.Shared.Model;

public class RegisterRequest
{
	[Required]
	public string UserName { get; set; }

	[Required]
	public string Password { get; set; }

	[Compare(nameof(Password), ErrorMessage = "Пароли не совпадают!")]
	public string PasswordConfirm { get; set; }
}
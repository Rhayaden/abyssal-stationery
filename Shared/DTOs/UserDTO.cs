using System.ComponentModel.DataAnnotations;

namespace Blazor.Shared.DTOs
{
	public class UserDTO
	{
		public Guid Id { get; set; }
		[Required(ErrorMessage = "Please enter your first name")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Please enter your last name")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "Please enter your email address")]
		[EmailAddress(ErrorMessage = "Please enter a valid email address")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Please enter a password of at least 8 characters")]
		[MinLength(8, ErrorMessage = "Password should have minimum 8 characters")]
		public string Password { get; set; }
		public CartDTO? Cart { get; set; }
		public ICollection<OrderDTO>? Orders { get; set; }

		public DateTime RegisteredAt { get; set; } = DateTime.Now;
	}
}

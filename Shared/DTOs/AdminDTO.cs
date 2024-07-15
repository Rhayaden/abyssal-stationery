using System.ComponentModel.DataAnnotations;

namespace Blazor.Shared.DTOs
{
	public class AdminDTO
	{
		public Guid Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}
}

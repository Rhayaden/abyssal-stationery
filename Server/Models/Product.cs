namespace Blazor.Server.Models
{
	public class Product
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string? Description { get; set; }
		public string? Image { get; set; }
		public decimal Price { get; set; }
		public int Stock { get; set; }
		public Guid CategoryId { get; set; }
		public Category Category { get; set; } = null!;
		public DateTime UpdatedAt { get; set; }
	}
}

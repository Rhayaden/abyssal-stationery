namespace Blazor.Server.Models
{
	public class ProductInOrder
	{
		public Guid Id { get; set; }
		public Guid ProductId { get; set; }
		public Guid CartId { get; set; }
		public string ProductTitle { get; set; }
		public string Category { get; set; }
		public string? Image { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
	}
}

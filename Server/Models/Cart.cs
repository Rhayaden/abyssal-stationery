namespace Blazor.Server.Models
{
	public class Cart
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public ICollection<ProductInCart> Products { get; set; } = new List<ProductInCart>();
		public int Quantity { get; set; }
		public decimal Total { get; set; }
		public User User { get; set; } = null!;
	}
}

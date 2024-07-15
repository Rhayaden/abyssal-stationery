namespace Blazor.Server.Models
{
	public class Order
	{
		public Guid Id { get; set; }
		public DateTime CreateDate { get; set; }
		public Guid UserId { get; set; }
		public ICollection<ProductInOrder> Products { get; set; } = new List<ProductInOrder>();
		public decimal Total { get; set; }
		public User User { get; set; } = null!;
	}
}

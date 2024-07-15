namespace Blazor.Shared.DTOs
{
	public class CartDTO
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public ICollection<ProductInCartDTO>? Products { get; set; }
		public int Quantity { get; set; }
		public decimal Total { get; set; }
	}
}

namespace Blazor.Shared.DTOs
{
	public class OrderDTO
	{
		public Guid Id { get; set; }
		public DateTime CreateDate { get; set; } = DateTime.Now;
		public Guid UserId { get; set; }
		public ICollection<ProductInOrderDTO>? Products { get; set; }
		public decimal Total { get; set; }
	}
}

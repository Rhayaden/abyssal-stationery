namespace Blazor.Shared.DTOs
{
	public class SaleDTO
	{
		public string? PromotionName { get; set; }
		public int Discount { get; set; }
		public Guid CategoryId { get; set; }
		public int Duration { get; set; }
	}
}

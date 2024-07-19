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
		public Guid SubsubcategoryId { get; set; }
		public Sub_subcategory Subsubcategory { get; set; } = null!;
		public DateTime UpdatedAt { get; set; }
		public bool OnSale { get; set; } = false;
		public int? Discount { get; set; }
		public decimal? SalePrice { get; set; }
		public int? DurationHour { get; set; }
        public DateTime? SaleStartedAt { get; set; }
    }
}

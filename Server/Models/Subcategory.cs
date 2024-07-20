namespace Blazor.Server.Models
{
	public class Subcategory
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public DateTime UpdatedAt { get; set; }
		public Guid CategoryId { get; set; }
		public Category Category { get; set; } = null!;
		public ICollection<Sub_subcategory> Subsubcategories { get; set; } = new List<Sub_subcategory>();
		public bool ActivePromotion { get; set; } = false;
		public string? PromotionName { get; set; }
		public int? PromotionDiscount { get; set; }
		public int? PromotionDurationHour { get; set; }
        public DateTime? PromotionStartedAt { get; set; }
    }
}

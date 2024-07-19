namespace Blazor.Shared.DTOs
{
	public class SubcategoryDTO
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public DateTime UpdatedAt { get; set; } = DateTime.Now;
		public Guid CategoryId { get; set; }
		public CategoryDTO? Category { get; set; }
		public ICollection<Sub_subcategoryDTO> Subsubcategories { get; set; } = new List<Sub_subcategoryDTO>();
		public bool ActivePromotion { get; set; } = false;
		public string? PromotionName { get; set; }
	}
}
namespace Blazor.Shared.DTOs
{
	public class CategoryDTO
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public ICollection<ProductDTO>? Products { get; set; }
		public DateTime UpdatedAt { get; set; } = DateTime.Now;
		public ICollection<SubcategoryDTO>? Subcategories { get; set; } = new List<SubcategoryDTO>();
	}
}

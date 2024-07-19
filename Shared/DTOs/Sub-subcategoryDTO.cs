namespace Blazor.Shared.DTOs
{
	public class Sub_subcategoryDTO
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public DateTime UpdatedAt { get; set; } = DateTime.Now;
		public Guid SubcategoryId { get; set; }
		public SubcategoryDTO? Subcategory { get; set; }
	}
}
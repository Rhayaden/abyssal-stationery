namespace Blazor.Server.Models
{
	public class Sub_subcategory
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public DateTime UpdatedAt { get; set; }
		public Guid SubcategoryId { get; set; }
		public Subcategory Subcategory { get; set; } = null!;
	}
}

﻿namespace Blazor.Shared.DTOs
{
	public class ProductInCartDTO
	{
		public Guid Id { get; set; }
		public Guid ProductId { get; set; }
		public string ProductTitle { get; set; }
		public string Category { get; set; }
		public string? Image { get; set; }
		public int Quantity { get; set; }
		public int Stock { get; set; }
		public decimal Price { get; set; }
	}
}

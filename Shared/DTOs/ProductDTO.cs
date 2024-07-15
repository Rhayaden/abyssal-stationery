﻿using System.ComponentModel.DataAnnotations;

namespace Blazor.Shared.DTOs
{
	public class ProductDTO
	{
		public Guid Id { get; set; }
		[Required(ErrorMessage = "Please enter a title")]
		public string Title { get; set; }
		public string? Description { get; set; }
		public string? Image { get; set; }
		[Required(ErrorMessage = "Please enter a price")]
		[Range(5, 100000, ErrorMessage = "Price can be between $ 5 - 100.000")]
		public decimal Price { get; set; }
		[Required(ErrorMessage = "Please enter a stock")]
		[Range(0, 1000, ErrorMessage = "Stock cannot be less than 1 or more than 1000")]
		public int Stock { get; set; }
		[Required]
		public Guid CategoryId { get; set; }
		public CategoryDTO? Category { get; set; }
		public DateTime UpdatedAt { get; set; } = DateTime.Now;
	}
}
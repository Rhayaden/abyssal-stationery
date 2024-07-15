using Azure.Core;
using Blazor.Server.Models;
using Blazor.Shared.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Blazor.Server.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		public DbSet<Product> Products { get; set; }
		public DbSet<ProductInOrder> ProductsInOrders { get; set; }
		public DbSet<ProductInCart> ProductsInCarts { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Admin> Admins { get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var hashedPw = PasswordCrypter.Encrypt("");
			modelBuilder.Entity<Admin>()
				.HasData(new Admin
				{
					Id = Guid.NewGuid(),
					Name = "",
					Email = "",
					Password = hashedPw
				});
		}
	}
}

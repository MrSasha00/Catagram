using Catagram.Server.Models;
using Catagram.Shared.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Catagram.Server.Data;

public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
{
	public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
	{
		// Database.EnsureDeleted();
		// Database.EnsureCreated();
	}
	public DbSet<User> Users { get; set; }
	public DbSet<Post> Feeds { get; set; }
	public DbSet<Comment> Comments { get; set; }
}
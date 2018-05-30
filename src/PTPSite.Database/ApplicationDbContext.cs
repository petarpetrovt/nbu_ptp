using Microsoft.EntityFrameworkCore;

namespace PTPSite.Database
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Comment> Comments { get; set; }

		public DbSet<ApplicationUser> Users { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			#region Entity: ApplicationUser

			modelBuilder.Entity<ApplicationUser>(b =>
			{
				b.Property(x => x.Id);
				b.Property(x => x.PasswordHash);
				b.Property(x => x.Email);
				b.Property(x => x.UserName);
				b.Property(x => x.NormalizedUserName);
				b.Property(x => x.Role);
				b.Property(x => x.Name);

				b.HasKey(x => x.Id);
				b.ToTable("Users");
			});

			#endregion

			#region Entity: Comment

			modelBuilder.Entity<Comment>(b =>
			{
				b.Property(x => x.Id);
				b.Property(x => x.Text);
				b.Property(x => x.ByUserId);
				b.Property(x => x.Date);

				b.HasKey(x => x.Id);
				b.ToTable("Comments");
			});

			#endregion
		}
	}
}

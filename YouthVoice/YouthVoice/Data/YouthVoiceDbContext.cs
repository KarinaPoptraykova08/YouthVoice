using Microsoft.EntityFrameworkCore;

namespace YouthVoice.Data
{
    public class YouthVoiceDbContext : DbContext
    {
        public YouthVoiceDbContext(DbContextOptions<YouthVoiceDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
            .HasOne(m => m.User)
            .WithMany()
            .HasForeignKey(m => m.UserId);

            modelBuilder.Entity<Member>()
                .HasOne(m => m.Organisation)
                .WithMany()
                .HasForeignKey(m => m.OrganisationId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Organisation>()
                .HasOne(o => o.City)
                .WithMany()
                .HasForeignKey(o => o.CityId);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Organisation)
                .WithMany(o => o.Events)
                .HasForeignKey(e => e.OrganisationName)
                .HasPrincipalKey(o => o.Name);

            modelBuilder.Entity<Image>()
                .HasOne(i => i.Event) // Image has one Event
                .WithMany(e => e.AdditionalImages) // Event has many Images
                .HasForeignKey(i => i.EventId); // Foreign key is EventId

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

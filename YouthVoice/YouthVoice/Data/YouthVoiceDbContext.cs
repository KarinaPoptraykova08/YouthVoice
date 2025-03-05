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

            // Configure the many-to-many relationship
            modelBuilder.Entity<OrganisationEvent>()
                .HasKey(oe => new { oe.OrganisationId, oe.EventId });

            modelBuilder.Entity<OrganisationEvent>()
                .HasOne(oe => oe.Organisation)
                .WithMany(o => o.OrganisationEvents)
                .HasForeignKey(oe => oe.OrganisationId);

            modelBuilder.Entity<OrganisationEvent>()
                .HasOne(oe => oe.Event)
                .WithMany(o => o.OrganisationEvents)
                .HasForeignKey(oe => oe.EventId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<OrganisationEvent> OrganisationEvent { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Sim.Identity.Context
{
    using Entity;
    public class IdentityContext : IdentityDbContext
    {       
        public IdentityContext(){}

        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {

        }

        /*
        private static string _connectionstring = @"Server=127.0.0.1,1433\\sql1;Database=Sim-Identity-db20210001;User Id=sa;Password=sql@1234;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionstring);
            }
        }
        */

        public DbSet<ApplicationUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.ApplyConfiguration(new Config.UserMap());
            base.OnModelCreating(modelbuilder);

            //Seeding a  'Administrator' role to AspNetRoles table
            modelbuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Admin Global", NormalizedName = "Admin Global".ToUpper() });

            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<ApplicationUser>();


            //Seeding the User to AspNetUsers table
            modelbuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
                    Name = "Admin",
                    LastName = "Global",
                    Gender = "Masculino",
                    Email = "sim@sim.com",
                    NormalizedEmail = "sim@sim.com".ToUpper(),
                    UserName = "Admin",
                    NormalizedUserName = "Admin".ToUpper(),
                    PhoneNumber = "+111111111111",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    PasswordHash = hasher.HashPassword(null, "$im1234")
                }
            );


            //Seeding the relation between our user and role to AspNetUserRoles table
            modelbuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                }
            );

        }
    }
}

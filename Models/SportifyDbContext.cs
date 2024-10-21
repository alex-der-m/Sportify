using Microsoft.EntityFrameworkCore;


namespace Sportify_back.Models{

    public class SportifyDbContext:DbContext{

        public SportifyDbContext(DbContextOptions<SportifyDbContext> options):base(options){

        }

        public DbSet<Activities> Activities {get; set;}

        public DbSet<Classes> Classes {get; set;}

        public DbSet<Licenses> Licenses {get; set;}

        public DbSet<Plans> Plans {get; set;}

        public DbSet<Profiles> Profiles {get; set;}

        public DbSet<Programmings> Programmings {get; set;}

        public DbSet<Teachers> Teachers {get; set;}

        public DbSet<Users> Users {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder){
                    
        }
    }
}
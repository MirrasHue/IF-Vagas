using Microsoft.EntityFrameworkCore;

namespace IF_Vagas.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){

    }

    public DbSet<Vacancy> Vacancies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasData(PopulateUsers());

        modelBuilder.Entity<Project>()
            .HasOne(p => p.User)
            .WithMany(u => u.Projects)
            .HasForeignKey(p => p.UserId);

        //modelBuilder.Entity<Project>().HasData(PopulateProjects());
        //modelBuilder.Entity<Vacancy>().HasData(PopulateVacancies());
    }

    private List<User> PopulateUsers(){
        return new List<User>{
            new User { Id = 1, Name = "Zé", Email = "ze@gmail.com", Password = "123", IsAdmin = true},
            new User { Id = 2, Name = "Maria", Email = "maria@hotmail.com", Password = "321", IsAdmin = false},
            new User { Id = 3, Name = "João", Email = "joao@bol.com", Password = "abc", IsAdmin = false},
            new User { Id = 4, Name = "Ana", Email = "ana@yahoo.com", Password = "cba", IsAdmin = true}
        };
    }
}
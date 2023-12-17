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

        modelBuilder.Entity<Project>()
            .HasOne(p => p.User)
            .WithMany(u => u.Projects)
            .HasForeignKey(p => p.UserId);

        modelBuilder.Entity<User>().HasData(GetDummyUsers());
        modelBuilder.Entity<Project>().HasData(GetDummyProjects());
        modelBuilder.Entity<Vacancy>().HasData(GetDummyVacancies());
    }

    private List<User> GetDummyUsers(){
        return new List<User>{
            new User { Id = 1, Name = "Zé", Email = "ze@gmail.com", Password = "123", IsAdmin = true},
            new User { Id = 2, Name = "Maria", Email = "maria@hotmail.com", Password = "321", IsAdmin = false},
            new User { Id = 3, Name = "João", Email = "joao@bol.com", Password = "abc", IsAdmin = false},
            new User { Id = 4, Name = "Ana", Email = "ana@yahoo.com", Password = "cba", IsAdmin = true}
        };
    }

    private List<Project> GetDummyProjects(){
        return new List<Project>{
            new Project { Id = 1, Name = "Project", Description = "Software", WebsiteURL = "https", UserId = 1},
            new Project { Id = 2, Name = "Project2", Description = "DB", WebsiteURL = "https", UserId = 2},
            new Project { Id = 3, Name = "Project3", Description = "SQL", WebsiteURL = "https", UserId = 3},
            new Project { Id = 4, Name = "Project4", Description = "C++", WebsiteURL = "https", UserId = 4}
        };
    }

    private List<Vacancy> GetDummyVacancies(){
        return new List<Vacancy>{
            new Vacancy { Id = 1,
             Title = "Estágio em Desenvolvimento back-end",
              Description = "Estamos em busca de um desenvolvedor backend apaixonado por criar soluções robustas e escaláveis. Você será responsável por projetar, implementar e manter a lógica de servidor, APIs e bancos de dados, colaborando estreitamente com equipes multidisciplinares para garantir a eficiência e confiabilidade do sistema. Procuramos alguém com sólidos conhecimentos em linguagens como Python, Java ou Node.js, familiaridade com frameworks como Django, Spring ou Express, além de experiência em modelagem de dados e arquitetura de sistemas distribuídos. Se você é proativo, possui habilidades analíticas afiadas e deseja fazer parte de um ambiente dinâmico e inovador, junte-se a nós nesta jornada tecnológica.",
              Company = "Software Company",
              externalLinkToApply = "https://br.indeed.com/jobs?q=desenvolvedor%20back-end&l=&from=searchOnHP",
              UserId = 1,
              Location = "Moc city",
              CreatedAt = DateTime.UtcNow,
              Salary = 10000},
              new Vacancy { Id = 2,
             Title = "Estágio em Desenvolvimento front-end",
              Description = "Estamos à procura de um desenvolvedor front-end apaixonado por criar interfaces visuais intuitivas e responsivas. Sua função incluirá a tradução de designs em códigos eficientes, colaborando de perto com designers e equipes de back-end para garantir a integração perfeita entre a interface e a lógica do aplicativo. Procuramos alguém com proficiência em HTML, CSS e JavaScript, experiência em frameworks modernos como React, Vue.js ou Angular, além de habilidades sólidas em design responsivo e familiaridade com princípios de UX/UI. Se você é criativo, possui uma mentalidade orientada para soluções e busca constantemente aprimorar a experiência do usuário, junte-se a nós nesta oportunidade desafiadora e dinâmica.",
              Company = "Software Company 2",
              externalLinkToApply = "https://br.indeed.com/jobs?q=desenvolvedor%20front-end&l=",
              UserId = 1,
              Location = "BH",
              CreatedAt = DateTime.UtcNow,
              Salary = 8000},
                new Vacancy { Id = 3,
             Title = "Estágio Engenheiro de Software",
              Description = "Oportunidade de estágio na área de engenharia de software.",
              Company = "Google",
              externalLinkToApply = "https://br.indeed.com/jobs?q=engenheiro+de+software&l=&vjk=872a641093fae6cb",
              UserId = 1,
              Location = "Sillicon Valley",
              CreatedAt = DateTime.UtcNow,
              Salary = 15000},
        };
    }
}
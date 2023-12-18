using System.Security.Claims;

namespace IF_Vagas.Data;

public class Vacancy
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Company { get; set; }
    public string? Location { get; set; }
    public double Salary { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? externalLinkToApply {get; set;}
    public int UserId { get; set; }

    public User? user {get; set;}
    public int Applications {get; set;}
}

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public bool IsAdmin { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Project>? Projects { get; set; }
    public ClaimsPrincipal ToClaimsPrincipal()=> new ( new ClaimsIdentity(
        new Claim[]{
            new (ClaimTypes.Email, Email),
            new (ClaimTypes.Hash, Password),
            IsAdmin ? new(ClaimTypes.Role, "admin") : new(ClaimTypes.Role, "user"),
        }.Where(c=> c!= null), "IF-Vagas")  
    );
    public static User FromClaimsPrincipal(ClaimsPrincipal principal) => new()
    {
        Email = principal.FindFirstValue(ClaimTypes.Email),
        Password = principal.FindFirstValue(ClaimTypes.Hash),
        Name = principal.FindFirstValue(nameof(Name)),
    };
}

public class Project
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? WebsiteURL { get; set; }

    // Foreign key for the relationship with User
    public int UserId { get; set; }
    public User? User { get; set; }
}
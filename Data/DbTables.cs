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
}

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public bool IsAdmin { get; set; }
    public List<Project>? Projects { get; set; }
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
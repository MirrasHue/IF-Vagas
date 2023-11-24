using Microsoft.EntityFrameworkCore;

namespace IF_Vagas.Data;

public class ApplicationServices
{
    private ApplicationDbContext dbContext;

    public ApplicationServices(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    /// Vacancy services
    public async Task<List<Vacancy>> GetVacanciesAsync()
    {
        return await dbContext.Vacancies.ToListAsync();
    }

    public async Task<Vacancy> AddVacancyAsync(Vacancy vacancy)
    {
        try
        {
            dbContext.Vacancies.Add(vacancy);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
        return vacancy;
    }

    public async Task<Vacancy> UpdateVacancyAsync(Vacancy vacancy)
    {
        try
        {
            var vacancyExist = dbContext.Vacancies.FirstOrDefault(p => p.Id == vacancy.Id);
            if (vacancyExist != null)
            {
                dbContext.Update(vacancy);
                await dbContext.SaveChangesAsync();
            }
        }
        catch (Exception)
        {
            throw;
        }
        return vacancy;
    }

    public async Task DeleteVacancyAsync(Vacancy vacancy)
    {
        try
        {
            dbContext.Vacancies.Remove(vacancy);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    /// End Vacancy services

    /// User services
    public async Task<List<User>> GetUsersAsync()
    {
        return await dbContext.Users.ToListAsync();
    }

    public async Task<User> AddUserAsync(User user)
    {
        try
        {
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        try
        {
            var userExist = dbContext.Users.FirstOrDefault(p => p.Id == user.Id);
            if (userExist != null)
            {
                dbContext.Update(user);
                await dbContext.SaveChangesAsync();
            }
        }
        catch (Exception)
        {
            throw;
        }
        return user;
    }

    public async Task DeleteUserAsync(User user)
    {
        try
        {
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    /// End User services
    
    /// Project services
    public async Task<List<Project>> GetProjectsAsync()
    {
        return await dbContext.Projects.ToListAsync();
    }

    public async Task<Project> AddProjectAsync(Project proj)
    {
        try
        {
            dbContext.Projects.Add(proj);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
        return proj;
    }

    public async Task<Project> UpdateProjectAsync(Project proj)
    {
        try
        {
            var projExist = dbContext.Projects.FirstOrDefault(p => p.Id == proj.Id);
            if (projExist != null)
            {
                dbContext.Update(proj);
                await dbContext.SaveChangesAsync();
            }
        }
        catch (Exception)
        {
            throw;
        }
        return proj;
    }

    public async Task DeleteProjectAsync(Project proj)
    {
        // We also need to delete the project from the user's project list 
        try
        {
            dbContext.Projects.Remove(proj);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    /// End Project services
}
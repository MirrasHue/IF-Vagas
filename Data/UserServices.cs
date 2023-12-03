using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;
namespace IF_Vagas.Data;

public class UserServices{
    private readonly ProtectedLocalStorage _protectedLocalStorage;
    private readonly ApplicationDbContext _dbContext;

    public UserServices(ProtectedLocalStorage protectedLocalStorage, ApplicationDbContext dbContext)
    {
        _protectedLocalStorage = protectedLocalStorage;
        _dbContext = dbContext;
    }
    public User? LookupUserInDatabase(string useremail, string password)
    {
        var foundUser= _dbContext.Users.SingleOrDefault(u => u.Email == useremail && u.Password == password);
        return foundUser;
    }
    private readonly string _blazorStorageKey = "blazorIdentity";

    public async Task PersistUserToBrowserAsync(User user)
    {
        string userJson = JsonConvert.SerializeObject(user);        
        await _protectedLocalStorage.SetAsync(_blazorStorageKey, userJson);
    }
    public async Task<User?> FetchUserFromBrowserAsync()
    {
        try
        {
            var storedUserResult = await _protectedLocalStorage.GetAsync<string>(_blazorStorageKey);

            if (storedUserResult.Success && !string.IsNullOrEmpty(storedUserResult.Value))
            {
                var user = JsonConvert.DeserializeObject<User>(storedUserResult.Value);

                return user;
            }
        }
        catch (InvalidOperationException)
        {
        }

        return null;
    }
    public async Task ClearBrowserUserDataAsync() => await _protectedLocalStorage.DeleteAsync(_blazorStorageKey);
}
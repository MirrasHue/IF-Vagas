using System.Security.Claims;
using IF_Vagas.Data;
using Microsoft.AspNetCore.Components.Authorization;

public class IF_VagasAuthenticationStateProvider : AuthenticationStateProvider, IDisposable
{
    private readonly UserServices _blazorSchoolUserService;
    public bool IsAuthenticated { get; private set; }
    public User CurrentUser { get; private set; } = new();
    public IF_VagasAuthenticationStateProvider(UserServices blazorSchoolUserService)
    {
        _blazorSchoolUserService = blazorSchoolUserService;
        AuthenticationStateChanged += OnAuthenticationStateChangedAsync;
    }
    public async Task LoginAsync(string username, string password)
    {
        var principal = new ClaimsPrincipal();
        var user = _blazorSchoolUserService.LookupUserInDatabase(username, password);

        if (user is not null)
        {
            await _blazorSchoolUserService.PersistUserToBrowserAsync(user);
            principal = user.ToClaimsPrincipal();
            Console.WriteLine("claim " + principal.Identity.Name);
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }
    public async Task LogoutAsync()
    {
        await _blazorSchoolUserService.ClearBrowserUserDataAsync();
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new())));
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var principal = new ClaimsPrincipal();
        var user = await _blazorSchoolUserService.FetchUserFromBrowserAsync();

        if (user is not null && user.Name is not null &&  user.Password is not null)
        {
            var userInDatabase = _blazorSchoolUserService.LookupUserInDatabase(user.Name, user.Password);

            if (userInDatabase is not null)
            {
                principal = userInDatabase.ToClaimsPrincipal();
                IsAuthenticated= principal.Identity.IsAuthenticated;
                CurrentUser = userInDatabase;
            }
        }

        return new(principal);
    }
    
    private async void OnAuthenticationStateChangedAsync(Task<AuthenticationState> task)
    {
        var authenticationState = await task;

        if (authenticationState is not null)
        {
            CurrentUser = User.FromClaimsPrincipal(authenticationState.User);
        }
    }
    public void Dispose() => AuthenticationStateChanged -= OnAuthenticationStateChangedAsync;

}
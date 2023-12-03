using System.Security.Claims;
using IF_Vagas.Data;
using Microsoft.AspNetCore.Components.Authorization;

public class IF_VagasAuthenticationStateProvider : AuthenticationStateProvider, IDisposable
{
    private readonly UserServices _blazorUserService;
    public bool IsAuthenticated { get; private set; }
    public User CurrentUser { get; private set; } = new();
    public IF_VagasAuthenticationStateProvider(UserServices blazorUserService)
    {
        _blazorUserService = blazorUserService;
        AuthenticationStateChanged += OnAuthenticationStateChangedAsync;
    }
    public async Task LoginAsync(string useremail, string password)
    {
        var principal = new ClaimsPrincipal();
        var user = _blazorUserService.LookupUserInDatabase(useremail, password);

        if (user is not null)
        {
            await _blazorUserService.PersistUserToBrowserAsync(user);
            principal = user.ToClaimsPrincipal();
            //Console.WriteLine("claim " + principal.Identity.Name);
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }
    public async Task LogoutAsync()
    {
        await _blazorUserService.ClearBrowserUserDataAsync();
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new())));
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var principal = new ClaimsPrincipal();
        var user = await _blazorUserService.FetchUserFromBrowserAsync();

        if (user is not null && user.Email is not null &&  user.Password is not null)
        {
            var userInDatabase = _blazorUserService.LookupUserInDatabase(user.Email, user.Password);

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
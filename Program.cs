using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using IF_Vagas.Data;
using IF_Vagas.Shared;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<StateContainer>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>{options.UseSqlite("Data Source=Database.db");});
builder.Services.AddScoped<ApplicationServices>();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<IF_VagasAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<IF_VagasAuthenticationStateProvider>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

/*var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();
optionsBuilder.UseSqlite("Data Source = Database.db");
using(var context = new UserDbContext(optionsBuilder.Options))  {
        context.Database.Migrate();
}*/

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

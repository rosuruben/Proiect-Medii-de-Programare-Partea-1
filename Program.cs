using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ClinicaVeterinaraP1.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ClinicaVeterinaraP1Context")
    ?? throw new InvalidOperationException("Connection string 'ClinicaVeterinaraP1Context' not found.");

builder.Services.AddDbContext<ClinicaVeterinaraP1Context>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; 
    options.Password.RequireDigit = false;          
    options.Password.RequiredLength = 3;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddRoles<IdentityRole>() 
    .AddEntityFrameworkStores<ClinicaVeterinaraP1Context>();

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Animale");
    options.Conventions.AuthorizeFolder("/MediciVeterinari");
    options.Conventions.AuthorizeFolder("/Programari");
    options.Conventions.AuthorizeFolder("/Proprietari");
    options.Conventions.AuthorizeFolder("/Recenzii");
    options.Conventions.AllowAnonymousToPage("/Index");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
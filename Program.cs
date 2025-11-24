using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pardut_Daniela_Laborator2.Data;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

// ------------------------------------------
// 1. Add Authorization Policy
// ------------------------------------------
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("Admin"));
});

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    // Acces doar Admin la Publishers și Categories
    options.Conventions.AuthorizeFolder("/Publishers", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Categories", "AdminPolicy");

    // Acces restricționat pentru Books (implicit toate paginile necesita login)
    options.Conventions.AuthorizeFolder("/Books");

    // Permitem acces ANONIM la Index și Details
    options.Conventions.AllowAnonymousToPage("/Books/Index");
    options.Conventions.AllowAnonymousToPage("/Books/Details");
});


builder.Services.AddDbContext<Pardut_Daniela_Laborator2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Pardut_Daniela_Laborator2Context")
    ?? throw new InvalidOperationException("Connection string 'Pardut_Daniela_Laborator2Context' not found.")));

builder.Services.AddDbContext<LibraryIdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Pardut_Daniela_Laborator2Context")
    ?? throw new InvalidOperationException("Connection string 'Pardut_Daniela_Laborator2Context' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LibraryIdentityContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();


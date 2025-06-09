using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using TodoApp.Infrastructure.Services;
using Application.Interfaces;
using Web.Areas.Identity.Data;
using Web.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IToDoService, ToDoService>();

// Add DbContext
builder.Services.AddDbContext<Infrastructure.Data.ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<Infrastructure.Data.ApplicationDbContext>();

// Blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(); // <- ¡Esta parte es obligatoria!

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorPages(); // ¡NECESARIO para que funcione el scaffold!

app.MapBlazorHub();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode(); // <- ¡Esta también es obligatoria!

app.Run();

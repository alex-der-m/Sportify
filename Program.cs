using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sportify_back.Models;
using Microsoft.Extensions.DependencyInjection;
using Sportify_solution_app.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UsersContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UsersContext") ?? throw new InvalidOperationException("Connection string 'UsersContext' not found.")));
builder.Services.AddDbContext<TeachersContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TeachersContext") ?? throw new InvalidOperationException("Connection string 'TeachersContext' not found.")));
builder.Services.AddDbContext<ProgrammingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProgrammingContext") ?? throw new InvalidOperationException("Connection string 'ProgrammingContext' not found.")));
builder.Services.AddDbContext<ProfileContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProfileContext") ?? throw new InvalidOperationException("Connection string 'ProfileContext' not found.")));
builder.Services.AddDbContext<PlansContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PlansContext") ?? throw new InvalidOperationException("Connection string 'PlansContext' not found.")));
builder.Services.AddDbContext<LicenciesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LicenciesContext") ?? throw new InvalidOperationException("Connection string 'LicenciesContext' not found.")));
builder.Services.AddDbContext<ActivitiesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ActivitiesContext") ?? throw new InvalidOperationException("Connection string 'ActivitiesContext' not found.")));
builder.Services.AddDbContext<ClassesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClassesContext") ?? throw new InvalidOperationException("Connection string 'ClassesContext' not found.")));

// Add services to the container.
builder.Services.AddDbContext<SportifyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnectionString")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<SportifyDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

using BlogApp.Core.Repositories;
using BlogApp.Core.Services;
using BlogApp.Infrastructure.Services;
using BlogApp.Infrastructure.Repositories.DapperRepositories;
using BlogApp.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using BlogApp.Presentation.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("MsSqlServer");

builder.Services.AddSingleton(connectionString);

builder.Services.AddScoped<IRoleRepository, RoleDapperRepository>(provider =>
    new RoleDapperRepository(connectionString));

builder.Services.AddValidatorsFromAssemblyContaining<UserRegistrationValidator>();

builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IRoleRepository, RoleDapperRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleDapperRepository>();
builder.Services.AddScoped<IUserRepository, UserDapperRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.Run();

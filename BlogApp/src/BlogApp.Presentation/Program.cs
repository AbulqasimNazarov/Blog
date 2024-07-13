

using BlogApp.Core.Blog.Repositories.Base;
using BlogApp.Core.Role.Models;
using BlogApp.Core.User.Models;

// using BlogApp.Core.Blog.Services.Base;
// using BlogApp.Core.Role.Repositories.Base;
// using BlogApp.Core.Role.Services.Base;
// using BlogApp.Core.Topic.Repositories.Base;
// using BlogApp.Core.Topic.Services.Base;
// using BlogApp.Core.User.Repositories.Base;
// using BlogApp.Core.User.Services.Base;
// using BlogApp.Core.UserRole.Repositories.Base;
// using BlogApp.Core.UserRole.Services.Base;
// using BlogApp.Infrastructure.Blog.Repositories.Dapper;
// using BlogApp.Infrastructure.Blog.Services;
// using BlogApp.Infrastructure.Role.Repositories.Dapper;
// using BlogApp.Infrastructure.Role.Services;
// using BlogApp.Infrastructure.Topic.Repositories.Dapper;
// using BlogApp.Infrastructure.Topic.Services;
// using BlogApp.Infrastructure.User.Repositories.Dapper;
// using BlogApp.Infrastructure.User.Services;
// using BlogApp.Infrastructure.UserRole.Repositories.Dapper;
// using BlogApp.Infrastructure.UserRole.Services;

using BlogApp.Presentation.Validators;
using BlogApp.Presentation.Verification.Base;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MyGames.Infrastructure.Data.DbContext;

using BlogApp.Infrastructure.Topic.Repositories.Dapper;
using BlogApp.Infrastructure.Blog.Repositories.Dapper;
using BlogApp.Core.Blog.Models;
using BlogApp.Infrastructure.UserTopic.Repositories.Dapper;

var rep = new UserTopicDapperRepository("Server=blogpostgresqlserver.postgres.database.azure.com;Database=postgres;Port=5432;User Id=azureuser;Password=Password123!;Ssl Mode=Require;");
await rep.GetAllTopicsByUserId(1);

return;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllersWithViews();

// var connectionString = builder.Configuration.GetConnectionString("MsSqlServer");

// builder.Services.AddSingleton(connectionString);

// builder.Services.AddScoped<IRoleRepository, RoleDapperRepository>(provider =>
//     new RoleDapperRepository(connectionString));

builder.Services.AddValidatorsFromAssemblyContaining<UserRegistrationValidator>();

// builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

// builder.Services.AddScoped<IRoleService, RoleService>();
// builder.Services.AddScoped<IUserRoleService, UserRoleService>();
// builder.Services.AddScoped<IUserService, UserService>();
// builder.Services.AddScoped<IEmailService, EmailService>();
// builder.Services.AddScoped<ITopicService, TopicService>();
// builder.Services.AddScoped<IBlogService, BlogService>();
// builder.Services.AddScoped<IConfiguration>(provider => builder.Configuration);

// builder.Services.AddScoped<IRoleRepository, RoleDapperRepository>();
// builder.Services.AddScoped<IUserRoleRepository, UserRoleDapperRepository>();
// builder.Services.AddScoped<IUserRepository, UserDapperRepository>();
// builder.Services.AddScoped<ITopicRepository, TopicDapperRepository>();
// builder.Services.AddScoped<IBlogRepository, BlogDapperRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Identity/Login";
        options.AccessDeniedPath = "/Identity/AccessDenied";
    });

builder.Services.AddDbContext<BlogDbContext>(dbContextOptionsBuilder => {
    var connectionString = builder.Configuration.GetConnectionString("PostgreSqlCloud");
    dbContextOptionsBuilder.UseNpgsql(connectionString: connectionString);
});

builder.Services.AddIdentity<User, Role>(options => {
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<BlogDbContext>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


// app.UseSwagger();
// app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

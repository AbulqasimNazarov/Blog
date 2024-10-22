using BlogApp.Core.Blog.Repositories.Base;
using BlogApp.Core.Role.Models;
using BlogApp.Core.User.Models;




using BlogApp.Presentation.Validators;
using BlogApp.Presentation.Verification.Base;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using BlogApp.Infrastructure.Data.DbContext;
using Microsoft.AspNetCore.Identity;
using BlogApp.Core.Topic.Repositories.Base;
using BlogApp.Infrastructure.Topic.Repositories.Ef_Core;
using BlogApp.Infrastructure.Blog.Repositories.Ef_Core;
using BlogApp.Core.UserTopic.Repositories.Base;
using BlogApp.Infrastructure.UserTopic.Repositories.Ef_Core;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<BlogDbContext>(dbContextOptionsBuilder => {
    var connectionString = builder.Configuration.GetConnectionString("PostgreSqlDev");
    dbContextOptionsBuilder.UseNpgsql(connectionString: connectionString);
});

builder.Services.AddIdentity<User, Role>(options =>
{
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<BlogDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

builder.Services.AddMediatR(configuration => {
    Type typeInReferencedAssembly = typeof(BlogApp.Infrastructure.Data.DbContext.BlogDbContext);
    configuration.RegisterServicesFromAssembly( typeInReferencedAssembly.Assembly );
});


builder.Services.AddValidatorsFromAssemblyContaining<UserRegistrationValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserLoginValidator>();

builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IUserTopicRepository, UserTopicEfCoreRepository>();
builder.Services.AddScoped<ITopicRepository, TopicEfCoreRepository>();
builder.Services.AddScoped<IBlogRepository, BlogEfCoreRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Identity/Login";
        options.AccessDeniedPath = "/Identity/AccessDenied";
    });


builder.Services.AddSwaggerGen();

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


app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
using BlogApp.Core.Models;
using BlogApp.Infrastructure.Repositories;
using BlogApp.Infrastructure.Repositories.DapperRepositories;

var rep = new UserDapperRepository("Server=blogpostgresqlserver.postgres.database.azure.com;Database=postgres;Port=5432;User Id=azureuser;Password=Password123!;Ssl Mode=Require;");
var role = await rep.GetByIdAsync(1);

    System.Console.WriteLine($"{role?.Id} = role?.Id");


// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.

// builder.Services.AddControllersWithViews();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }

// app.UseHttpsRedirection();
// app.UseStaticFiles();
// app.UseRouting();


// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller}/{action=Index}/{id?}");

// app.MapFallbackToFile("index.html");

// app.Run();

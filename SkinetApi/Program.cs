using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using SkinetApi;
using SkinetApi.Contracts;
using SkinetApi.Data;
using SkinetApi.Entities.Identity;
using SkinetApi.Extensions;
using SkinetApi.LoggerService;
using SkinetApi.Helpers;
using SkinetApi.Repositories;
using SkinetApi.MiddleWare;
using Microsoft.AspNetCore.Mvc;
using SkinetApi.Errors;

var builder = WebApplication.CreateBuilder(args);
#region  Including StartUp
//var  startup = new Startup(builder.Configuration);

//startup.ConfigureServices(builder.Services);
#endregion

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlServer("name = StoreDbConnection"));

//builder.Services.AddDbContext<StoreIdentityContext>(options =>
//    options.UseSqlServer("name = IdentityDbContext"));

builder.Services.AddApplicationServices();
builder.Services.AddSwaggerDoc();

builder.Services.AddIdentityServices();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleWare>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
   
}

app.UseStatusCodePagesWithReExecute("/errors/{0}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = services.GetRequiredService<StoreDbContext>();
        await context.Database.MigrateAsync();
        await SystemCodesSeed.SeedAsync(context, loggerFactory);
        await Seed.SeedAsync(context, loggerFactory);

        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var roleManager = services.GetRequiredService<RoleManager<Role>>();

        await IdentityUsersSeeding.SeedUserAsync(userManager, roleManager);
        
        
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred during Migration");
    }
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles();

app.UseAuthorization();

app.UseSwaggerDoc();

app.MapControllers();

app.Run();

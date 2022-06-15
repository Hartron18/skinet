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
using SkinetApi.Repositories;

var builder = WebApplication.CreateBuilder(args);
//var  startup = new Startup(builder.Configuration);

//startup.ConfigureServices(builder.Services);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlServer("name = StoreDbConnection"));

//builder.Services.AddDbContext<StoreIdentityContext>(options =>
//    options.UseSqlServer("name = IdentityDbContext"));

builder.Services.AddScoped<ILoggerManager, LoggerManager>();
builder.Services.AddScoped <IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IGeneralRepository, GeneralRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

//builder.Services.AddIdentityCore<AppUser>()
//    .AddEntityFrameworkStores<StoreDbContext>()
//    .AddSignInManager<SignInManager<AppUser>>();

//builder.Services.AddIdentityCore<Role>()
//    .AddEntityFrameworkStores<StoreDbContext>()
//    .AddRoleManager<RoleManager<AppUser>>();

builder.Services.AddIdentityServices();


LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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

app.UseAuthorization();

app.MapControllers();

app.Run();

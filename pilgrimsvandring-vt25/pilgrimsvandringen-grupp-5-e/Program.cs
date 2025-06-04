using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using pilgrimsvandringen_grupp_5_e.Data;
using pilgrimsvandringen_grupp_5_e.Models;
using pilgrimsvandringen_grupp_5_e.Repositories;
using pilgrimsvandringen_grupp_5_e.Repositories.Interfaces;
using pilgrimsvandringen_grupp_5_e.Services;
using pilgrimsvandringen_grupp_5_e.Services.Interfaces;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add sesssions
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ITrailInformationService, TrailInformationService>();
builder.Services.AddScoped<ITrailRepository, TrailRepository>();
builder.Services.AddScoped<IPointOfinterestService, PointOfInterestService>();
builder.Services.AddScoped<IPointOfInterestRepository, PointOfInterestRepository>();
builder.Services.AddScoped<IStageInformationService, StageInformationService>();
builder.Services.AddScoped<IStageRepository, StageRepository>();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();


// api
builder.Services.AddHttpClient<ApiEngine>();
builder.Services.AddScoped<ApiEngine>();

var config = new ConfigurationBuilder()
.AddUserSecrets<Program>()
.Build();

// database connection
builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Scoped
                );

builder.Services.AddDbContext<CustomIdentityDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Scoped
                );

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Loginfunction for user
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<CustomIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var messageTypeManager = scope.ServiceProvider.GetRequiredService<IMessageService>();
    var localRoleManager = scope.ServiceProvider.GetRequiredService<IAccountService>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    await SeedRoles(roleManager, localRoleManager);
    await SeedMessageTypes(messageTypeManager);
}

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();

// Borrowed from Erik. 
static async Task SeedRoles(RoleManager<IdentityRole> roleManager, IAccountService accountService)
{
    if (!await roleManager.RoleExistsAsync("admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("admin"));
    }

    if (!await accountService.CheckRoleExistensAsync("admin"))
    {
        await accountService.SeedRolesAsync(new Role("admin"));
    }

    if (!await roleManager.RoleExistsAsync("user"))
    {
        await roleManager.CreateAsync(new IdentityRole("user"));
    }

    if (!await accountService.CheckRoleExistensAsync("user"))
    {
        await accountService.SeedRolesAsync(new Role("user"));
    }
}

static async Task SeedMessageTypes(IMessageService messageService)
{
    if (!await messageService.CheckMessageTypeExistensAsync("guestbook"))
    {
        messageService.SeedMessageTypesAsync(new MessageType("guestbook"));
    }

    if (!await messageService.CheckMessageTypeExistensAsync("notification"))
    {
        messageService.SeedMessageTypesAsync(new MessageType("notification"));
    }
}
using FlyingProject.CQRS.CQRSContracts;
using FlyingProject.CQRS.Flights.FlightOrchestor;
using FlyingProject.Project.core;
using FlyingProject.Project.core.DTOS.ViewModles;
using FlyingProject.Project.core.Entities.Identity;
using FlyingProject.Project.core.MappingProfiles;
using FlyingProject.Project.core.NewFolder.InterfaceContrect;
using FlyingProject.Project.core.ServiceContrect;
using FlyingProject.Project.Repo;
using FlyingProject.Project.Repo.Data.Context;
using FlyingProject.Project.Repo.Repositories;
using FlyingProject.Project.Service;
using FlyingProject.Shared.attachmentService;
using FlyingProject.Shared.DbInitializerService;
using FlyingProject.Shared.MiddleWares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using static FlyingProject.Project.Service.FlightAvailabilityService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);



// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AirlineDbContext>(x => 
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
    /*.LogTo(log=>Debug.WriteLine(log),LogLevel.Information)*/;

});

builder.Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, option =>
               option.TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidateIssuer = true,
                   ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
                   ValidateAudience = true,
                   ValidAudience = builder.Configuration["Jwt:ValidAudience"],
                   ValidateLifetime = true,
                   ClockSkew = TimeSpan.Zero,
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:AuthKey"] ?? string.Empty))

               }
               );




builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<AirlineDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 4;
});



builder.Services.AddAutoMapper(x => x.AddProfile(new FlightProfile()));

builder.Services.AddScoped(typeof(IRepo<>), typeof(Repository<>));

builder.Services.AddScoped<IunitofWork, UnitOfwork>();
builder.Services.AddScoped<IFlightRepo, FlyightRepo>();
builder.Services.AddScoped<IUpdateFlightsCommandOrchestrator, FlightUpdateOrchestor>();
builder.Services.AddScoped<IFlightAvailabilityService, FlightAvailabilityServices>();
builder.Services.AddScoped<IFlightBookOrchestor, FlightBookOrchestor>();

builder.Services.AddScoped<IattachmentService, attachmentService>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();


builder.Services.AddScoped<TransactionMiddlerWare>();

builder.Services.AddScoped<IJwtService, JwtService>();


builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
});

var app = builder.Build();

app.UseStaticFiles();
using var scope = app.Services.CreateScope();
var service = scope.ServiceProvider;
var _dbcontext = service.GetRequiredService<AirlineDbContext>();
var dbInitializer = service.GetRequiredService<IDbInitializer>();

try
{
    _dbcontext.Database.Migrate();
    await dbInitializer.InitializeIdentityAsync();

}
catch (Exception ex)
{

    var logger = service.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An Error Occurred During Apply the Migration");
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.MapGet("/{name}", async context =>
{
    var name = context.GetRouteValue("name");
   await context.Response.WriteAsync($"Heloo{name}");
});

app.UseAuthentication();
app.UseAuthorization();

// Use Transaction Middleware (before controllers)
app.UseMiddleware<TransactionMiddlerWare>();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Flight}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

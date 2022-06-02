using ComicBookStoreAPI.Database;
using ComicBookStoreAPI.Database.Helpers;
using ComicBookStoreAPI.Database.Managers;
using ComicBookStoreAPI.Database.Repository;
using ComicBookStoreAPI.Database.Seeders;
using ComicBookStoreAPI.Domain.Authorization.RequirementHandler;
using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Interfaces.DbContext;
using ComicBookStoreAPI.Domain.Interfaces.Helpers;
using ComicBookStoreAPI.Domain.Interfaces.Repositories;
using ComicBookStoreAPI.Domain.Interfaces.Services;
using ComicBookStoreAPI.Domain.Models.Settings;
using ComicBookStoreAPI.Middleware;
using ComicBookStoreAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(config =>
    {
        config.SignIn.RequireConfirmedEmail = true;
    }).AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
builder.Services.AddScoped<ComicBookSeeder>();
builder.Services.AddScoped<UserSeeder>();


builder.Services.AddScoped<IAuthorizationHandler, ComicBookResourceOperationRequirementHandler>();

builder.Services.AddScoped<IEntityHelper, EntityHelper>();


builder.Services.AddScoped<IRepository<ComicBookIllustrator, ComicBook, Illustrator>, ComicBookIllustratorRepository>();
builder.Services.AddScoped<IRepository<ComicBookHeroesTeams, ComicBook, HeroesTeams>, ComicBookHeroesTeamsRepository>();


builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IComicBookManager, ComicBookManager>();
builder.Services.AddScoped<IRatingManager, RatingManager>();

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.AddAuthentication()
    .AddGoogle(opt =>
    {
        opt.ClientId = builder.Configuration.GetValue<string>("ExternalAuthentication:Google:ClientId");
        opt.ClientSecret = builder.Configuration.GetValue<string>("ExternalAuthentication:Google:ClientSecret");
    });



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.


var scope = app.Services.CreateScope();
var comicbookSeeder = scope.ServiceProvider.GetRequiredService<ComicBookSeeder>();
var userSeeder = scope.ServiceProvider.GetRequiredService<UserSeeder>();

comicbookSeeder.Seed();
await userSeeder.Seed();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

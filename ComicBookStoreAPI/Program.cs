using ComicBookStoreAPI.Database;
using ComicBookStoreAPI.Database.Repository;
using ComicBookStoreAPI.Database.Seeders;
using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Interfaces.DbContext;
using ComicBookStoreAPI.Domain.Interfaces.Repositories;
using ComicBookStoreAPI.Domain.Interfaces.Services;
using ComicBookStoreAPI.Middleware;
using ComicBookStoreAPI.Services;
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


builder.Services.AddScoped<IRepository<ComicBook>, Repository<ComicBook>>();
builder.Services.AddScoped<IRepository<Screenwriter>, Repository<Screenwriter>>();
builder.Services.AddScoped<IRepository<Translator>, Repository<Translator>>();
builder.Services.AddScoped<IRepository<Series>, Repository<Series>>();
builder.Services.AddScoped<IRepository<CoverType>, Repository<CoverType>>();
builder.Services.AddScoped<IRepository<Illustrator>, Repository<Illustrator>>();
builder.Services.AddScoped<IRepository<HeroesTeams>, Repository<HeroesTeams>>();
builder.Services.AddScoped<IRepository<ComicBookIllustrator, ComicBook, Illustrator>, ComicBookIllustratorRepository>();
builder.Services.AddScoped<IRepository<ComicBookHeroesTeams, ComicBook, HeroesTeams>, ComicBookHeroesTeamsRepository>();


builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IComicBooksService, ComicBooksService>();

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
var seeder = scope.ServiceProvider.GetRequiredService<ComicBookSeeder>();

seeder.Seed();

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

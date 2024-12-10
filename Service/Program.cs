using DAL;
using BLL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure the PostgreSQL database connection
builder.Services.AddDbContext<SalesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SalesDb")));

// Register IRepository and EFRepository
builder.Services.AddScoped<IRepository, EFRepository>();

// Register Business Logic for Products and Categories
builder.Services.AddScoped<ProductLogic>();
builder.Services.AddScoped<CategoryLogic>();

builder.Services.AddControllers();

var app = builder.Build();

RepositoryFactory.Configure(app.Services);

app.UseAuthorization();
app.MapControllers();

app.Run();
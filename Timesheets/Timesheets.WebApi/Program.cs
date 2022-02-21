using Timesheets.Domain;
using Timesheets.Storage;
using Timesheets.Storage.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TimesheetsDb>(options =>
	options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));

builder.Services.AddControllers();
builder.Services.AddScoped<IRepository<Person>, LocalRepositoryPerson>();

builder.Services.AddScoped<IRepositoryDB<Employee>, DbEntityRepository<Employee>>();
builder.Services.AddScoped<IRepositoryDB<User>, DbEntityRepository<User>>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

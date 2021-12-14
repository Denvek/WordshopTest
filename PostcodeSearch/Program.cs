using Microsoft.EntityFrameworkCore;
using PostcodeSearch.Db;
using PostcodeSearch.Services;
using PostcodeSearch.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var connectionString = builder.Configuration.GetConnectionString("PostcodeDb");
builder.Services.AddDbContext<PostcodeDbContext>(opts=>opts.UseSqlServer(connectionString));
builder.Services.AddScoped<IPostcodeRepo, DbPostcodeRepo>();
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

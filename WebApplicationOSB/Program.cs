using Microsoft.EntityFrameworkCore;
using WebApplicationOSB;
using WebApplicationOSB.Models;
using WebApplicationOSB.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CampusDbConnection")));

builder.Services.AddScoped<GetAboneBilgi>();
builder.Services.AddScoped<FaturaService>();

builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

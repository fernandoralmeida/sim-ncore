using Microsoft.AspNetCore.Identity;
using Sim.Identity.Context;
using Sim.Identity.Entity;
using Sim.Identity.IoC;
using Sim.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.IdentityDataBase(builder.Configuration, "IdentityContextConnection");

builder.Services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>();
                
builder.Services.IdentityConfig();

builder.Services.RegisterServices();
builder.Services.RegisterServicesCNPJ();

builder.Services.AddControllers();
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

await app.RunAsync();

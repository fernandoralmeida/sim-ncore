using Microsoft.AspNetCore.Identity;
using Sim.Identity.Context;
using Sim.Identity.Entity;
using Sim.Identity.IoC;
using Sim.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "allowAny",
        policy => {
            policy.AllowAnyOrigin().WithMethods("GET");
    });
});

// Add services to the container.
builder.Services.IdentityDataBase(builder.Configuration, "IdentityContextConnection");

builder.Services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>();
                
builder.Services.IdentityConfig();
builder.Services.DataBaseConfig(builder.Configuration, "App_____ContextConnection");
builder.Services.RegisterServices();
builder.Services.DataBaseConfigCNPJ(builder.Configuration, "RFB_____ContextConnection");
builder.Services.RegisterServicesCNPJ();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();


var app = builder.Build();
app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors();

app.UseResponseCaching();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();

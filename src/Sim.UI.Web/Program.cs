using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sim.Identity.Context;
using Sim.Identity.Entity;
using Sim.Identity.Interfaces;
using Sim.Identity.Repository;
using Sim.IoC;
using Sim.UI.Web.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<IdentityContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityContextConnection"));
});

builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>();

builder.Services.AddScoped<IServiceUser, RepositoryUser>();

builder.Services.Configure<IdentityOptions>(options => {
    options.SignIn.RequireConfirmedAccount = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
    options.Lockout.AllowedForNewUsers = false;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 5;
});

builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(op =>
    op.SerializerSettings.ReferenceLoopHandling =
    Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.DataBaseConfig(builder.Configuration, "App_____ContextConnection");
builder.Services.DataBaseConfigCNPJ(builder.Configuration, "RFB_____ContextConnection");

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.RegisterServices();
builder.Services.RegisterServicesCNPJ();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

await app.RunAsync();

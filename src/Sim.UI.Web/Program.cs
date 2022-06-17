using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sim.Identity.Context;
using Sim.Identity.Entity;
using Sim.Identity.Interfaces;
using Sim.Identity.Repository;
using Sim.IoC;

var builder = WebApplication.CreateBuilder(args);

#region Identity
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
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(720);
    options.Lockout.MaxFailedAccessAttempts = 5;
});
#endregion

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(op =>
    op.SerializerSettings.ReferenceLoopHandling =
    Newtonsoft.Json.ReferenceLoopHandling.Ignore);

Container.RegisterDataContext(builder.Services, builder.Configuration, "App_____ContextConnection");
Container.ApplicationContextServices(builder.Services);

var _mapper = new AutoMapper.MapperConfiguration(config => config.AddProfile(new Sim.UI.Web.AutoMapper.AutoMapperProfile()));
var mapper = _mapper.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddAutoMapper(typeof(WebApplication));

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

app.Run();

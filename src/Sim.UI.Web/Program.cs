using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Sim.Identity.IoC;
using Sim.Identity.Entity;
using Sim.Identity.Context;
using Sim.UI.Web.AutoMapper;
using Sim.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization();
var cultureInfoBR = new[]{ new CultureInfo("pt-BR")};

builder.Services.IdentityDataBase(builder.Configuration, "IdentityContextConnection");

builder.Services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>();

builder.Services.IdentityConfig();

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

builder.Services.AddScoped<HttpContextAccessor>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
    app.UseDeveloperExceptionPage();

app.UseRequestLocalization(new RequestLocalizationOptions {
    DefaultRequestCulture = new RequestCulture("pt-BR"),
    SupportedCultures = cultureInfoBR,
    FallBackToParentCultures= false
});
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CreateSpecificCulture("pt-BR");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
//app.UseResponseCaching();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

await app.RunAsync();

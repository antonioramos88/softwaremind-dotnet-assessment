using Data.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SoftwareMindWeb.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddPooledDbContextFactory<SoftwareMindContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<SoftwareMindScopedFactory>();
builder.Services.AddScoped(
    sp => sp.GetRequiredService<SoftwareMindScopedFactory>().CreateDbContext());

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("WebAuthentication", policy =>
        policy.Requirements.Add(new WebAuthenticationRequirement()));
});
builder.Services.AddSingleton<IAuthorizationHandler, WebAuthenticationHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseCors(opts => opts.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

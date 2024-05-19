using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using shop.data;
using shop.Data;
using shop.Models;
using shop.Services;
using System;
using shop.Interfaces;
using shop.Repositories;
using Autofac.Core;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IHomeRepository, HomeRepository>();
builder.Services.AddScoped<ICookieService, CookieService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICheckoutRepository, CheckoutRepository>();
builder.Services.AddScoped<IAdminDashboardRepository, AdminDashboardRepository>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthorization();
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "en-US", "fr-FR", "de-DE", "pl-PL" };
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
    options.SupportedUICultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Seed users and roles
//await Seed.SeedUsersAndRolesAsync(app);
//Seed.SeedGames(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

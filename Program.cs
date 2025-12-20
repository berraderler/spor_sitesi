using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Spor_web_sitesi.Data;
using Spor_web_sitesi.Identity;
using System;

var builder = WebApplication.CreateBuilder(args);

// --- EKLE: Session (Oturum) deste�i ---
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30); // 30 dk i�lem yapmazsa atar
});

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";      // AuthN yoksa
    options.AccessDeniedPath = "/Account/AccessDenied"; // Yetki yoksa
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// --- EKLE: Session kullan�m�n� a� ---
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
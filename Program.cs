using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using spor_sitesi.Data;

var builder = WebApplication.CreateBuilder(args);

// DbContext kaydı
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity + Roles
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// MVC + Razor Pages (Identity arayüzü için)
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();


// 🔹 ADMIN ROLÜ VE ADMIN KULLANICI OLUŞTURMA BLOĞU
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    const string adminRoleName = "Admin";

    // Rol yoksa oluştur
    if (!roleManager.RoleExistsAsync(adminRoleName).Result)
    {
        roleManager.CreateAsync(new IdentityRole(adminRoleName)).Wait();
    }

    // Admin kullanıcı bilgileri
    string adminEmail = "admin@spor.com";
    string adminPassword = "Admin123!";

    var adminUser = userManager.FindByEmailAsync(adminEmail).Result;
    if (adminUser == null)
    {
        adminUser = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = userManager.CreateAsync(adminUser, adminPassword).Result;
        if (result.Succeeded)
        {
            userManager.AddToRoleAsync(adminUser, adminRoleName).Wait();
        }
    }
}
// 🔹 BLOK BİTTİ


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// kimlik doğrulama + yetkilendirme
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Identity’nin hazır Login/Register sayfaları için
app.MapRazorPages();

app.Run();

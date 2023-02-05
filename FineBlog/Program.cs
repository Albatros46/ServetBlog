using FineBlog.Data;
using FineBlog.Models;
using FineBlog.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Veritabani baglantisi
var connectionStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options=>options.UseSqlServer(connectionStr));

//Identity islemleri
builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

//IDbInitializer tanimi
builder.Services.AddScoped<IDbInitializer, DbInitializer>();//daha sonra dataseeding metodu asagiya tanimlancak

var app = builder.Build();
Dataseeding();//metod yazildiktan sonra burada calistirilacak

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

app.UseAuthorization();

app.MapControllerRoute(//admin paneli icin
    name: "area",
    //pattern: "{controller=Home}/{action=Index}/{id?}");
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(//normal sayfa icin
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
https://www.youtube.com/watch?v=7MolpkyVgZs&list=PLIyNJGgXfvmp92nzScIN0ZIPA_C6O72OP&index=7
app.Run();
void Dataseeding()
{
    using(var scope = app.Services.CreateScope())
    {
        var DbInitializer= scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        DbInitializer.Initialize();
    }
}

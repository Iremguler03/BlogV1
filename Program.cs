using BlogV1.Context;
using BlogV1.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//
builder.WebHost.UseWebRoot("wwwroot");
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BlogDbContext>();
//ıdentity db context baglantisi
builder.Services.AddDbContext<BlogIdentityDbContext>(options =>
{
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

//admin giris yapmadiginda kullanicilari otomatik bir sayfaya yonlendirme
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Blogs/Index"; 
    });

builder.Services.AddIdentity<BlogIdentityUser, BlogIdentityRole>()
    .AddEntityFrameworkStores<BlogDbContext>()
    .AddDefaultTokenProviders();
 
var app = builder.Build();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Index}/{id?}");

app.Run();

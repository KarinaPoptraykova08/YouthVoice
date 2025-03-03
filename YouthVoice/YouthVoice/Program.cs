using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.Cookies;
using YouthVoice.Models;
using YouthVoice.Data;
using Microsoft.EntityFrameworkCore;
using Google;

var builder = WebApplication.CreateBuilder(args);

// Add Firebase Admin SDK
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("wwwroot/firebase-key.json")
});

builder.Services.AddDbContext<YouthVoiceDbContext>(options =>
{
    options.UseSqlServer("Server=LAPTOP-PHRSI0RL\\SQLEXPRESS;Database=YouthVoiceDb;Trusted_Connection=True;TrustServerCertificate=True");
});


builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Authentication/SignIn";
    });

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

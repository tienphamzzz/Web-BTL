using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Web_BTL.Repository;
using Web_BTL.Services.Cookie;
using Web_BTL.Services.EmailServices;
using Web_BTL.Services.UploadFile;
using Xabe.FFmpeg;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.UseKestrel().ConfigureKestrel((context, options) =>
//{
//    options.Configure(context.Configuration.GetSection("Kestrel"));
//});

// Cấu hình giới hạn request body cho Kestrel
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 100000000; // 100 MB
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 100000000; // Cấu hình giới hạn cho các form (100 MB)
});

// connection Database
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectedDb"));
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("EmailSetting")); // c?u hình kh?i t?o cho Email phía máy ch?
builder.Services.AddTransient<SendEmail>(); // s? d?ng d?ch v? g?i tin nh?n
builder.Services.AddScoped<CookieService>(); // thêm service Cookie
builder.Services.AddScoped<SaveImageVideo>(); // theem service SaveImageVideo
FFmpeg.SetExecutablesPath("C:/ffmpeg/bin"); // sử dụng ffmpeg bằng xabe.FFmpeg(có thể thay bằng mediatoolkit) và cấu hình đường dẫn thư mục

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
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
SeedData.SeedingData(context);
app.Run();

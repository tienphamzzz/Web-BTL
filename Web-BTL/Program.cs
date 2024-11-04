using Microsoft.EntityFrameworkCore;
using Web_BTL.Repository;
using Web_BTL.Services.Cookie;
using Web_BTL.Services.EmailServices;
using Web_BTL.Services.UploadFile;

var builder = WebApplication.CreateBuilder(args);
// connection Database
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectedDb"));
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromDays(7); // quá 7 ngày không sử dụng sẽ tự động xoá session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("EmailSetting")); // c?u hình kh?i t?o cho Email phía máy ch?
builder.Services.AddTransient<SendEmail>(); // sử dụng dịch vụ gửi tin nhắn
builder.Services.AddScoped<SaveImageVideo>(); // sử dụng dịch vụ lưu file(image và video)
builder.Services.AddScoped<CookieService>(); // thêm service Cookie 
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
// SeedData.SeedingData(context); đã không còn cần thiết
app.Run();

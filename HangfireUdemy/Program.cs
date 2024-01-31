using Hangfire;
using HangfireUdemy.Services;

var builder = WebApplication.CreateBuilder(args);

// IConfiguration nesnesini almak için bir dependency injection ekliyoruz.
builder.Services.AddSingleton(builder.Configuration);

builder.Services.AddScoped<IEmailSender, EmailSender>();
// Add services to the container.
builder.Services.AddControllersWithViews();

// Hangfire'ý yapýlandýrma iþlemleri
var hangfireConnectionString = builder.Configuration.GetConnectionString("MsSqlConnection");
builder.Services.AddHangfire(config => config.UseSqlServerStorage(hangfireConnectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Hangfire'ýn arkaplanda çalýþabilmesi için gerekli bileþenleri ekliyoruz.
app.UseHangfireServer();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Hangfire Dashboard'u kullanmak istiyorsanýz bu satýrý ekleyebilirsiniz.
app.UseHangfireDashboard("/hangfire");

app.Run();

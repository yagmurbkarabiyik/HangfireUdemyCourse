using Hangfire;
using HangfireUdemy.Services;

var builder = WebApplication.CreateBuilder(args);

// IConfiguration nesnesini almak i�in bir dependency injection ekliyoruz.
builder.Services.AddSingleton(builder.Configuration);

builder.Services.AddScoped<IEmailSender, EmailSender>();
// Add services to the container.
builder.Services.AddControllersWithViews();

// Hangfire'� yap�land�rma i�lemleri
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

// Hangfire'�n arkaplanda �al��abilmesi i�in gerekli bile�enleri ekliyoruz.
app.UseHangfireServer();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Hangfire Dashboard'u kullanmak istiyorsan�z bu sat�r� ekleyebilirsiniz.
app.UseHangfireDashboard("/hangfire");

app.Run();

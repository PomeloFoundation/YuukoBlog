using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using YuukoBlog;
using YuukoBlog.Authentication;
using YuukoBlog.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BlogContext>(x => x.UseSqlite("Data source=blog.db"));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
        options.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ssZ";
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    })
    .AddControllersAsServices();
builder.Services.AddAuthentication(x => x.DefaultScheme = TokenAuthenticateHandler.Scheme)
    .AddPersonalAccessToken();

var app = builder.Build();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseDeveloperExceptionPage();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
});
app.UsePueMiddleware();

using (var scope = app.Services.CreateScope())
{
    await SampleData.InitializeYuukoBlog(scope.ServiceProvider);
}

app.Run();

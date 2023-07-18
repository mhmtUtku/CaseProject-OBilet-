using CaseProject.Interface;
using CaseProject.MiddleWare;
using CaseProject.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mvcBuilder = builder.Services.AddControllersWithViews();
mvcBuilder.AddRazorRuntimeCompilation();

builder.Services.AddTransient<IUserSessionService, UserSessionService>();
builder.Services.AddTransient<IBusService, BusService>();
builder.Services.AddTransient<ICookieService, CookieService>();

builder.Services.AddBrowserDetection();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseMiddleware<UserDetailsMiddleWare>();

app.Run();


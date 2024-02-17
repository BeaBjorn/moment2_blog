var builder = WebApplication.CreateBuilder(args);
//Aktivate MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();
//wwwroot
app.UseStaticFiles();
//Routing   
app.UseRouting();

//Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();

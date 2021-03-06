using BethanysPieShop.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// getting the service support -- ConfugureServices
var services = builder.Services;
services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
services.AddControllersWithViews();
services.AddScoped<IPieRepository, PieRepository>();
services.AddScoped<ICategoryRepository, CategoryRepository>();
services.AddMemoryCache();
// create or check if there have any session associated with the current user 
services.AddScoped<ShoppingCart>(sp => ShoppingCart.GetCart(sp));

services.AddHttpContextAccessor();
services.AddSession();


// added the configure pipeline
var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
//setting the route config
app.UseEndpoints(endpoint =>
{
    endpoint.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();

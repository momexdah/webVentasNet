using WebSistemaVentas.BLL;
using WebSistemaVentas.BLL.Implementation;
using WebSistemaVentas.UOW;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
//builder.Services.AddSingleton(typeof(IHttpContextAccessor), typeof(HttpContextAccessor));
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMantenimientosBL, MantenimientosBL>();
builder.Services.AddScoped<IConfiguracionesBL, ConfiguracionesBL>();


builder.Services.AddScoped<IUOWVentas>(opcion =>new UOWVentas(configuration.GetConnectionString("BDSistemaVentas")));

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

app.Run();
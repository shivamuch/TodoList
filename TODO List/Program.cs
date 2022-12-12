using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TODO_List.Data;
var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<TODOListContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("TODOListContext") ?? throw new InvalidOperationException("Connection string 'TODOListContext' not found.")));
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();
builder.Services.AddDbContext<TODOListContext>(options =>
{
    var ConnectionString = builder.Configuration.GetConnectionString("TODOListContext");
    options.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
});




// Add services to the container.
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=TODOList}/{action=Index}/{id?}");

app.Run();

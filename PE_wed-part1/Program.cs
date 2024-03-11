using Microsoft.EntityFrameworkCore;
using PE_wed_part1.DataAccess;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton(builder.Configuration);

var s = builder.Configuration.GetConnectionString("PE_PRN221");
builder.Services.AddDbContext<PePrn221Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PE_PRN221"))
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", context =>
    {
        context.Response.Redirect("/Students/List");
        return Task.CompletedTask;
    });

    // Các endpoints khác của ứng dụng
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.UseAuthorization();

app.MapRazorPages();

app.Run();

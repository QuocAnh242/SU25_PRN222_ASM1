using DNATestServiceManager.RazorWebApp.AnhTHQ.Hubs;
using DNATestServiceManager.Repositories.AnhTHQ.DBContext;
using DNATestServiceManager.Services.AnhTHQ;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<SU25_PRN222_SE1706_G6_DNATestServiceManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddSignalR(); 
        builder.Services.AddScoped<IServicesAnhTHQService, ServicesAnhTHQService>();
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.AccessDeniedPath = "/Account/Forbidden";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();
        app.MapHub<ChatHub>("/chatHub");
        app.MapHub<DNATestServiceManagerHub>("/DNATestServiceManagerHub");
        app.MapRazorPages().RequireAuthorization();

        app.Run();
    }
}

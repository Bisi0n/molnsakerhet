using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using molnsakerhet.Data;

namespace molnsakerhet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //L�gg till Azure Keyvault i konfiguration
            var keyVaultUri = new Uri("https://kyh-vault.vault.azure.net/");
            builder.Configuration.AddAzureKeyVault(keyVaultUri, new DefaultAzureCredential());

            // H�r konfigurerar vi v�r databas med connectionstr�ng fr�n appsettings.json
            builder.Services.AddDbContext<AppDbContext>(options => 
            options.UseNpgsql(builder.Configuration["DefaultConnection"]));


            // >> Om ni k�r SQL Server anv�nd .UseSqlServer
            // l�gg till i appsettings.json = "Server=localhost;Database=databasnamn;Integrated Security=True;"
            //

            //builder.Services.AddDbContext<AppDbContext>(options =>
            //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
        }
    }
}

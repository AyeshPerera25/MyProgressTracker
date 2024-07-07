using Microsoft.EntityFrameworkCore;
using MyProgressTracker.DataResources;
using MyProgressTracker.Handlers;
using MyProgressTracker.ServiceCore;

namespace MyProgressTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<InMemoryDBContext>(options => options.UseInMemoryDatabase("InMemoryDb")); // Config in memory db
            builder.Services.AddTransient<DummyDataInsertHandler>();
            builder.Services.AddTransient<SignInHandler>();
            builder.Services.AddTransient<ReportHandler>();
            builder.Services.AddTransient<CourseHandler>();
            builder.Services.AddTransient<SubjectHandler>();
            builder.Services.AddTransient<StudySessionHandler>();
            builder.Services.AddTransient<SystemServiceCore>();

			Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NCaF5cXmZCeUx3Rnxbf1x0ZFBMY1hbRXZPMyBoS35RckVkWXledXdRR2BVU0Ny");

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
                pattern: "{controller=Dashboard}/{action=DashboardView}/{id?}");

            app.Run();
        }
    }
}

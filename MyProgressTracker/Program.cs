using Microsoft.EntityFrameworkCore;
using MyProgressTracker.DataResources;
using MyProgressTracker.Handlers;
using MyProgressTracker.ServiceConnectors;
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
            builder.Services.AddTransient<LoginHandler>();
            builder.Services.AddTransient<ReportHandler>();
            builder.Services.AddTransient<CourseHandler>();
            builder.Services.AddTransient<AthenticationServiceConnector>();
            builder.Services.AddTransient<InquiryServiceConnector>();
            builder.Services.AddTransient<SubjectHandler>();
            builder.Services.AddTransient<StudySessionHandler>();
            builder.Services.AddTransient<SystemServiceCore>();
            builder.Services.AddDistributedMemoryCache();
			builder.Services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(30); // Set the session timeout
				options.Cookie.HttpOnly = true; // Make the session cookie HttpOnly
				options.Cookie.IsEssential = true; // Make the session cookie essential
			});

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
			// Enable session
			app.UseSession();

			app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=UserLogin}/{id?}");

            app.Run();
        }
    }
}

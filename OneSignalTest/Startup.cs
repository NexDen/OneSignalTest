using Microsoft.OpenApi.Models;
using OneSignalTest.Data;
using Microsoft.EntityFrameworkCore;
using OneSignalTest.Services;

namespace OneSignalTest;

public class Startup
{
    private IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "OneSignalTest", Version = "v1" });
        });
        services.AddHttpClient();
        services.AddControllers();
        
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        Console.WriteLine($"Connection string: {connectionString}");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.Parse("9.1.0"));
        });
        
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
            );
                
        });

        services.AddScoped<OneSignalService>();
    }
    
    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        
        var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
        
        _configuration = builder.Build();
        app.UseSwagger().UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "OneSignalTest v1");
        });
        

        app.UseCors("AllowAll");
        app.UseRouting();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
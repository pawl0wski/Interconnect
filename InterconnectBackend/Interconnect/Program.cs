using Database;
using Interconnect.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.Config;
using NativeLibrary;
using Services;
using Services.Impl;

namespace Interconnect
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            LibraryInitializer.Initialize(builder.Services);
            ServicesInitializer.Initialize(builder.Services);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<InterconnectConfig>(
                builder.Configuration.GetSection("Interconnect"));

            builder.Services.AddHostedService<InitializationService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalFrontend",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                    });
            });

            builder.Services.AddDbContext<InterconnectDbContext>((serviceProvider, options) =>
            {
                var config = serviceProvider.GetRequiredService<IOptions<InterconnectConfig>>();
                options.UseNpgsql(config.Value.DatabaseConnectionUrl);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowLocalFrontend");

            app.MapControllers();

            app.Run();
        }
    }
}

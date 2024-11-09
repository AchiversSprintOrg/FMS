
using Finance_Api.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FinanceWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
           
                //Configure Serilog
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                    .CreateLogger();

            Log.Information("Started");

            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog();
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<FinanceDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    policyBuilder => policyBuilder.AllowAnyOrigin()
                                                 .AllowAnyMethod()
                                                 .AllowAnyHeader());

            });
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

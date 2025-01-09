using System.Collections.Generic;
using System.Threading.Tasks;
using WebHost.Models;
using Data.Data;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Web;


public class Program
{
    
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add Swagger/OpenAPI support
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                },
            });

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "WebAPI",
                Description = "WebAPI App",
            });

        });
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        
        builder.Services.AddAuthorization();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        builder.Services.AddServices();
        builder.Services.AddControllers();
        builder.Services.AddHttpContextAccessor();
        
        builder.Services.AddAutoMapper(typeof(MappingProfile));

        var app = builder.Build();
        app.UseCors();
        
        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI");
            });
        }
        

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

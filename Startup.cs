using Data.Context;
using EvertecApi.Helpers.Respuestas;
using EvertecApi.Log4net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EvertecApi
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("CorsApi",
                    builder => builder.WithOrigins("http://localhost:4200", "https://localhost:7178")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });



            services.AddControllers();

            services.AddSwaggerGen();

            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton<ILoggerManager, LoggerManager>();

            //Configuración de la base de datos
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("database")));
        }

        public void configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
  
            app.UseSwagger();
            app.UseSwaggerUI();
   
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsApi");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}

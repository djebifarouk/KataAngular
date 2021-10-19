using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using KataAPI.DbModels;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using static KataAPI.Startup;


namespace KataAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
            //                                                       .AllowAnyMethod()
            //                                                        .AllowAnyHeader()));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("database"));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            var context = serviceProvider.GetService<ApiContext>();
            AddTestData(context);
            app.UseCorsMiddleware();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private static void AddTestData(ApiContext context)
        {



            List<Salle> sals = new List<Salle>();

            for (int i = 0; i < 10; i++) {
                sals.Add(new DbModels.Salle
                {
                    Id = i.ToString(),
                    Label = "room"+i
                });
                

            }

            List<Heur> Heurs = new List<Heur>();

            for (int h = 0; h < 24; h++)
            {
                Heurs.Add(new DbModels.Heur
                {
                    Id = h.ToString(),
                    Label = "De " + h + " à " + (h+1)
                });


            }
            context.Heurs.AddRange(Heurs);
            context.Salles.AddRange(sals);
            context.SaveChanges();
        }

        public class CorsMiddleware
        {
            private readonly RequestDelegate _next;

            public CorsMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public Task Invoke(HttpContext httpContext)
            {
                httpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                httpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                httpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept");
                httpContext.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,PUT,PATCH,DELETE,OPTIONS");
                return _next(httpContext);
            }
        }

        // Extension method used to add the middleware to the HTTP request pipeline.  
      

    }
    public static class CorsMiddlewareExtensions
    {
        public static IApplicationBuilder UseCorsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorsMiddleware>();
        }
    }
}

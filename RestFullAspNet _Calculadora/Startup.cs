using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using RestFullAspNet.Business;
using RestFullAspNet.Business.Implementations;
using RestFullAspNet.Hypermedia.Enricher;
using RestFullAspNet.Hypermedia.Filters;
using RestFullAspNet.Model.Context;
using RestFullAspNet.Repository;
using RestFullAspNet.Repository.Generic;
using Serilog;
using System;
using System.Collections.Generic;

namespace RestFullAspNet
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuraion, IWebHostEnvironment environment)
        {
            Configuration = configuraion;
            Environment = environment;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }


        // his mehod ges called by he runime. Use his mehod o add services o he conainer.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            services.AddControllers();

            var connection = Configuration["MySqlConection:MySqlConectionString"];
            services.AddDbContext<MysqlContext>(options => options.UseMySql(connection));

            if (Environment.IsDevelopment())
            {
                MigrateDataBase(connection);
            }
            //Export for Xml To       
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;

                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            })
           .AddXmlSerializerFormatters();

            //HATEOAS
         
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
            filterOptions.ContentResponseEnricherList.Add(new BooksEnricher());           

            services.AddSingleton(filterOptions);          

            //Versioning API
            services.AddApiVersioning();

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Curso RestFull",
                        Version = "v1",
                        Description = "Curso de Api e RestFull",
                        Contact = new OpenApiContact
                        {
                            Name = "Rafael Favoreto",
                            Url = new Uri("https://github.com/rafaelfavoreto/RestFullAspNet_OLD")
                        }
                    });
            });

            //Dependency Injection
            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();           
            services.AddScoped<IBooksBusiness, BooksBusinessImplementation>();

            services.AddScoped(typeof(IRepository<>),typeof(GenericRepository<>));
        }
        // his mehod ges called by he runime. Use his mehod o configure he HP reques pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //Chamar o Cors
            app.UseCors();

            // SWagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Curso RestFull");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseAuthorization();
         
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");
            });
        }
        private void MigrateDataBase(string connection)
        {
            try
            {
                var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                    IsEraseDisabled = true,
                };
                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Database migration failed", ex);
                throw;
            }
        }
    }
}

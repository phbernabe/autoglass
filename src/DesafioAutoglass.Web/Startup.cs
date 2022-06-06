using DesafioAutoglass.Application.AutoMapper;
using DesafioAutoglass.Application.Interfaces;
using DesafioAutoglass.Application.Services;
using DesafioAutoglass.Data;
using DesafioAutoglass.Data.Repositories;
using DesafioAutoglass.Data.UoW;
using DesafioAutoglass.Domain.Interfaces;
using DesafioAutoglass.Domain.Repositories;
using DesafioAutoglass.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DesafioAutoglass.Application
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
            services.AddDbContext<ApplicationDbContext>(options => options
                .UseLazyLoadingProxies()
                .UseSqlServer(Configuration.GetConnectionString("Default"))
            );
            services.AddAutoMapper(AutoMapperConfig.RegisterMappings());
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DesafioAutoglass.Application", Version = "v1" });
            });

            services.AddScoped<DbContext, ApplicationDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddTransient<IProdutoAppService, ProdutoAppService>();
            services.AddTransient<IFornecedorAppService, FornecedorAppService>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<IFornecedorRepository, FornecedorRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DesafioAutoglass.Application v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

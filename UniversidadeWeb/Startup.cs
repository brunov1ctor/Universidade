using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Universidade.Repositorio;
using Universidade.Data;

namespace Universidade
{
    public class Startup
    {
        public Startup(IConfiguration configuration,IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env; 
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }  
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddScoped<DbSession>();
            services.AddTransient<IAlunoRepositorio, AlunoRepositorio>();
            services.AddTransient<ICursoRepositorio, CursoRepositorio>();
            services.AddTransient<IDisciplinaRepositorio, DisciplinaRepositorio>();
            services.AddTransient<INotasRepositorio, NotasRepositorio>();
            services.AddTransient<IAprovacaoCursoRepositorio, AprovacaoCursoRepositorio>();
            services.AddTransient<IAprovacaoDisciplinaRepositorio, AprovacaoDisciplinaRepositorio>();
            services.AddTransient<IAprovacaoAlunoRepositorio, AprovacaoAlunoRepositorio>();
            services.AddRazorPages().AddRazorRuntimeCompilation();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

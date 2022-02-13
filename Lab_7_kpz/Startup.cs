using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lab_7_kpz
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
           // services.AddControllers();

            services.AddControllers().AddXmlDataContractSerializerFormatters();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            

            

            string connection = "Server=localhost,1433; Database=Clientsdb; MultipleActiveResultSets=true; User=sa;Password=Mysql1892;  Trusted_Connection=false";
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));

            services.AddTransient<Clients_repo, EF_clients_repo>();

            services.AddTransient<Entertainment_repo, EF_Entertainment_repo>();

            // services.AddSwaggerGen();

            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

        }
    }
}

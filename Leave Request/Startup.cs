using Leave_Request.Repositories.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NETCore.Context;

namespace Leave_Request
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
=========
            //services.AddControllers();
>>>>>>>>> Temporary merge branch 2
=========
            //services.AddControllers();
>>>>>>>>> Temporary merge branch 2
            services.AddControllersWithViews();

            //scope tiap repository
            services.AddScoped<AccountRepository>();
            services.AddScoped<AccountRoleRepository>();
            services.AddScoped<EmployeeRepository>();
            services.AddScoped<JobRepository>();
            services.AddScoped<LeaveRequestRepository>();
            services.AddScoped<LeaveTypeRepository>();
            services.AddScoped<ManagerFillRepository>();
            services.AddScoped<ReligionRepository>();
            services.AddScoped<RoleRepository>();
            services.AddScoped<StatusRepository>();

            services.AddDbContext<MyContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("NETCoreContext"))
            );
            //services.AddDbContext<MyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("NETCoreContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

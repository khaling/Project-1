using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PizzaBox.Storing;
using PizzaBox.Storing.Repository;

namespace PizzaBox.WebClient
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
      services.AddControllersWithViews();
      services.AddDbContext<PizzaBoxContext>(options => options.UseSqlServer(Configuration.GetConnectionString("sqlserver")));
     
      services.AddScoped<PizzaBoxRepository>();
      services.AddScoped<IStoreRepository, StoreRepository>();
      services.AddScoped<IOrderRepository, OrderRepository>();
      services.AddScoped<ICrustRepository, CrustRepository>();
      services.AddScoped<IToppingsRepository, ToppingsRepository>();
      services.AddScoped<ISizeRepository, SizeRepository>();
      services.AddScoped<IPizzaRepository, PizzaRepository>();
      services.AddScoped<ICustomerRepository, CustomerRepository>();
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
      //app.UseHttpsRedirection();
      //app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        // endpoints.MapControllerRoute(
        //           name: "default",
        //           pattern: "{controller=Customer}/{action=Home}/{id?}");
        
      });
    }
  }
}
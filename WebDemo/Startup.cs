using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreRpc.Server;
using IRpcService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebDemo.Filters;
using WebDemo.Options;
using WebDemo.RpcServices;

namespace WebDemo
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
            services.AddControllers();
            //*注册DotNetCoreRpcServer
            services.AddSingleton<IPersonService, PersonService>().AddSingleton(
                new RedisConfigOptions { Address = "127.0.0.1:6379", Db = 10 }).AddDotNetCoreRpcServer(options =>
        {
            //*确保添加的契约服务接口事先已经被注册到DI容器中

            //添加契约接口
            //options.AddService<IPersonService>();

            //或添加契约接口名称以xxx为结尾的
            //options.AddService("*Service");

            //或添加具体名称为xxx的契约接口
            //options.AddService("IPersonService");

            //或扫描具体命名空间下的契约接口
            options.AddNameSpace("IRpcService");

            //可以添加全局过滤器,实现方式和CacheFilterAttribute一致
            options.AddFilter<LoggerFilterAttribute>();
        });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseHttpsRedirection();

            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            //这一堆可以不要+1
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //添加DotNetCoreRpc中间件既可
            app.UseDotNetCoreRpc();

            //这一堆可以不要+2
            app.UseRouting();

            //这一堆可以不要+3
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Server Start!");
                });
            });
        }
    }
}

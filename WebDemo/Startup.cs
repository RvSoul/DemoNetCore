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
            //*ע��DotNetCoreRpcServer
            services.AddSingleton<IPersonService, PersonService>().AddSingleton(
                new RedisConfigOptions { Address = "127.0.0.1:6379", Db = 10 }).AddDotNetCoreRpcServer(options =>
        {
            //*ȷ����ӵ���Լ����ӿ������Ѿ���ע�ᵽDI������

            //�����Լ�ӿ�
            //options.AddService<IPersonService>();

            //�������Լ�ӿ�������xxxΪ��β��
            //options.AddService("*Service");

            //����Ӿ�������Ϊxxx����Լ�ӿ�
            //options.AddService("IPersonService");

            //��ɨ����������ռ��µ���Լ�ӿ�
            options.AddNameSpace("IRpcService");

            //�������ȫ�ֹ�����,ʵ�ַ�ʽ��CacheFilterAttributeһ��
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

            //��һ�ѿ��Բ�Ҫ+1
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //���DotNetCoreRpc�м���ȿ�
            app.UseDotNetCoreRpc();

            //��һ�ѿ��Բ�Ҫ+2
            app.UseRouting();

            //��һ�ѿ��Բ�Ҫ+3
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

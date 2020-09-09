using Demo.Model.CM;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using LinuxDemo.Controllers;
using Demo.Domian;
using System.Reflection;
using System.Linq;
using log4net.Repository;
using log4net;
using log4net.Config;
using System.IO;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using LinuxDemo.ApiAttribute;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace LinuxDemo
{
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ������־
        /// </summary>
        public static ILoggerRepository Repository { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //������־
            Repository = LogManager.CreateRepository("AprilLog");
            XmlConfigurator.Configure(Repository, new FileInfo("log4net.config"));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.MaxModelValidationErrors = 5;
                options.Filters.Add<VldFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            // ʹ���Զ���ģ����֤
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            //services.Configure<ApiBehaviorOptions>(options =>
            //{        //options.SuppressModelStateInvalidFilter = true; // ʹ���Զ���ģ����֤

            //    options.InvalidModelStateResponseFactory = (context) =>
            //    {
            //        StringBuilder errTxt = new StringBuilder();
            //        foreach (var item in context.ModelState.Values)
            //        {
            //            foreach (var error in item.Errors)
            //            {
            //                errTxt.Append(error.ErrorMessage + "|");
            //            }
            //        }

            //        ApiResp result = new ApiResp(ApiRespCode.F400000, errTxt.ToString().Substring(0, errTxt.Length - 1));
            //        return new JsonResult(result);
            //    };

            //});

            #region ����API�ӿ�˵���ĵ�
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hello", Version = "v1" });
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "LinuxDemo.xml");
                c.IncludeXmlComments(xmlPath);
            });
            #endregion             

            #region ҵ���ע�뷽ʽ1
            Assembly BLL = Assembly.Load("Demo.Domian");
            Assembly IBLL = Assembly.Load("Demo.IDomian");
            var typesIBLL = IBLL.GetTypes();
            var typesBLL = BLL.GetTypes();
            foreach (var item in typesIBLL)
            {
                var name = item.Name.Substring(1);

                string implBLLImpName = name + "Imp";
                var impl = typesBLL.Where(w => w.Name == implBLLImpName).FirstOrDefault();

                if (impl != null)
                {
                    services.AddTransient(item, impl);
                }
            }

            #endregion

            //services.AddSingleton<IObjectModelValidator, NullObjectModelValidator>();

            #region ҵ���ע�뷽ʽ2
            //services.AddScoped<BasicsDataDM, BasicsDataDM>();
            //services.AddScoped<CompanyDM, CompanyDM>();
            #endregion

            #region ���ݲ�EF����ע��
            services.AddDbContext<TodoContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("QMConnection")));//, b => b.MigrationsAssembly("Responsibility")
            #endregion

            #region Ȩ������

            #endregion

            services.AddControllers();


        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            #region ����API�ӿ�˵���ĵ�
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "test V1");
                c.RoutePrefix = string.Empty;//�������������������������Ϊhttps://localhost:44334/swagger/index.html�������˾���https://localhost:44334/index.html
            });
            #endregion

            #region ҵ��㲻��Ҫע�룬ʹ��ʵ����ʵ�ֵ��÷�ʽ
            //BaseDM.service = app.ApplicationServices;
            #endregion

            #region Ȩ������

            #endregion

        }
    }
}

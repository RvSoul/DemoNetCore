using DotNetCoreRpc.Core;
using DotNetCoreRpc.Core.RpcBuilder;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDemo.Options;

namespace WebDemo.Filters
{
    //*要继承自抽象类RpcFilterAttribute
    public class CacheFilterAttribute : RpcFilterAttribute
    {
        public int CacheTime { get; set; }

        //*支持属性注入,可以是public或者private
        //*这里的FromServices并非Asp.Net Core命名空间下的,而是来自DotNetCoreRpc.Core命名空间
        [FromServices]
        private RedisConfigOptions RedisConfig { get; set; }

        [FromServices]
        public ILogger<CacheFilterAttribute> Logger { get; set; }

        public override async Task InvokeAsync(RpcContext context, RpcRequestDelegate next)
        {
            //Logger.LogInformation($"CacheFilterAttribute Begin,CacheTime=[{CacheTime}],Class=[{context..FullName}],Method=[{context.Method.Name}],Params=[{JsonConvert.SerializeObject(context.Parameters)}]");
            await next(context);
            Logger.LogInformation($"CacheFilterAttribute End,ReturnValue=[{JsonConvert.SerializeObject(context.ReturnValue)}]");
        }
    }
}

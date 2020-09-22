using DotNetCoreRpc.Core.RpcBuilder;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo.Filters
{
    public class LoggerFilterAttribute : RpcFilterAttribute
    {
        private readonly ILogger _logger;
        public LoggerFilterAttribute(ILogger<LoggerFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override async Task InvokeAsync(RpcContext context, RpcRequestDelegate next)
        {
            _logger.LogInformation($"LoggerFilterAttribute Begin,Class=[{context.TargetType.FullName}],Method=[{context.Method.Name}],Params=[{JsonConvert.SerializeObject(context.Parameters)}]");
            await next(context);
            _logger.LogInformation($"LoggerFilterAttribute End,ReturnValue=[{JsonConvert.SerializeObject(context.ReturnValue)}]");
        }
    }
}

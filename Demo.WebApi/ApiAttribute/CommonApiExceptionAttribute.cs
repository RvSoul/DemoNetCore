using Demo.DTO;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Demo.WebApi.ApiAttribute
{
    /// <summary>
    /// 
    /// </summary>
    public class CommonApiExceptionAttribute : ExceptionFilterAttribute
    {
        //protected static ILog log = LogManager.GetLogger(Startup.repository.Name, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 统一对调用异常信息进行处理，返回自定义的异常信息
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            ResultEntity<bool> result = new ResultEntity<bool>();
            if (context.Exception is UnAuthorizeException)
            {
                result.ErrorCode = Convert.ToInt32(Utility.ApiResultCode.UnAuthorize);
                result.Msg = context.Exception.Message;
            }
            else if (context.Exception is DMException)
            {
                LogUtil.Debug(context.Exception.Message, context.Exception);
                result.ErrorCode = Convert.ToInt32(Utility.ApiResultCode.Error);
                result.Msg = context.Exception.Message;
            }
            else if (context.Exception is EntityException)
            {
                LogUtil.Debug(context.Exception.Message, context.Exception);
                result.ErrorCode = Convert.ToInt32(Utility.ApiResultCode.Error);
                result.Msg = context.Exception.Message;
            }
            else
            {
                LogUtil.Error(context.Exception.Message, context.Exception);
                result.ErrorCode = Convert.ToInt32(Utility.ApiResultCode.Error);
                result.Msg = context.Exception.Message;
                //Utility.ApiResultMessage.MESSAGE_ERROR;
            }
            context.Result = new ApplicationErrorResult(result.ToJSON());
            context.HttpContext.Response.StatusCode = result.ErrorCode;

            base.OnException(context);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationErrorResult : ObjectResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public ApplicationErrorResult(object value) : base(value)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public ErrorResponse(string msg)
        {
            Message = msg;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object DeveloperMessage { get; set; }
    }
}

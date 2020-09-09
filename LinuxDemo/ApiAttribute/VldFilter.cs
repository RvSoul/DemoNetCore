using Demo.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace LinuxDemo.ApiAttribute
{
    public class VldFilter : IActionFilter
    {
        /// <summary>
        /// 执行到action时
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                StringBuilder errTxt = new StringBuilder();
                foreach (var item in context.ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        errTxt.Append(error.ErrorMessage + "|");
                    }
                }
                ResultEntity<bool> result = new ResultEntity<bool>();

                result.ErrorCode = Convert.ToInt32(Utility.ApiResultCode.Error);
                result.Msg = errTxt.ToString().Substring(0, errTxt.Length - 1);
                // api响应报文，多封装几个构造方法，这里使用模型验证失败的响应码和模型校验信息
                // ApiResp result = new ApiResp(ApiRespCode.F400000, errTxt.ToString().Substring(0, errTxt.Length - 1));
                context.Result = new ApplicationErrorResult(result.ToJSON());
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

    }

}

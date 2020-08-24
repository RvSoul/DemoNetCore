using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Demo.DTO.Enum
{
    /// <summary>
    /// Api返回接口码的枚举类
    /// </summary>
    public enum ApiResultCode
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        [Description("操作成功")]
        Succ = 0,
        /// <summary>
        /// 系统错误
        /// </summary>
        [Description("系统错误")]
        Error = 200,
        /// <summary>
        /// 接口未授权
        /// </summary>
        [Description("接口未授权")]
        UnAuthorize = 1024,
    }
}

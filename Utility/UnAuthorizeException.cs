using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    /// <summary>
    /// 授权失败异常
    /// </summary>
    public class UnAuthorizeException : ApplicationException
    {
        public UnAuthorizeException() { }
        public UnAuthorizeException(string message)
            : base(message) { }
        public UnAuthorizeException(string message, Exception inner)
            : base(message, inner) { }
    }
}

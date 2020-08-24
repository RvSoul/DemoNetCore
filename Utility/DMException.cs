using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    /// <summary>
    /// 业务处理异常
    /// </summary>
    public class DMException : ApplicationException
    {
        public DMException() { }
        public DMException(string message)
            : base(message) { }
        public DMException(string message, Exception inner)
            : base(message, inner) { }
    }
}

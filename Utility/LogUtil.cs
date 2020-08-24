using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class LogUtil
    {
        public static readonly ILog log = LogManager.GetLogger("AprilLog", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="obj"></param>
        public static void Debug(string msg, Exception obj = null)
        {
            if (log.IsDebugEnabled && !string.IsNullOrEmpty(msg))
            {
                if (obj == null)
                {
                    log.Debug(msg);
                }
                else
                {
                    log.Debug(msg, obj);
                }
            }
        }
        /// <summary>
        /// 日常日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="obj"></param>
        public static void Info(string msg, Exception obj = null)
        {
            if (log.IsInfoEnabled && !string.IsNullOrEmpty(msg))
            {
                if (obj == null)
                {
                    log.Info(msg);
                }
                else
                {
                    log.InfoFormat(msg, obj);
                }
            }
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="obj"></param>
        public static void Error(string msg, Exception obj = null)
        {
            if (log.IsErrorEnabled && !string.IsNullOrEmpty(msg))
            {
                if (obj == null)
                {
                    log.Error(msg);
                }
                else
                {
                    log.Error(msg, obj);
                }
            }
        }
        /// <summary>
        /// 重要日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="obj"></param>
        public static void Fatal(string msg, Exception obj = null)
        {
            if (log.IsFatalEnabled && !string.IsNullOrEmpty(msg))
            {
                if (obj == null)
                {
                    log.Fatal(msg);
                }
                else
                {
                    log.Fatal(msg, obj);
                }
            }
        }

    }
}

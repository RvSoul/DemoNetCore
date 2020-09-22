using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Utility;

namespace DemoConsoleAppCore
{
    /// <summary>
    /// StackExchangeRedis帮助类
    /// </summary>
    public sealed class RedisHelper2
    {
        /// <summary>
        /// Redis服务器地址
        /// </summary>
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["RedisConnectionString"].ConnectionString;

        /// <summary>
        /// 静态变量锁
        /// </summary>
        private static object _locker = new Object();

        /// <summary>
        /// 静态实例
        /// </summary>
        private static ConnectionMultiplexer _instance = null;
         
        /// <summary>
        /// 数据库
        /// </summary>
        private IDatabase _db;
        public RedisHelper2()
        { 
            _db = GetDatabase();
        }
        /// <summary>
        /// 使用一个静态属性来返回已连接的实例，如下列中所示。这样，一旦 ConnectionMultiplexer 断开连接，便可以初始化新的连接实例。
        /// </summary>
        private static ConnectionMultiplexer Instance
        {
            get
            {
                try
                {
                    if (_instance == null)
                    {
                        lock (_locker)
                        {
                            if (_instance == null || !_instance.IsConnected)
                            {
                                _instance = ConnectionMultiplexer.Connect(ConnectionString);
                                //注册如下事件
                                _instance.ConnectionFailed += MuxerConnectionFailed;
                                _instance.ConnectionRestored += MuxerConnectionRestored;
                                _instance.ErrorMessage += MuxerErrorMessage;
                                _instance.ConfigurationChanged += MuxerConfigurationChanged;
                                _instance.HashSlotMoved += MuxerHashSlotMoved;
                                _instance.InternalError += MuxerInternalError;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    LogUtil.Error(  string.Format("redis初始化异常,连接字符串={0}", ConnectionString), ex);
                }
                return _instance;
            }
        }

        /// <summary>
        /// 获取redis数据库对象
        /// </summary>
        /// <returns></returns>
        private static IDatabase GetDatabase()
        {
            return Instance.GetDatabase();
        }

        /// <summary>
        /// 检查Key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Exists(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return false;
            }
            try
            {
                return GetDatabase().KeyExists(key);
            }
            catch (Exception ex)
            {
                LogUtil.Error( string.Format("检查Key是否存在异常,缓存key={0}", key), ex);
            }
            return false;
        }

        /// <summary>
        /// 设置String类型的缓存对象(如果value是null或者空字符串则设置失败)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="ts">过期时间</param>
        public static bool Set(string key, string value, TimeSpan? ts = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }
            try
            {
                return GetDatabase().StringSet(key, value, ts);
            }
            catch (Exception ex)
            {
                LogUtil.Error( string.Format("设置string类型缓存异常,缓存key={0},缓存值={1}", key, value), ex);
            }
            return false;
        }

        /// <summary>
        /// 根据key获取String类型的缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Get(string key)
        {
            try
            {
                return GetDatabase().StringGet(key);
            }
            catch (Exception ex)
            {
                LogUtil.Error( string.Format("获取string类型缓存异常,缓存key={0}", key), ex);
            }
            return null;
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static bool KeyDelete(string key)
        {
            try
            {
                return GetDatabase().KeyDelete(key);
            }
            catch (Exception ex)
            {
                LogUtil.Error( "删除缓存异常,缓存key={0}" + key, ex);
                return false;
            }
        }

        /// <summary>
        /// 设置Hash类型缓存对象(如果value没有公共属性则不设置缓存)
        ///    会使用反射将object对象所有公共属性作为Hash列存储
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetHash(string key, object value)
        {
            if (null == value)
            {
                return;
            }
            try
            {
                List<HashEntry> list = new List<HashEntry>();
                Type type = value.GetType();
                var propertyArray = type.GetProperties();
                foreach (var property in propertyArray)
                {
                    string propertyName = property.Name;
                    string propertyValue = property.GetValue(value).ToString();
                    list.Add(new HashEntry(propertyName, propertyValue));
                }
                if (list.Count < 1)
                {
                    return;
                }
                IDatabase db = GetDatabase();
                db.HashSet(key, list.ToArray());
            }
            catch (Exception ex)
            {
                LogUtil.Error( string.Format("设置Hash类型缓存异常,缓存key={0},缓存值={1}", key, JsonConvert.SerializeObject(value)), ex);
            }
        }

        /// <summary>
        /// 设置Hash类型缓存对象(用于存储对象)
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">字典,key是列名 value是列的值</param>
        public static void SetHash(string key, Dictionary<string, string> value)
        {
            if (null == value || value.Count < 1)
            {
                return;
            }
            try
            {
                HashEntry[] array = (from item in value select new HashEntry(item.Key, item.Value)).ToArray();
                IDatabase db = GetDatabase();
                db.HashSet(key, array);
            }
            catch (Exception ex)
            {
                LogUtil.Error( string.Format("设置Hash类型缓存异常,缓存key={0},缓存对象值={1}", key, string.Join(",", value)), ex);
            }
        }

        /// <summary>
        /// 根据key和列数组从缓存中拿取数据(如果fieldList为空或者个数小于0返回null)
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="fieldList">列数组</param>
        /// <returns>根据列数组构造一个字典,字典中的列与入参列数组相同,字典中的值是每一列的值</returns>
        public static Dictionary<string, string> GetHash(string key, List<string> fieldList)
        {
            if (null == fieldList || fieldList.Count < 1)
            {
                return null;
            }
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                RedisValue[] array = (from item in fieldList select (RedisValue)item).ToArray();
                IDatabase db = GetDatabase();
                RedisValue[] redisValueArray = db.HashGet(key, array);
                for (int i = 0; i < redisValueArray.Length; i++)
                {
                    string field = fieldList[i];
                    string value = redisValueArray[i];
                    dic.Add(field, value);
                }
                return dic;
            }
            catch (Exception ex)
            {
                LogUtil.Error( string.Format("获取Hash类型缓存异常,缓存key={0},列数组={1}", key, string.Join(",", fieldList)), ex);
            }
            return null;
        }

        /// <summary>
        /// 使用Redis incr 记录某个Key的调用次数
        /// </summary>
        /// <param name="key"></param>
        public static long SaveInvokeCount(string key)
        {
            try
            {
                return GetDatabase().StringIncrement(key);
            }
            catch { return -1; }
        }

        /// <summary>
        /// 配置更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConfigurationChanged(object sender, EndPointEventArgs e)
        {
            LogUtil.Info( "MuxerConfigurationChanged=>e.EndPoint=" + e.EndPoint, null);
        }

        /// <summary>
        /// 发生错误时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerErrorMessage(object sender, RedisErrorEventArgs e)
        {
            LogUtil.Error( "MuxerErrorMessage=>e.EndPoint=" + e.EndPoint + ",e.Message=" + e.Message, null);
        }

        /// <summary>
        /// 重新建立连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            LogUtil.Info( "MuxerConnectionRestored=>e.ConnectionType=" + e.ConnectionType + ",e.EndPoint=" + e.EndPoint + ",e.FailureType=" + e.FailureType, e.Exception);
        }

        /// <summary>
        /// 连接失败
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            LogUtil.Error( "MuxerConnectionFailed=>e.ConnectionType=" + e.ConnectionType + ",e.EndPoint=" + e.EndPoint + ",e.FailureType=" + e.FailureType, e.Exception);
        }

        /// <summary>
        /// 更改集群
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerHashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            LogUtil.Info( "MuxerHashSlotMoved=>" + e.NewEndPoint + ", OldEndPoint" + e.OldEndPoint, null);
        }

        /// <summary>
        /// redis类库错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerInternalError(object sender, InternalErrorEventArgs e)
        {
            LogUtil.Error( "MuxerInternalError", e.Exception);
        }
    }
}
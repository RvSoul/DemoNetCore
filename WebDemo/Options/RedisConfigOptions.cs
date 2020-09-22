using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo.Options
{
    public class RedisConfigOptions
    {
        public  int Db { get; internal set; }

        public string Address { get; internal set; }
    }
}

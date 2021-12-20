using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authority.ServiceImpl
{
  public  class RedisServerConfig
    {
        /// <summary>
        /// 主机名
        /// </summary>
        public string Connection { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string InstanceName { get; set; }
        /// <summary>
        /// 
        /// </summary>
       public int  DefaultDB { get; set; }

    }
}

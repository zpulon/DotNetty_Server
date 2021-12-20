using System;

namespace Domain
{
    public abstract class BaseDomain
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
    }
}

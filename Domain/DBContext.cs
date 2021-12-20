using System;
using System.Collections.Generic;

namespace Domain
{
    internal static class DBContext
    {
        public static Dictionary<Type, object> DataBase { get; } = new Dictionary<Type, object>();
    }
}

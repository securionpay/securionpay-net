using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Internal
{
    public static class ReflectionExtensions
    {
        public static bool IsEnumeration(this Type type )
        {
            return type.IsEnum;
        }

        public static bool IsClass(this Type type)
        {
            return type.IsClass;
        }
    }
}

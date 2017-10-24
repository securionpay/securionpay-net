using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SecurionPay.Internal
{
    public static class ReflectionExtensions
    {
        public static bool IsEnumeration(this Type type )
        {
            return type.GetTypeInfo().IsEnum;
        }

        public static bool IsClass(this Type type)
        {
            return type.GetTypeInfo().IsClass;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Core
{
    public static class Extensions
    {
        public static bool IsSimpleType(this object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var type = value.GetType();

            return
                type.IsValueType ||
                type.IsPrimitive ||
                ((IList) new Type[]
                {
                    typeof(String),
                    typeof(Decimal),
                    typeof(DateTime),
                    typeof(DateTimeOffset),
                    typeof(TimeSpan),
                    typeof(Guid)
                }).Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }
    }
}

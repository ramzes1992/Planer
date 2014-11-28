using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;

namespace Universal
{
    public static class ReflectionHelper
    {
        public static string NameOf<T>(Expression<Func<T>> expr)
        {
            return ((MemberExpression)expr.Body).Member.Name;
        }
    }
}

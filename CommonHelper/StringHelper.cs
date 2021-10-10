using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    public static class StringHelper
    {
        public static T GetDefaultIfStringNull<T>(this string data)
        {
            if (string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data))
            {
                return default(T);
            }
            else {
                return (T)Convert.ChangeType(data,typeof(T));
            }

        }
    }
}

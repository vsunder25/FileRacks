using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace FileRacks.Resources
{
    public class EnumExt
    {
        public static Dictionary<int, string> ToDictionary(Enum myEnum)
        {
            var myEnumType = myEnum.GetType();
            var names = myEnumType.GetFields()
                .Where(m => m.GetCustomAttribute<DisplayAttribute>() != null)
                .Select(e => e.GetCustomAttribute<DisplayAttribute>().Name);
            var values = Enum.GetValues(myEnumType).Cast<int>();
            return names.Zip(values, (n, v) => new KeyValuePair<int, string>(v, n))
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public static string DisplayEnumText(Enum myEnum)
        {
            var myEnumType = myEnum.GetType();
            var names = myEnumType.GetFields()
                .Where(m => m.GetCustomAttribute<DisplayAttribute>() != null && m.Name == Convert.ToString(myEnum))
                .Select(e => e.GetCustomAttribute<DisplayAttribute>().Name);

            return names.First();
        }
    }
}

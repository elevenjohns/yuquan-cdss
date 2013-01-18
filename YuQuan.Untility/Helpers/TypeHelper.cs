using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace YuQuan.Helpers
{
    public static class TypeHelper
    {
        public static object GetPropertyValue(object obj, string name)
        {
            foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
            {
                if (propertyInfo.Name == name)
                {
                    if (propertyInfo.CanRead)
                    {
                        return propertyInfo.GetValue(obj, null);                        
                    }
                }
            }
            return null;
        }

        public static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            var typelist = assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();

            for (int i = 0; i < typelist.Length; i++)
            {
                Console.WriteLine(typelist[i].Name);
            }

            return typelist;
        }


        public static List<string> GetMembers(Type targetType)
        {
            List<string> members = new List<string>();

            if (targetType != null)
            {
                try
                {
                    PropertyInfo[] properties = targetType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                    foreach (PropertyInfo property in properties)
                    {
                        members.Add(string.Format(CultureInfo.InvariantCulture, "{0}   ({1})", property.Name, property.PropertyType));
                    }

                    FieldInfo[] fields = targetType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                    foreach (FieldInfo field in fields)
                    {
                        members.Add(string.Format(CultureInfo.InvariantCulture, "{0}   ({1})", field.Name, field.FieldType));
                    }

                    members.Sort(); //sort all fields and properties as one, but exclude methods which will all be listed at the end

                    List<string> methodMembers = new List<string>();
                    MethodInfo[] methods = targetType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                    foreach (MethodInfo method in methods)
                    {
                        if (!method.Name.StartsWith("get_", StringComparison.Ordinal) && !method.Name.StartsWith("set_", StringComparison.Ordinal))
                        {
                            methodMembers.Add(method.ToString());
                        }
                    }

                    methodMembers.Sort();
                    members.AddRange(methodMembers);
                }
                catch{}
            }

            return members;
        }
    }
}

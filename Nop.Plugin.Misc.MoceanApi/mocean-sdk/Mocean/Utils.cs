using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mocean
{
    public static class Utils
    {
        public static IDictionary<string, string> ConvertClassToDictionary(object className)
        {
            var paramType = className.GetType().GetTypeInfo();
            var dictionary = new Dictionary<string, string>();
            foreach (var property in paramType.GetProperties())
            {
                string jsonPropertyName = null;

                if (property.GetCustomAttributes(typeof(JsonPropertyAttribute), false).Any())
                {
                    jsonPropertyName =
                        ((JsonPropertyAttribute)property.GetCustomAttributes(typeof(JsonPropertyAttribute), false).First())
                            .PropertyName;
                }

                if (null == paramType.GetProperty(property.Name).GetValue(className, null))
                {
                    continue;
                }

                dictionary.Add(
                    string.IsNullOrEmpty(jsonPropertyName) ? property.Name : jsonPropertyName,
                    paramType.GetProperty(property.Name).GetValue(className, null).ToString()
                    );
            }

            return dictionary;
        }
    }
}

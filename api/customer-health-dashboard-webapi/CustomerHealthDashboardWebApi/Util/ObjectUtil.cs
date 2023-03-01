
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CustomerHealthDashboardWebApi.Ext
{
    public static class ObjectUtil
    {

        public static void CopyPropertyValues(this object source, object destination, bool ignoreIfNullOrDefault = false)
        {
            // If any this null throw an exception
            if (source == null || destination == null)
                throw new Exception("Source or/and Destination Objects are null");
            // Getting the Types of the objects
            Type typeDest = destination.GetType();
            Type typeSrc = source.GetType();

            // Iterate the Properties of the source instance and  
            // populate them from their desination counterparts  
            PropertyInfo[] srcProps = typeSrc.GetProperties();
            foreach (PropertyInfo srcProp in srcProps)
            {
                if (!srcProp.CanRead)
                {
                    continue;
                }
                PropertyInfo targetProperty = typeDest.GetProperty(srcProp.Name);
                if (targetProperty == null)
                {
                    continue;
                }
                if (!targetProperty.CanWrite)
                {
                    continue;
                }
                if (targetProperty.GetSetMethod(true) != null && targetProperty.GetSetMethod(true).IsPrivate)
                {
                    continue;
                }
                if ((targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) != 0)
                {
                    continue;
                }
                if (!targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType))
                {
                    continue;
                }
                // Passed all tests, lets set the value
                var sourceValue = srcProp.GetValue(source, null);
                if (ignoreIfNullOrDefault = true)
                {
                    if (sourceValue != null)
                    {
                        if (sourceValue != GetDefaultTypeValue(srcProp.PropertyType))
                        {
                            targetProperty.SetValue(destination, srcProp.GetValue(source, null), null);
                        }
                    }

                }
                else
                {
                    targetProperty.SetValue(destination, sourceValue, null);
                }

            }
        }
        public static T ValueOrDefault<T>(this object obj)
        {
            if (obj == null || CanConvertTo<T>(obj) == false)
            {
                return (T)GetDefaultTypeValue(typeof(T));
            }
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        public static T ValueOrDefault<T>(this string obj)
        {
            if (obj == null || CanConvertTo<T>(obj) == false)
            {
                return (T)GetDefaultTypeValue(typeof(T));
            }
            return (T)Convert.ChangeType(obj, typeof(T));

        }


        public static string ValueOrDefault(this string obj)
        {
            if (obj == null)
            {
                return "";
            }
            return obj;
        }

        public static DateTime ValueOrDefault(this DateTime obj)
        {
            if (obj == null)
            {
                return new DateTime(1900, 01, 01);
            }
            return obj;
        }

        public static int ValueOrDefault(this int? obj)
        {
            if (obj == null)
            {
                return 0;
            }
            return (int)obj;
        }

        public static double ValueOrDefault(this double? obj)
        {
            if (obj == null)
            {
                return 0.0;
            }
            return (double)obj;
        }

        public static DateTime ValueOrDefault(this DateTime? obj)
        {
            if (obj == null)
            {
                return new DateTime(1900, 01, 01);
            }
            return (DateTime)obj;
        }

        public static bool ValueOrDefault(this bool? obj)
        {
            if (obj == null)
            {
                return false;
            }
            return (bool)obj;
        }

        public static bool IsNullOrZero(this int? obj)
        {
            if (obj == null)
            {
                return true;
            }
            return obj == 0;
        }
        public static bool IsNullOrLessThanOrEqualToZero(this int? obj)
        {
            if (obj == null)
            {
                return true;
            }
            return obj <= 0;
        }
        public static bool IsNullOrZero(this double? obj)
        {
            if (obj == null)
            {
                return true;
            }
            return obj == 0;
        }
        public static bool IsNullOrLessThanOrEqualToZero(this double? obj)
        {
            if (obj == null)
            {
                return true;
            }
            return obj <= 0;
        }


        public static void ConvertNullPropertyValuesToDefault(this object sourceObject, List<string> propertiesToIgnore)
        {
            if (sourceObject == null)
            {
                return;
            }


            foreach (var propertyInfo in sourceObject.GetType().GetProperties())
            {

                if (CanSetDefaultTypeValue(propertyInfo.PropertyType))
                {
                    if (!propertiesToIgnore.Contains(propertyInfo.Name, StringComparer.InvariantCultureIgnoreCase))
                    {
                        var currentPropertyValue = propertyInfo.GetValue(sourceObject, null);
                        if (currentPropertyValue == null)
                        {
                            propertyInfo.SetValue(sourceObject, GetDefaultTypeValue(propertyInfo.PropertyType, true), null);
                        }
                        else if (propertyInfo.PropertyType == typeof(DateTime))
                        {
                            var dateValue = (DateTime)currentPropertyValue;
                            var minDateValue = new DateTime(1900, 1, 1);

                            if (dateValue < minDateValue)
                            {
                                //if its a date and the date is somehow less than 1/2/1900 set it to default
                                propertyInfo.SetValue(sourceObject, GetDefaultTypeValue(propertyInfo.PropertyType, true), null);
                            }

                        }
                    }


                }
            }
        }

        public static T ConvertNullPropertyValuesToDefault<T>(this T sourceObject)
        {
            var propertiesToIgnore = new List<string>(); //create an empty list
            sourceObject.ConvertNullPropertyValuesToDefault(propertiesToIgnore);
            return sourceObject;
        }

        public static void ConvertNullPropertyValuesToDefault(this object sourceObject)
        {
            var propertiesToIgnore = new List<string>(); //create an empty list
            sourceObject.ConvertNullPropertyValuesToDefault(propertiesToIgnore);
        }

        public static void ConvertPropertyValuesToDefault(this object sourceObject, List<string> propertiesToIgnore)
        {
            if (sourceObject == null)
            {
                return;
            }


            foreach (var propertyInfo in sourceObject.GetType().GetProperties())
            {

                if (CanSetDefaultTypeValue(propertyInfo.PropertyType))
                {
                    if (!propertiesToIgnore.Contains(propertyInfo.Name, StringComparer.InvariantCultureIgnoreCase))
                    {
                        var currentPropertyValue = propertyInfo.GetValue(sourceObject, null);

                        propertyInfo.SetValue(sourceObject, GetDefaultTypeValue(propertyInfo.PropertyType, true), null);

                        if (propertyInfo.PropertyType == typeof(DateTime))
                        {
                            var dateValue = (DateTime)currentPropertyValue;
                            var minDateValue = new DateTime(1900, 1, 1);

                            if (dateValue < minDateValue)
                            {
                                //if its a date and the date is somehow less than 1/2/1900 set it to default
                                propertyInfo.SetValue(sourceObject, GetDefaultTypeValue(propertyInfo.PropertyType, true), null);
                            }

                        }
                    }


                }
            }
        }

        public static void ConvertPropertyValuesToDefault(this object sourceObject)
        {
            var propertiesToIgnore = new List<string>(); //create an empty list
            sourceObject.ConvertPropertyValuesToDefault(propertiesToIgnore);
        }

        public static void SetPropertyValue(this object sourceObject, string propertyName, object value)
        {
            if (sourceObject == null)
            {
                return;
            }

            var propertyInfo = sourceObject.GetType().GetProperties().FirstOrDefault(x => x.Name.ToLower().Trim() == propertyName.ToLower().Trim());
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(sourceObject, value, null);
            }

        }

        public static bool IsAnEnumerable<T>(this T obj)
        {
            return IsAnEnumerable(obj.GetType());
        }
        public static bool IsAnEnumerable(Type type)
        {
            var bol = false;
            // Check for array type
            if (typeof(IEnumerable).IsAssignableFrom(type) || typeof(IEnumerable<>).IsAssignableFrom(type))
            {
                bol = true;
            }
            return bol;
        }

        public static bool IsSimpleType(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // nullable type, check if the nested type is simple.
                return IsSimpleType(type.GetGenericArguments()[0]);
            }
            return type.IsPrimitive
              || type.IsEnum
              || type.Equals(typeof(string))
              || type.Equals(typeof(decimal));
        }
        public static Type GetPropertyType(this object sourceObject, string propertyName)
        {
            if (sourceObject == null)
            {
                return null;
            }

            var propertyInfo = sourceObject.GetType().GetProperties().FirstOrDefault(x => x.Name.ToLower().Trim() == propertyName.ToLower().Trim());
            if (propertyInfo != null)
            {
                return propertyInfo.PropertyType;
            }

            return null;

        }

        public static bool CanSetDefaultTypeValue(Type type)
        {
            var canSet = false;

            var typeString = type.ToString().ToUpper();
            switch (typeString)
            {

                case "SYSTEM.BOOLEAN":
                    canSet = true;
                    break;
                case "SYSTEM.NULLABLE`1[SYSTEM.BOOLEAN]":
                    canSet = true;
                    break;
                case "SYSTEM.DOUBLE":
                    canSet = true;
                    break;
                case "SYSTEM.NULLABLE`1[SYSTEM.DOUBLE]":
                    canSet = true;
                    break;
                case "SYSTEM.INT32":
                    canSet = true;
                    break;
                case "SYSTEM.NULLABLE`1[SYSTEM.INT32]":
                    canSet = true;
                    break;
                case "SYSTEM.INT64":
                    canSet = true;
                    break;
                case "SYSTEM.NULLABLE`1[SYSTEM.INT64]":
                    canSet = true;
                    break;
                case "SYSTEM.DECIMAL":
                    canSet = true;
                    break;
                case "SYSTEM.NULLABLE`1[SYSTEM.DECIMAL]":
                    canSet = true;
                    break;
                case "SYSTEM.INTEGER":
                    canSet = true;
                    break;
                case "SYSTEM.NULLABLE`1[SYSTEM.INTEGER]":
                    canSet = true;
                    break;
                case "SYSTEM.INT16":
                    canSet = true;
                    break;
                case "SYSTEM.NULLABLE`1[SYSTEM.INT16]":
                    canSet = true;
                    break;
                case "SYSTEM.INT8":
                    canSet = true;
                    break;
                case "SYSTEM.NULLABLE`1[SYSTEM.INT8]":
                    canSet = true;
                    break;
                case "SYSTEM.DATETIME":
                    canSet = true;
                    break;
                case "SYSTEM.NULLABLE`1[SYSTEM.DATETIME]":
                    canSet = true;
                    break;
                case "SYSTEM.DATE":
                    canSet = true;
                    break;
                case "SYSTEM.NULLABLE`1[SYSTEM.DATE]":
                    canSet = true;
                    break;
                case "SYSTEM.STRING":
                    canSet = true;
                    break;
                default:
                    canSet = false;
                    break;
            }

            return canSet;
        }

        public static bool CanConvertTo<T>(object source)
        {

            var bol = false;
            try
            {
                var result = Convert.ChangeType(source, typeof(T));
                bol = true;
            }
            catch
            {
                //ignore
            }

            return bol;
        }

        public static bool CanConvertTo(object source, System.Type type)
        {

            var bol = false;
            try
            {
                var result = Convert.ChangeType(source, type);
                bol = true;
            }
            catch
            {
                //ignore
            }

            return bol;
        }

        public static object GetDefaultTypeValue(Type type, bool useCurrentDayForDates)
        {
            object value;
            

            if (useCurrentDayForDates)
            {
                switch (type.ToString().ToUpper())
                {


                    case "SYSTEM.DATETIME":
                        value = DateTime.Today.AddHours(12);
                        break;
                    case "SYSTEM.DATE":
                        value = DateTime.Today.AddHours(12);
                        break;
                    case "SYSTEM.NULLABLE`1[SYSTEM.DATETIME]":
                        value = DateTime.Today.AddHours(12);
                        break;

                    case "SYSTEM.NULLABLE`1[SYSTEM.DATE]":
                        value = DateTime.Today.AddHours(12);
                        break;
                    default:
                        value = GetDefaultTypeValue(type);
                        break;
                }
            }
            else
            {
                value = GetDefaultTypeValue(type);
            }


            return value;
        }

        public static object GetDefaultTypeValue(Type type)
        {
            object value;
            
            switch (type.ToString().ToUpper())
            {

                case "SYSTEM.BOOLEAN":
                    value = false;
                    break;
                case "SYSTEM.SINGLE":
                    value = (System.Single)0;
                    break;
                case "SYSTEM.DOUBLE":
                    value = (double)0;
                    break;
                case "SYSTEM.INT32":
                    value = 0;
                    break;
                case "SYSTEM.INT64":
                    value = 0;
                    break;
                case "SYSTEM.DECIMAL":
                    value = (decimal)0;
                    break;
                case "SYSTEM.INTEGER":
                    value = 0;
                    break;
                case "SYSTEM.INT16":
                    value = 0;
                    break;
                case "SYSTEM.INT8":
                    value = 0;
                    break;
                case "SYSTEM.DATETIME":
                    value = new DateTime(1900, 1, 1);
                    break;
                case "SYSTEM.DATE":
                    value = new DateTime(1900, 1, 1);
                    break;
                case "SYSTEM.STRING":
                    value = "";
                    break;

                case "SYSTEM.NULLABLE`1[SYSTEM.BOOLEAN]":
                    value = false;
                    break;

                case "SYSTEM.NULLABLE`1[SYSTEM.DOUBLE]":
                    value = (double)0;
                    break;

                case "SYSTEM.NULLABLE`1[SYSTEM.INT32]":
                    value = (Int32)0;
                    break;

                case "SYSTEM.NULLABLE`1[SYSTEM.INT64]":
                    value = (Int64)0;
                    break;

                case "SYSTEM.NULLABLE`1[SYSTEM.DECIMAL]":
                    value = (decimal)0;
                    break;

                case "SYSTEM.NULLABLE`1[SYSTEM.INTEGER]":
                    value = (Int32)0;
                    break;

                case "SYSTEM.NULLABLE`1[SYSTEM.INT16]":
                    value = (Int16)0;
                    break;

                case "SYSTEM.NULLABLE`1[SYSTEM.INT8]":
                    value = 0;
                    break;

                case "SYSTEM.NULLABLE`1[SYSTEM.DATETIME]":
                    value = new DateTime(1900, 1, 1);
                    break;

                case "SYSTEM.NULLABLE`1[SYSTEM.DATE]":
                    value = new DateTime(1900, 1, 1);
                    break;

                default:
                    value = null;
                    break;
            }

            return value;
        }

        public static object GetDefaultTypeValueNullableAware(Type type)
        {
            object value;
            var typeToEval = type;
            if (type.IsNullable() == true)
            {
                typeToEval = Nullable.GetUnderlyingType(type);
            }
            value = GetDefaultTypeValue(typeToEval);

            return value;
        }

        public static bool HasProperty<T>(this T obj, string name)
        {
            if (obj == null)
            {
                return false;
            }
            var properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            var property = properties.FirstOrDefault(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            if (property != null && property.CanWrite)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static object GetPropertyValue<T>(this T obj, string name)
        {
            if (obj == null)
            {
                return null;
            }
            var property = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            if (property != null)
            {
                return property.GetValue(obj, null);
            }
            else
            {
                return null;
            }
        }

        public static void SetPropertyValue<T>(this T obj, string name, object value)
        {
            if (obj == null)
            {
                return;
            }
            var property = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            if (property != null)
            {
                property.SetValue(obj, value);
            }
            else
            {
                return;
            }
        }

        public static object GetPropertyValueOrDefault<T>(this T obj, string name)
        {
            if (obj == null)
            {
                var type = typeof(T).GetProperty(name).PropertyType;
                return GetDefaultTypeValue(type);
            }
            return obj.GetPropertyValue(name);

        }

        public static List<PropertyInfo> GetProperties<T>(this T obj)
        {
            if (obj == null)
            {
                return new List<PropertyInfo>(); 
            }
            return obj.GetType().GetProperties().ToList();
        }

       

        public static Dictionary<string, object> ToDictionary<T>(this T obj)
        {
            var dict = new Dictionary<string, object>();
            foreach (var prop in obj.GetProperties())
            {
                var value = prop.GetValue(obj);
                dict.Add(prop.Name, value);
            }
            return dict;
        }

        public static string ToJson<T>(this T obj)
        {
            var options = GetStandardJsonSerializationOptions();
            return System.Text.Json.JsonSerializer.Serialize(obj, options);
        }




        public static string ToJsonFormatted<T>(this T obj)
        {
            var options = GetStandardJsonSerializationOptions();
            options.WriteIndented = true;
            return System.Text.Json.JsonSerializer.Serialize(obj, options);
        }

        public static T ToObject<T>(this string json)
        {
            var options = GetStandardJsonDeserializationOptions();
            return System.Text.Json.JsonSerializer.Deserialize<T>(json, options);
        }
        public static object ToObject(this string json, Type returnType)
        {
            var options = GetStandardJsonDeserializationOptions();
            return System.Text.Json.JsonSerializer.Deserialize(json, returnType, options);
        }
        public static T FromJson<T>(this string json)
        {
            var options = GetStandardJsonDeserializationOptions();
            return System.Text.Json.JsonSerializer.Deserialize<T>(json, options);
        }
        public static object FromJson(this string json, Type returnType)
        {
            var options = GetStandardJsonDeserializationOptions();
            return System.Text.Json.JsonSerializer.Deserialize(json, returnType, options);
        }


        public static System.Text.Json.JsonSerializerOptions GetStandardJsonSerializationOptions()
        {
            var options = new System.Text.Json.JsonSerializerOptions
            {
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString,
                AllowTrailingCommas = true,
                ReadCommentHandling = System.Text.Json.JsonCommentHandling.Skip,
                PropertyNameCaseInsensitive = true
            };

            return options;
        }

        public static System.Text.Json.JsonSerializerOptions GetStandardJsonDeserializationOptions()
        {
            var options = new System.Text.Json.JsonSerializerOptions
            {
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString,
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = System.Text.Json.JsonCommentHandling.Skip
                 

            };
           
            return options;
        }


        public static bool IsNullable(this object obj)
        {
            Type t = obj.GetType();
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        //public static bool IsNullable(this Type type)
        //{
        //    return type.IsNullableType();
        //}
    }
}

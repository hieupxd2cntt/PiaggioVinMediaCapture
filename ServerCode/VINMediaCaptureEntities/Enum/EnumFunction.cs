using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace VINMediaCaptureEntities.Enum
{
    public static class EnumFunction
    {
        public static List<TEnum> GetListFromEnum<TEnum, TFunc>(TFunc func)
        {
            return (from object e in System.Enum.GetValues(typeof(TEnum)) select (TEnum)e).ToList();
        }
        public static List<System.Enum> GetListFromEnum(System.Enum value)
        {
            return (from object e in System.Enum.GetValues(value.GetType()) select (System.Enum)e).ToList();
        }
        public static string GetDescription(this System.Enum value)
        {
            if (value == null) return string.Empty;
            var fi = value.GetType().GetField(value.ToString());
            if (fi == null) return string.Empty;
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
                return attributes[0].Description;
            return value.ToString();
        }
        public static string GetMapping(this System.Enum value)
        {
            if (value != null)
            {
                var fi = value.GetType().GetField(value.ToString());
                if (fi != null)
                {

                    var attributes = (MappingToAttribute[])fi.GetCustomAttributes(typeof(MappingToAttribute), false);
                    if (attributes.Length > 0)
                        return attributes[0].Mapping;
                    return value.ToString();
                }
                return string.Empty;
            }
            return string.Empty;
        }
        /// <summary>
        /// Lấy thuộc tính Value XML của Enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetValueXml(this System.Enum value)
        {
            if (value != null)
            {
                var fi = value.GetType().GetField(value.ToString());
                if (fi != null)
                {

                    var attributes = (ValueXMLAttribute[])fi.GetCustomAttributes(typeof(ValueXMLAttribute), false);
                    if (attributes.Length > 0)
                        return attributes[0].ValueXML;
                    return value.ToString();
                }
                return string.Empty;
            }
            return string.Empty;
        }
        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(MappingToAttribute)) as MappingToAttribute;
                if (attribute != null)
                {
                    if (attribute.Mapping.ToUpper() == description.ToUpper())
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name.ToUpper() == description.ToUpper())
                        return (T)field.GetValue(null);
                }
            }

            return default(T);
        }
        public static T GetValueFromMapping<T>(string mapping)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(MappingToAttribute)) as MappingToAttribute;
                if (attribute != null)
                {
                    if (attribute.Mapping.ToUpper() == mapping.ToUpper())
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name.ToUpper() == mapping.ToUpper())
                        return ((T)field.GetValue(null));
                }
            }

            return default(T);
        }
    }

}

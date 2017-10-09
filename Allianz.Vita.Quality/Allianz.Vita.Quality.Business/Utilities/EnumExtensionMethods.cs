using System;
using System.ComponentModel;
using System.Reflection;

namespace Allianz.Vita.Quality.Business.Utilities
{
    public static class EnumExtensionMethods
    {

        public static string GetFieldName(this Enum enumValue)
        {
            string enumValueAsString = enumValue.ToString();

            var type = enumValue.GetType();
            FieldInfo fieldInfo = type.GetField(enumValueAsString);
            object[] attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                var attribute = (DescriptionAttribute)attributes[0];
                return attribute.Description;
            }

            return enumValueAsString;
        }

    }
}

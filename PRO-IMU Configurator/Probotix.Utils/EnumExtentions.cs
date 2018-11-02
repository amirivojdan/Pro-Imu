using System;
using System.ComponentModel;
using System.Reflection;
 

namespace Probotix.Utils
{
    public static class EnumExtension
    {
        /// <summary>
        /// Gets the first [Description("")] Attribute of the enum.
        /// value.ToString() if there is none defined
        /// </summary>
        /// <param name="value">the enum value</param>
        /// <returns>the description</returns>
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }

            return value.ToString();
        }
    }
}

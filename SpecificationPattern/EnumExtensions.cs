using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SpecificationPattern
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the value of the EnumDescription attribute.
        /// </summary>
        /// <param name="enumValue">The enum value</param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumValue)
        {
            if (enumValue == null)
                return null;

            string stringValue = enumValue.ToString();
            Type type = enumValue.GetType();
            MemberInfo member = type.GetMember(stringValue).FirstOrDefault();

            if (member == null)
                return stringValue;

            DescriptionAttribute attr = member.GetCustomAttribute<DescriptionAttribute>(false);

            return attr != null ? attr.Description : stringValue;
        }
    }
}

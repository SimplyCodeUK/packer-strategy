// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Helpers
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary> An attributes. </summary>
    public static class Attributes
    {
        /// <summary> An Enum extension method that returns the short name. </summary>
        ///
        /// <param name="value"> The value to act on. </param>
        ///
        /// <returns> The short name. </returns>
        public static string ShortName(this Enum value)
        {
            dynamic displayAttribute = DisplayAttribute(value);

            // return description
            return displayAttribute?.ShortName ?? value.ToString();
        }

        /// <summary> An Enum extension method that returns the name. </summary>
        ///
        /// <param name="value"> The value to act on. </param>
        ///
        /// <returns> The name. </returns>
        public static string Name(this Enum value)
        {
            dynamic displayAttribute = DisplayAttribute(value);

            // return description
            return displayAttribute?.Name ?? value.ToString();
        }

        /// <summary> An Enum extension method that returns the URL name. </summary>
        ///
        /// <param name="value"> The value to act on. </param>
        ///
        /// <returns> The url name. </returns>
        public static string UrlName(this Enum value)
        {
            return ShortName(value).ToLowerInvariant();
        }

        /// <summary> An Enum extension method to get the display attrubute of a value. </summary>
        ///
        /// <param name="value"> The value to act on. </param>
        ///
        /// <returns> The display attribute. </returns>
        private static dynamic DisplayAttribute(this Enum value)
        {
            // get attributes  
            var field = value.GetType().GetField(value.ToString());
            object[] attributes = field.GetCustomAttributes(false);

            // Description is in a hidden Attribute class called DisplayAttribute
            // Not to be confused with DisplayNameAttribute
            dynamic displayAttribute = null;

            if (attributes.Any())
            {
                displayAttribute = attributes.ElementAt(0);
            }

            // return description
            return displayAttribute;
        }
    }
}

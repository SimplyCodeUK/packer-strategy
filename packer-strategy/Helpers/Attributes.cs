﻿//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.Helpers
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>   An attributes. </summary>
    public static class Attributes
    {
        /// <summary>   An Enum extension method that short name. </summary>
        ///
        /// <param name="value">    The value to act on. </param>
        ///
        /// <returns>   A string. </returns>
        public static string ShortName(this Enum value)
        {
            // get attributes  
            FieldInfo field = value.GetType().GetField(value.ToString());
            object[] attributes = field.GetCustomAttributes(false);

            // Description is in a hidden Attribute class called DisplayAttribute
            // Not to be confused with DisplayNameAttribute
            dynamic displayAttribute = null;

            if (attributes.Any())
            {
                displayAttribute = attributes.ElementAt(0);
            }

            // return description
            return displayAttribute?.ShortName ?? value.ToString();
        }

        /// <summary>   An Enum extension method that URL name. </summary>
        ///
        /// <param name="value">    The value to act on. </param>
        ///
        /// <returns>   A string. </returns>
        public static string UrlName(this Enum value)
        {
            return ShortName(value).ToLowerInvariant();
        }
    }
}

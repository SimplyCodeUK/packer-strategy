// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>List box item for a flag attributed enum.</summary>
    ///
    /// <typeparam name="TEnum"> The type of the enum. </typeparam>
    public class ListForFlag<TEnum>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ListForFlag{TEnum}" /> class.
        /// </summary>
        ///
        /// <param name="value"> The enum value. </param>
        public ListForFlag(TEnum value)
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enumerated type");
            }

            this.ListItems = new List<SelectListItem>();

            var enumValues = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

            foreach (var enumValue in enumValues)
            {
                this.ListItems.Add(new SelectListItem
                {
                    Selected = (value.GetHashCode() & enumValue.GetHashCode()) != 0,
                    Value = enumValue.GetHashCode().ToString(),
                    Text = Enum.GetName(typeof(TEnum), enumValue)
                });
            }
        }

        /// <summary> Gets the list items. </summary>
        ///
        /// <value> The list items. </value>
        public List<SelectListItem> ListItems { get; }
    }
}

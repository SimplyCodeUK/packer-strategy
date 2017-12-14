// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Packs.Models
{
    /// <summary> Pack home view model. </summary>
    public class PackViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PackViewModel"/> class.
        /// </summary>
        public PackViewModel()
        {
            this.Data = new PackIt.Pack.Pack();
        }

        /// <summary> Gets or sets the pack data. </summary>
        ///
        /// <value> The pack data. </value>
        public PackIt.Pack.Pack Data { get; set; }
    }
}

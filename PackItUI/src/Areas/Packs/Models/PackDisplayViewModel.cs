// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Packs.Models
{
    using PackIt.Drawing;
    using PackIt.Pack;

    /// <summary> Pack display view model. </summary>
    public class PackDisplayViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PackDisplayViewModel"/> class.
        /// </summary>
        public PackDisplayViewModel()
        {
            this.Pack = new Pack();
            this.Drawing = new Drawing();
        }

        /// <summary> Gets or sets the pack data. </summary>
        ///
        /// <value> The pack data. </value>
        public Pack Pack { get; set; }

        /// <summary> Gets or sets the drawing data. </summary>
        public Drawing Drawing { get; set; }
    }
}

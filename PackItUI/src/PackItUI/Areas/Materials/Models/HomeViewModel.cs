// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Materials.Models
{
    using System.Collections.Generic;
    using PackItUI.Services;

    /// <summary> Materials home view model. </summary>
    public class HomeViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="HomeViewModel"/> class.
        /// </summary>
        ///
        /// <param name="information"> The service information. </param>
        /// <param name="items"> The items. </param>
        public HomeViewModel(ServiceInfo information, IList<PackIt.Material.Material> items)
        {
            this.Information = information;
            this.Items = items;
        }

        /// <summary> Gets the service information. </summary>
        ///
        /// <value> The service information. </value>
        public ServiceInfo Information { get; }

        /// <summary> Gets the items. </summary>
        ///
        /// <value> The items. </value>
        public IList<PackIt.Material.Material> Items { get; }
    }
}

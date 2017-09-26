// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Packs.Models
{
    using PackItUI.Services;

    /// <summary> Packs home view model. </summary>
    public class HomeViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="HomeViewModel"/> class.
        /// </summary>
        ///
        /// <param name="information"> The service information. </param>
        public HomeViewModel(ServiceInfo information)
        {
            this.Information = information;
        }

        /// <summary> Gets the service information. </summary>
        ///
        /// <value> The service information. </value>
        public ServiceInfo Information { get; }
    }
}

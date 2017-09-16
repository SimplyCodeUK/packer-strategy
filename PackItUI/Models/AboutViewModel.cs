// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Models
{
    using System.Collections.Generic;

    /// <summary> About view model. </summary>
    public class AboutViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AboutViewModel"/> class.
        /// </summary>
        public AboutViewModel()
        {
            this.Services = new List<ServiceViewModel>();
        }

        /// <summary> Gets the services. </summary>
        ///
        /// <value> The services. </value>
        public List<ServiceViewModel> Services { get; }
    }
}

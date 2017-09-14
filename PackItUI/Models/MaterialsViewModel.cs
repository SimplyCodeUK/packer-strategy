// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Models
{
    /// <summary> Materials model. </summary>
    public class MaterialsViewModel
    {
        /// <summary>   Gets or sets the version of the service. </summary>
        ///
        /// <value> The verison of the service. </value>
        public string Version { get; set; }

        /// <summary> Gets or sets about information on the service. </summary>
        /// 
        /// <returns> About the service. </returns>
        public string About { get; set; }
    }
}
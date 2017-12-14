// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Services
{
    /// <summary> Service information. </summary>
    public class ServiceInfo
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ServiceInfo" /> class.
        /// </summary>
        public ServiceInfo()
        {
            this.Version = "Unknown";
            this.About = "Service down!";
        }

        /// <summary> Gets or sets the version of the service. </summary>
        ///
        /// <value> The version of the service. </value>
        public string Version { get; set; }

        /// <summary> Gets or sets about information on the service. </summary>
        /// 
        /// <returns> About the service. </returns>
        public string About { get; set; }
    }
}

// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.App.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using PackItUI.Services;

    /// <summary> About view model. </summary>
    public class AboutViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AboutViewModel" /> class.
        /// </summary>
        public AboutViewModel()
        {
            this.Services = new Dictionary<string, ServiceInfo>();
        }

        /// <summary> Gets the services. </summary>
        ///
        /// <value> The services. </value>
        public Dictionary<string, ServiceInfo> Services { get; }

        /// <summary> Create the model with data from the endpoint. </summary>
        ///
        /// <param name="services"> Dictionary of all services. </param>
        ///
        /// <returns> The model data. </returns>
        public async Task Create(Dictionary<string, IServiceHandler> services)
        {
            foreach (KeyValuePair<string, IServiceHandler> service in services)
            {
                ServiceInfo info = await service.Value.InformationAsync();
                this.Services[service.Key] = info;
            }
        }
    }
}

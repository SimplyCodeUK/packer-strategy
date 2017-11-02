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
        /// <param name="endpoints"> The service endpoints. </param>
        ///
        /// <returns> The model data. </returns>
        public async Task Create(ServiceEndpoints endpoints)
        {
            var serviceMap = new Dictionary<string, Service>
            {
                { "Materials", new Service(endpoints.Materials) },
                { "Packs", new Service(endpoints.Packs) },
                { "Plans", new Service(endpoints.Plans) },
                { "Uploads", new Service(endpoints.Uploads) }
            };

            foreach (KeyValuePair<string, Service> service in serviceMap)
            {
                ServiceInfo info = await service.Value.InformationAsync();
                this.Services[service.Key] = info;
            }
        }
    }
}

// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.App.Models
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using PackItUI.Services;

    /// <summary> About view model. </summary>
    public class AboutViewModel
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="AboutViewModel"/> class from being created.
        /// </summary>
        private AboutViewModel()
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
        public static async Task<AboutViewModel> Create(ServiceEndpoints endpoints)
        {
            var model = new AboutViewModel();
            var serviceMap = new Dictionary<string, Service>
            {
                { "Materials", new Materials(endpoints.Materials) },
                { "Packs", new Materials(endpoints.Packs) },
                { "Plans", new Materials(endpoints.Plans) }
            };

            foreach (KeyValuePair<string, Service> service in serviceMap)
            {
                ServiceInfo info = await service.Value.InformationAsync();
                model.Services[service.Key] = info;
            }

            return model;
        }
    }
}

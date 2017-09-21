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


    /// <summary> About view model. </summary>
    public class AboutViewModel
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="AboutViewModel"/> class from being created.
        /// </summary>
        private AboutViewModel()
        {
            this.Services = new Dictionary<string, ServiceViewModel>();
        }

        /// <summary> Create the model with data from the endpoint. </summary>
        ///
        /// <param name="endpoint"> The plans service endpoint. </param>
        public static async Task<AboutViewModel> Create(ServiceEndpoints endpoints)
        {
            var httpClient = new HttpClient();
            var model = new AboutViewModel();
            var endpointMap = new Dictionary<string, string>
            {
                { "Materials", endpoints.Materials },
                { "Packs", endpoints.Packs },
                { "Plans", endpoints.Plans }
            };

            string body;
            foreach (KeyValuePair<string, string> endpoint in endpointMap)
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(endpoint.Value);

                    // Throw an exception if not successful
                    response.EnsureSuccessStatusCode();

                    body = await response.Content.ReadAsStringAsync();
                }
                catch (Exception)
                {
                    body = "{ \"Version\": \"Unknown\", \"About\": \"Service not responding\" }";
                }

                model.Services[endpoint.Key] = JsonConvert.DeserializeObject<ServiceViewModel>(body);
            }

            return model;
        }

        /// <summary> Gets the services. </summary>
        ///
        /// <value> The services. </value>
        public Dictionary<string, ServiceViewModel> Services { get; }
    }
}

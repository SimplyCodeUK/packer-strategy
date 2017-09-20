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

    /// <summary> App settings. </summary>
    public class AppSettings
    {
        /// <summary> Gets or sets the service endpoint. </summary>
        ///
        /// <value> The service endpoint. </value>
        public ServiceEndpoints ServiceEndpoints { get; set; }

        /// <summary> Gets the about view model. </summary>
        ///
        /// <returns> The about view model</returns>
        public async Task<AboutViewModel> GetAboutViewModel()
        {
            var httpClient = new HttpClient();
            var model = new AboutViewModel();
            var endpoints = new Dictionary<string, string>()
            {
                { "Materials", this.ServiceEndpoints.Materials },
                { "Packs", this.ServiceEndpoints.Packs },
                { "Plans", this.ServiceEndpoints.Plans }
            };

            string body;
            foreach (KeyValuePair<string, string> endpoint in endpoints)
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
    }
}

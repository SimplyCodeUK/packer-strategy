// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Models
{
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

        /// <summary> Gets the materials view model. </summary>
        ///
        /// <returns> The materials view model</returns>
        public async Task<MaterialsViewModel> GetMaterialsViewModel()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(this.ServiceEndpoints.Materials);

            // Throw an exception if not successful
            response.EnsureSuccessStatusCode();

            string body = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<MaterialsViewModel>(body);
        }

        /// <summary> Gets the packs view model. </summary>
        ///
        /// <returns> The packs view model</returns>
        public async Task<PacksViewModel> GetPacksViewModel()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(this.ServiceEndpoints.Packs);

            // Throw an exception if not successful
            response.EnsureSuccessStatusCode();

            string body = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<PacksViewModel>(body);
        }

        /// <summary> Gets the plans view model. </summary>
        ///
        /// <returns> The plans view model</returns>
        public async Task<PlansViewModel> GetPlansViewModel()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(this.ServiceEndpoints.Plans);

            // Throw an exception if not successful
            response.EnsureSuccessStatusCode();

            string body = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<PlansViewModel>(body);
        }

        /// <summary> Gets the about view model. </summary>
        ///
        /// <returns> The about view model</returns>
        public async Task<AboutViewModel> GetAboutViewModel()
        {
            HttpClient httpClient = new HttpClient();
            AboutViewModel model = new AboutViewModel();
            string[] endpoints = new string[] { this.ServiceEndpoints.Materials, this.ServiceEndpoints.Packs, this.ServiceEndpoints.Plans };

            foreach (string endpoint in endpoints)
            {
                HttpResponseMessage response = await httpClient.GetAsync(endpoint);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();

                string body = await response.Content.ReadAsStringAsync();

                model.Services.Add(JsonConvert.DeserializeObject<ServiceViewModel>(body));
            }

            return model;
        }
    }
}

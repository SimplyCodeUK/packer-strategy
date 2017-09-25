// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Services
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary> Generic service. </summary>
    public class Service
    {
        /// <summary> The endpoint for the service. </summary>
        protected readonly string Endpoint;

        /// <summary> The http client. </summary>
        protected readonly HttpClient HttpClient;

        /// <summary>
        /// Initialises a new instance of the <see cref="Service"/> class.
        /// </summary>
        ///
        /// <param name="endpoint"> The endpoint for the material service. </param>
        public Service(string endpoint)
        {
            this.Endpoint = endpoint;
            this.HttpClient = new HttpClient();
        }

        /// <summary> Reads asynchronously the service information. </summary>
        ///
        /// <returns> The service information. </returns>
        public async Task<ServiceInfo> InformationAsync()
        {
            try
            {
                HttpResponseMessage response = await this.HttpClient.GetAsync(this.Endpoint);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();

                // Get the content
                string content = await response.Content.ReadAsStringAsync();

                // Create a plan from the content
                return JsonConvert.DeserializeObject<ServiceInfo>(content);
            }
            catch (Exception)
            {
                return new ServiceInfo();
            }
        }
    }
}

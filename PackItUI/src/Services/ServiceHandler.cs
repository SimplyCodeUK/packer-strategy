// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Services
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary> Service information implementation. </summary>
    public static class ServiceHandler
    {
        /// <summary> Reads asynchronously the service information. </summary>
        ///
        /// <param name="httpClient"> The HTTP client. </param>
        /// <param name="endpoint"> The service endpoint. </param>
        ///
        /// <returns> The service information. </returns>
        public static async Task<ServiceInfo> InformationAsync(HttpClient httpClient, string endpoint)
        {
            try
            {
                var response = await httpClient.GetAsync(endpoint);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();

                // Get the content
                var content = await response.Content.ReadAsStringAsync();

                // Create a plan from the content
                return JsonConvert.DeserializeObject<ServiceInfo>(content);
            }
            catch (Exception)
            {
                return new ServiceInfo(endpoint);
            }
        }
    }
}

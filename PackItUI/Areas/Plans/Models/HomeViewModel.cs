// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Plans.Models
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary> Packs home view model. </summary>
    public class HomeViewModel : App.Models.ServiceViewModel
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="HomeViewModel"/> class from being created.
        /// </summary>
        private HomeViewModel()
        {
        }

        /// <summary> Create the model with data from the endpoint. </summary>
        ///
        /// <param name="endpoint"> The plans service endpoint. </param>
        /// 
        /// <returns> The model. </returns>
        public static async Task<HomeViewModel> Create(string endpoint)
        {
            var httpClient = new HttpClient();
            string body;

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(endpoint);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();

                body = await response.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                body = NotFoundService;
            }

            return JsonConvert.DeserializeObject<HomeViewModel>(body);
        }

        /// <summary> Stores the plan.</summary>
        ///
        /// <param name="endpoint"> The plan endpoint. </param>
        /// <param name="data"> The data to store. </param>
        public static async Task Create(string endpoint, PackIt.Plan.Plan data)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json,
                        Encoding.UTF8,
                        "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(endpoint+"Plans/"+ data.PlanId, content);

            // Throw an exception if not successful
            response.EnsureSuccessStatusCode();
        }
    }
}

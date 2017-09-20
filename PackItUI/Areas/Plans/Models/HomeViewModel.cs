// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Plans.Models
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary> Packs home view model. </summary>
    public class HomeViewModel : App.Models.ServiceViewModel
    {
        /// <summary> Prepares the model with data from the endpoint. </summary>
        ///
        /// <param name="endpoint"> The plans service endpoint. </param>
        public static async Task<HomeViewModel> Prepare(string endpoint)
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
    }
}

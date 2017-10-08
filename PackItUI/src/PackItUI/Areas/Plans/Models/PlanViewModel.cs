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

    /// <summary> Plan home view model. </summary>
    public class PlanViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PlanViewModel"/> class.
        /// </summary>
        public PlanViewModel()
        {
            this.Data = new PackIt.Plan.Plan();
        }

        /// <summary> Gets or sets the plan data. </summary>
        ///
        /// <value> The plan data. </value>
        public PackIt.Plan.Plan Data { get; set; }

        /// <summary> Reads asynchronously the model for a material. </summary>
        ///
        /// <param name="endpoint"> The plans service endpoint. </param>
        /// <param name="id"> The identifier of the plan. </param>
        ///
        /// <returns> The model. </returns>
        public static async Task<PlanViewModel> ReadAsync(string endpoint, string id)
        {
            var httpClient = new HttpClient();
            string body;

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(endpoint + "Plans/" + id);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();

                var ret = new PlanViewModel
                {
                    Data = JsonConvert.DeserializeObject<PackIt.Plan.Plan>(body)
                };

                return ret;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

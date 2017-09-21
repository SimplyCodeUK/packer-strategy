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
    public class PlanModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlanModel"/> class.
        /// </summary>
        public PlanModel()
        {
            Data = new PackIt.Plan.Plan();
        }

        /// <summary> Reads asynchronously the model for a plan. </summary>
        ///
        /// <param name="endpoint"> The plans service endpoint. </param>
        /// <param name="id"> The identifier ot the plan. </param>
        ///
        /// <returns> The model. </returns>
        public static async Task<PlanModel> ReadAsync(string endpoint, string id)
        {
            var httpClient = new HttpClient();
            string body;

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(endpoint+"Plans/"+id);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();

                var ret = new PlanModel();
                ret.Data = JsonConvert.DeserializeObject<PackIt.Plan.Plan>(body);

                return ret;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>Gets or sets the data.</summary>
        ///
        /// <value>The data.</value>
        public PackIt.Plan.Plan Data { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PlanModel"/> is editable.
        /// </summary>
        ///
        /// <value><c>true</c> if editable; otherwise, <c>false</c>.</value>
        public bool Editable { get; set; }
    }
}

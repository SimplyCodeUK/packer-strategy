// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Plans.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using PackIt.Helpers.Enums;

    /// <summary> Plan home view model. </summary>
    public class PlanUpdateViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PlanUpdateViewModel"/> class.
        /// </summary>
        public PlanUpdateViewModel()
        {
            this.Data = new Plan();
        }

        /// <summary> Gets or sets the plan data. </summary>
        ///
        /// <value> The plan data. </value>
        public Plan Data { get; set; }

        /// <summary> Reads asynchronously the model for a plan. </summary>
        ///
        /// <param name="endpoint"> The plans service endpoint. </param>
        /// <param name="id"> The identifier of the plan. </param>
        ///
        /// <returns> The model. </returns>
        public static async Task<PlanUpdateViewModel> ReadAsync(string endpoint, string id)
        {
            var httpClient = new HttpClient();
            string body;

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(endpoint + "Plans/" + id);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();

                var ret = new PlanUpdateViewModel
                {
                    Data = JsonConvert.DeserializeObject<Plan>(body)
                };

                return ret;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Data for plan view model
        /// </summary>
        public class Plan
        {
            /// <summary> Gets or sets the Plan identifier. </summary>
            ///
            /// <value> The Plan identifier. </value>
            [Required]
            [Display(Name = "ID", Prompt = "Enter Plan Id")]
            public string PlanId { get; set; }

            /// <summary> Gets or sets the name. </summary>
            ///
            /// <value> The name. </value>
            [Display(Prompt = "Enter Material Name")]
            public string Name { get; set; }

            /// <summary> Gets or sets the notes. </summary>
            ///
            /// <value> The notes. </value>
            [Display(Prompt = "Enter Material Notes")]
            public string Notes { get; set; }
        }
    }
}

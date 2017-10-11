// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Packs.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using PackIt.Helpers.Enums;

    /// <summary> Pack home view model. </summary>
    public class PackUpdateViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PackUpdateViewModel"/> class.
        /// </summary>
        public PackUpdateViewModel()
        {
            this.Data = new Pack();
        }

        /// <summary> Gets or sets the pack data. </summary>
        ///
        /// <value> The pack data. </value>
        public Pack Data { get; set; }

        /// <summary> Reads asynchronously the model for a pack. </summary>
        ///
        /// <param name="endpoint"> The packs service endpoint. </param>
        /// <param name="id"> The identifier of the pack. </param>
        ///
        /// <returns> The model. </returns>
        public static async Task<PackUpdateViewModel> ReadAsync(string endpoint, string id)
        {
            var httpClient = new HttpClient();
            string body;

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(endpoint + "Packs/" + id);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();

                var ret = new PackUpdateViewModel
                {
                    Data = JsonConvert.DeserializeObject<Pack>(body)
                };

                return ret;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Data for pack view model
        /// </summary>
        public class Pack
        {
            /// <summary> Gets or sets the Pack identifier. </summary>
            ///
            /// <value> The Pack identifier. </value>
            [Required]
            [Display(Name = "ID", Prompt = "Enter Pack Id")]
            public string PackId { get; set; }

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

            /// <summary>   Gets or sets the plan code. </summary>
            ///
            /// <value> The plan code. </value>
            public string PlanCode { get; set; }

            /// <summary>   Gets or sets the name of the plan. </summary>
            ///
            /// <value> The name of the plan. </value>
            public string PlanName { get; set; }

            /// <summary>   Gets or sets the type of the material. </summary>
            ///
            /// <value> The type of the material. </value>
            public MaterialType MaterialType { get; set; }

            /// <summary>   Gets or sets the material code. </summary>
            ///
            /// <value> The material code. </value>
            public string MaterialCode { get; set; }

            /// <summary>   Gets or sets the name of the material. </summary>
            ///
            /// <value> The name of the material. </value>
            public string MaterialName { get; set; }

            /// <summary>   Gets or sets the costing premium. </summary>
            ///
            /// <value> The costing premium. </value>
            public double CostingPremium { get; set; }
        }
    }
}

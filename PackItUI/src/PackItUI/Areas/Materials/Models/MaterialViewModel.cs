// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Materials.Models
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary> Material home view model. </summary>
    public class MaterialViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MaterialViewModel"/> class.
        /// </summary>
        public MaterialViewModel()
        {
            this.Data = new PackIt.Material.Material();
        }

        /// <summary> Gets or sets the material data. </summary>
        ///
        /// <value> The material data. </value>
        public PackIt.Material.Material Data { get; set; }

        /// <summary> Reads asynchronously the model for a material. </summary>
        ///
        /// <param name="endpoint"> The materials service endpoint. </param>
        /// <param name="id"> The identifier of the material. </param>
        ///
        /// <returns> The model. </returns>
        public static async Task<MaterialViewModel> ReadAsync(string endpoint, string id)
        {
            var httpClient = new HttpClient();
            string body;

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(endpoint + "Materials/" + id);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();

                var ret = new MaterialViewModel
                {
                    Data = JsonConvert.DeserializeObject<PackIt.Material.Material>(body)
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

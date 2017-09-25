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

    /// <summary> Materials home view model. </summary>
    public class MaterialModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MaterialModel"/> class.
        /// </summary>
        public MaterialModel()
        {
            this.Data = new PackIt.Material.Material();
        }

        /// <summary> Gets or sets the material data. </summary>
        ///
        /// <value> The material data. </value>
        public PackIt.Material.Material Data { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MaterialModel"/> is editable.
        /// </summary>
        ///
        /// <value><c>true</c> if editable; otherwise, <c>false</c>.</value>
        public bool Editable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MaterialModel"/> is to be deleted.
        /// </summary>
        ///
        /// <value><c>true</c> if editable; otherwise, <c>false</c>.</value>
        public bool Deletable { get; set; }

        /// <summary> Reads asynchronously the model for a material. </summary>
        ///
        /// <param name="endpoint"> The materials service endpoint. </param>
        /// <param name="id"> The identifier of the material. </param>
        ///
        /// <returns> The model. </returns>
        public static async Task<MaterialModel> ReadAsync(string endpoint, string id)
        {
            var httpClient = new HttpClient();
            string body;

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(endpoint + "Materials/" + id);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();

                var ret = new MaterialModel
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

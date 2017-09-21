// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Packs.Models
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary> Pack home view model. </summary>
    public class PackModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PackModel"/> class.
        /// </summary>
        public PackModel()
        {
            Data = new PackIt.Pack.Pack();
        }

        /// <summary> Reads asynchronously the model for a pack. </summary>
        ///
        /// <param name="endpoint"> The packs service endpoint. </param>
        /// <param name="id"> The identifier ot the pack. </param>
        ///
        /// <returns> The model. </returns>
        public static async Task<PackModel> ReadAsync(string endpoint, string id)
        {
            var httpClient = new HttpClient();
            string body;

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(endpoint+"Packs/"+id);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();

                var ret = new PackModel();
                ret.Data = JsonConvert.DeserializeObject<PackIt.Pack.Pack>(body);

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
        public PackIt.Pack.Pack Data { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PackModel"/> is editable.
        /// </summary>
        ///
        /// <value><c>true</c> if editable; otherwise, <c>false</c>.</value>
        public bool Editable { get; set; }
    }
}

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
    public class PackViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PackViewModel"/> class.
        /// </summary>
        public PackViewModel()
        {
            this.Data = new PackIt.Pack.Pack();
        }

        /// <summary> Gets or sets the pack data. </summary>
        ///
        /// <value> The pack data. </value>
        public PackIt.Pack.Pack Data { get; set; }

        /// <summary> Reads asynchronously the model for a material. </summary>
        ///
        /// <param name="endpoint"> The packs service endpoint. </param>
        /// <param name="id"> The identifier of the pack. </param>
        ///
        /// <returns> The model. </returns>
        public static async Task<PackViewModel> ReadAsync(string endpoint, string id)
        {
            var httpClient = new HttpClient();
            string body;

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(endpoint + "Packs/" + id);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();

                var ret = new PackViewModel
                {
                    Data = JsonConvert.DeserializeObject<PackIt.Pack.Pack>(body)
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

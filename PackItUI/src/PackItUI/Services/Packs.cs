// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary> Pack home view model. </summary>
    public class Packs : Service
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Packs"/> class.
        /// </summary>
        ///
        /// <param name="endpoint"> The endpoint for the pack service. </param>
        public Packs(string endpoint) : base(endpoint)
        {
        }

        /// <summary> Creates asynchronously a pack. </summary>
        ///
        /// <param name="data"> The pack to save. </param>
        ///
        /// <returns> True if successful. </returns>
        public async Task<bool> CreateAsync(PackIt.Pack.Pack data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");
                HttpResponseMessage response = await this.HttpClient.PostAsync(this.Endpoint + "Packs", content);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary> Reads asynchronously all packs. </summary>
        ///
        /// <returns> The packs. </returns>
        public async Task<IList<PackIt.Pack.Pack>> ReadAsync()
        {
            try
            {
                HttpResponseMessage response = await this.HttpClient.GetAsync(this.Endpoint + "Packs");

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();

                // Get the content
                string content = await response.Content.ReadAsStringAsync();

                // Create a pack from the content
                return JsonConvert.DeserializeObject<List<PackIt.Pack.Pack>>(content);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary> Reads asynchronously a pack. </summary>
        ///
        /// <param name="id"> The identifier of the pack. </param>
        ///
        /// <returns> The pack. </returns>
        public async Task<PackIt.Pack.Pack> ReadAsync(string id)
        {
            try
            {
                HttpResponseMessage response = await this.HttpClient.GetAsync(this.Endpoint + "Packs/" + id);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();

                // Get the content
                string content = await response.Content.ReadAsStringAsync();

                // Create a pack from the content
                return JsonConvert.DeserializeObject<PackIt.Pack.Pack>(content);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary> Updates asynchronously a pack. </summary>
        ///
        /// <param name="id"> The id of the pack. </param>
        /// <param name="data"> The pack to update. </param>
        ///
        /// <returns> True if successful. </returns>
        public async Task<bool> UpdateAsync(string id, PackIt.Pack.Pack data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");
                HttpResponseMessage response = await this.HttpClient.PutAsync(this.Endpoint + "Packs/" + id, content);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary> Deletes asynchronously a pack. </summary>
        ///
        /// <param name="id"> The id of the pack. </param>
        ///
        /// <returns> True if successful. </returns>
        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                HttpResponseMessage response = await this.HttpClient.DeleteAsync(this.Endpoint + "Packs/" + id);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}

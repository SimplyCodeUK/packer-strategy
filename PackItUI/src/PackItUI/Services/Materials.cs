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

    /// <summary> Materials service. </summary>
    public class Materials : Service
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Materials"/> class.
        /// </summary>
        ///
        /// <param name="endpoint"> The endpoint for the material service. </param>
        public Materials(string endpoint) : base(endpoint)
        {
        }

        /// <summary> Creates asynchronously a material. </summary>
        ///
        /// <param name="data"> The material to save. </param>
        ///
        /// <returns> True if successful. </returns>
        public async Task<bool> CreateAsync(PackIt.Material.Material data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");
                HttpResponseMessage response = await this.HttpClient.PostAsync(this.Endpoint + "Materials", content);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary> Reads asynchronously all materials. </summary>
        ///
        /// <returns> The materials. </returns>
        public async Task<List<PackIt.Material.Material>> ReadAsync()
        {
            try
            {
                HttpResponseMessage response = await this.HttpClient.GetAsync(this.Endpoint + "Materials");

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();

                // Get the content
                string content = await response.Content.ReadAsStringAsync();

                // Create a material from the content
                return JsonConvert.DeserializeObject<List<PackIt.Material.Material>>(content);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary> Reads asynchronously a material. </summary>
        ///
        /// <param name="id"> The identifier of the material. </param>
        ///
        /// <returns> The material. </returns>
        public async Task<PackIt.Material.Material> ReadAsync(string id)
        {
            try
            {
                HttpResponseMessage response = await this.HttpClient.GetAsync(this.Endpoint + "Materials/" + id);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();

                // Get the content
                string content = await response.Content.ReadAsStringAsync();

                // Create a material from the content
                return JsonConvert.DeserializeObject<PackIt.Material.Material>(content);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary> Updates asynchronously a material. </summary>
        ///
        /// <param name="id"> The id of the material. </param>
        /// <param name="data"> The material to update. </param>
        ///
        /// <returns> True if successful. </returns>
        public async Task<bool> UpdateAsync(string id, PackIt.Material.Material data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");
                HttpResponseMessage response = await this.HttpClient.PutAsync(this.Endpoint + "Materials/" + id, content);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary> Deletes asynchronously a material. </summary>
        ///
        /// <param name="id"> The id of the material. </param>
        ///
        /// <returns> True if successful. </returns>
        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                HttpResponseMessage response = await this.HttpClient.DeleteAsync(this.Endpoint + "Materials/" + id);

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

// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Services
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary> Plan home view model. </summary>
    public class Plans : Service
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Plans"/> class.
        /// </summary>
        ///
        /// <param name="endpoint"> The endpoint for the plan service. </param>
        public Plans(string endpoint) : base(endpoint)
        {
        }

        /// <summary> Creates asynchronously a plan. </summary>
        ///
        /// <param name="data"> The plan to save. </param>
        ///
        /// <returns> True if successful. </returns>
        public async Task<bool> CreateAsync(PackIt.Plan.Plan data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");
                HttpResponseMessage response = await this.HttpClient.PostAsync(this.Endpoint + "Plans", content);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary> Reads asynchronously a plan. </summary>
        ///
        /// <param name="id"> The identifier of the plan. </param>
        ///
        /// <returns> The plan. </returns>
        public async Task<PackIt.Plan.Plan> ReadAsync(string id)
        {
            try
            {
                HttpResponseMessage response = await this.HttpClient.GetAsync(this.Endpoint + "Plans/" + id);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();

                // Get the content
                string content = await response.Content.ReadAsStringAsync();

                // Create a plan from the content
                return JsonConvert.DeserializeObject<PackIt.Plan.Plan>(content);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary> Updates asynchronously a plan. </summary>
        ///
        /// <param name="id"> The id of the plan. </param>
        /// <param name="data"> The plan to update. </param>
        ///
        /// <returns> True if successful. </returns>
        public async Task<bool> UpdateAsync(string id, PackIt.Plan.Plan data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");
                HttpResponseMessage response = await this.HttpClient.PutAsync(this.Endpoint + "Plans/" + id, content);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary> Deletes asynchronously a plan. </summary>
        ///
        /// <param name="id"> The id of the plan. </param>
        ///
        /// <returns> True if successful. </returns>
        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                HttpResponseMessage response = await this.HttpClient.DeleteAsync(this.Endpoint + "Plans/" + id);

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

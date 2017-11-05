// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Materials.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using PackItUI.Areas.App.Models;
    using PackItUI.Services;

    /// <summary> Material I/O implementation. </summary>
    ///
    /// <seealso cref="T:PackItUI.Areas.Materials.DTO.IMaterialHandler"/>
    public class MaterialHandler : IMaterialHandler
    {
        /// <summary> The HTTP client. </summary>
        private readonly HttpClient httpClient;

        /// <summary> The application endpoint. </summary>
        private readonly string endpoint;

        /// <summary>
        /// Initialises a new instance of the <see cref="MaterialHandler" /> class.
        /// </summary>
        ///
        /// <param name="appSettings"> The application settings. </param>
        /// <param name="timeout"> The timeout for http calls. </param>
        public MaterialHandler(IOptions<AppSettings> appSettings, TimeSpan timeout) : this(appSettings)
        {
            this.httpClient.Timeout = timeout;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="MaterialHandler" /> class.
        /// </summary>
        ///
        /// <param name="appSettings"> The application settings. </param>
        public MaterialHandler(IOptions<AppSettings> appSettings) : this(appSettings,  new HttpClientHandler())
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="MaterialHandler" /> class.
        /// </summary>
        ///
        /// <param name="appSettings"> The application settings. </param>
        /// <param name="messageHandler"> The http message handler. </param>
        public MaterialHandler(IOptions<AppSettings> appSettings, HttpMessageHandler messageHandler)
        {
            this.httpClient = new HttpClient(messageHandler);
            this.endpoint = appSettings.Value.ServiceEndpoints.Materials;
        }

        /// <summary> Reads asynchronously the service information. </summary>
        ///
        /// <returns> The service information. </returns>
        public async Task<ServiceInfo> InformationAsync()
        {
            return await ServiceHandler.InformationAsync(this.httpClient, this.endpoint);
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
                HttpResponseMessage response = await this.httpClient.PostAsync(this.endpoint + "Materials", content);

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
        public async Task<IList<PackIt.Material.Material>> ReadAsync()
        {
            try
            {
                HttpResponseMessage response = await this.httpClient.GetAsync(this.endpoint + "Materials");

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
        /// <returns> The material or null id the material could not be found. </returns>
        public async Task<PackIt.Material.Material> ReadAsync(string id)
        {
            try
            {
                HttpResponseMessage response = await this.httpClient.GetAsync(this.endpoint + "Materials/" + id);

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
                var response = await this.httpClient.PutAsync(this.endpoint + "Materials/" + id, content);

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
                HttpResponseMessage response = await this.httpClient.DeleteAsync(this.endpoint + "Materials/" + id);

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

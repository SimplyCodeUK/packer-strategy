// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Packs.DTO
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

    /// <summary> Pack I/O implementation. </summary>
    ///
    /// <seealso cref="T:PackItUI.Areas.Packs.DTO.IPackHandler"/>
    public class PackHandler : IPackHandler
    {
        /// <summary> The HTTP client. </summary>
        private readonly HttpClient httpClient;

        /// <summary> The application endpoint. </summary>
        private readonly string endpoint;

        /// <summary>
        /// Initialises a new instance of the <see cref="PackHandler" /> class.
        /// </summary>
        ///
        /// <param name="appSettings"> The application settings. </param>
        /// <param name="timeout"> The timeout for http calls. </param>
        public PackHandler(IOptions<AppSettings> appSettings, TimeSpan timeout) : this(appSettings)
        {
            this.httpClient.Timeout = timeout;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="PackHandler" /> class.
        /// </summary>
        ///
        /// <param name="appSettings"> The application settings. </param>
        public PackHandler(IOptions<AppSettings> appSettings) : this(appSettings, new HttpClientHandler())
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="PackHandler" /> class.
        /// </summary>
        ///
        /// <param name="appSettings"> The application settings. </param>
        /// <param name="messageHandler"> The http message handler. </param>
        public PackHandler(IOptions<AppSettings> appSettings, HttpMessageHandler messageHandler)
        {
            this.httpClient = new HttpClient(messageHandler);
            this.endpoint = appSettings.Value.ServiceEndpoints.Plans;
        }

        /// <summary> Reads asynchronously the service information. </summary>
        ///
        /// <returns> The service information. </returns>
        public async Task<ServiceInfo> InformationAsync()
        {
            return await ServiceHandler.InformationAsync(this.httpClient, this.endpoint);
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
                HttpResponseMessage response = await this.httpClient.PostAsync(this.endpoint + "Packs", content);

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
                HttpResponseMessage response = await this.httpClient.GetAsync(this.endpoint + "Packs");

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
                HttpResponseMessage response = await this.httpClient.GetAsync(this.endpoint + "Packs/" + id);

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
                HttpResponseMessage response = await this.httpClient.PutAsync(this.endpoint + "Packs/" + id, content);

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
                HttpResponseMessage response = await this.httpClient.DeleteAsync(this.endpoint + "Packs/" + id);

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

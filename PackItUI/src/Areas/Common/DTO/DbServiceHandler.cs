// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Common.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using PackItUI.Services;

    /// <summary> Interface for a database service. </summary>
    public abstract class DbServiceHandler<TData> : IServiceHandler
    {
        /// <summary> The HTTP client. </summary>
        protected readonly HttpClient httpClient;

        /// <summary> The application endpoint. </summary>
        protected readonly string endpoint;

        /// <summary> The application resource. </summary>
        protected readonly string resource;

        /// <summary>
        /// Initialises a new instance of the <see cref="DbServiceHandler{TData}" /> class.
        /// </summary>
        ///
        /// <param name="messageHandler"> The http message handler. </param>
        /// <param name="endpoint"> The http endpoint. </param>
        /// <param name="resource"> The REST resource. </param>
        protected DbServiceHandler(HttpMessageHandler messageHandler, string endpoint, string resource)
        {
            this.httpClient = new HttpClient(messageHandler);
            this.endpoint = endpoint;
            this.resource = resource;
        }

        /// <summary> Reads asynchronously the service information. </summary>
        ///
        /// <returns> The service information. </returns>
        public async Task<ServiceInfo> InformationAsync()
        {
            return await ServiceHandler.InformationAsync(this.httpClient, this.endpoint);
        }

        /// <summary> Gets or sets the time out for http calls. </summary>
        ///
        /// <value> The time out. </value>
        public TimeSpan TimeOut
        {
            get
            {
                return this.httpClient.Timeout;
            }

            set
            {
                this.httpClient.Timeout = value;
            }
        }

        /// <summary> Creates asynchronously a data entry. </summary>
        ///
        /// <param name="data"> The data entry to save. </param>
        ///
        /// <returns> True if successful. </returns>
        public async Task<bool> CreateAsync(TData data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");
                var response = await this.httpClient.PostAsync(this.endpoint + this.resource, content);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary> Read asynchronously all data entries. </summary>
        ///
        /// <returns> A list of all data entries. </returns>
        public async Task<IList<TData>> ReadAsync()
        {
            try
            {
                var response = await this.httpClient.GetAsync(this.endpoint + this.resource);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();

                // Get the content
                var content = await response.Content.ReadAsStringAsync();

                // Create a material from the content
                return JsonConvert.DeserializeObject<List<TData>>(content);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary> Reads asynchronously a data entry. </summary>
        ///
        /// <param name="id"> The identifier of a data entry. </param>
        ///
        /// <returns> The data entry or null id the plan could not be found. </returns>
        public async Task<TData> ReadAsync(string id)
        {
            try
            {
                var response = await this.httpClient.GetAsync(this.endpoint + this.resource + "/" + id);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();

                // Get the content
                var content = await response.Content.ReadAsStringAsync();

                // Create a pack from the content
                return JsonConvert.DeserializeObject<TData>(content);
            }
            catch (Exception)
            {
                return default(TData);
            }
        }

        /// <summary> Updates asynchronously a a data entry. </summary>
        ///
        /// <param name="id"> The id of the a data entry. </param>
        /// <param name="data"> The a data entry to update. </param>
        ///
        /// <returns> True if successful. </returns>
        public async Task<bool> UpdateAsync(string id, TData data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");
                var response = await this.httpClient.PutAsync(this.endpoint + this.resource + "/" + id, content);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary> Deletes asynchronously a data entry. </summary>
        ///
        /// <param name="id"> The id of the data entry. </param>
        ///
        /// <returns> True if successful. </returns>
        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var response = await this.httpClient.DeleteAsync(this.endpoint + this.resource + "/" + id);

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

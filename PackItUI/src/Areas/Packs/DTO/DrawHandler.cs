// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Packs.DTO
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using PackIt.Models;
    using PackIt.Pack;
    using PackItUI.Services;

    /// <summary> Pack I/O implementation. </summary>
    ///
    /// <seealso cref="T:PackItUI.Services.IServiceHandler"/>
    public class DrawHandler : IServiceHandler
    {
        /// <summary> The HTTP client. </summary>
        protected readonly HttpClient httpClient;

        /// <summary> The application endpoint. </summary>
        protected readonly string endpoint;

        /// <summary> The application resource. </summary>
        protected readonly string resource;

        /// <summary>
        /// Initialises a new instance of the <see cref="DrawHandler" /> class.
        /// </summary>
        ///
        /// <param name="appSettings"> The application settings. </param>
        public DrawHandler(IOptions<AppSettings> appSettings) : this(appSettings, new HttpClientHandler())
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="DrawHandler" /> class.
        /// </summary>
        ///
        /// <param name="appSettings"> The application settings. </param>
        /// <param name="messageHandler"> The http message handler. </param>
        public DrawHandler(IOptions<AppSettings> appSettings, HttpMessageHandler messageHandler)
        {
            this.httpClient = new(messageHandler);
            this.endpoint = appSettings.Value.ServiceEndpoints.Drawings;
            this.resource = "Drawings";
        }

        /// <summary> Gets or sets the time out for http calls. </summary>
        ///
        /// <value> The time out. </value>
        public TimeSpan TimeOut
        {
            get => this.httpClient.Timeout;

            set => this.httpClient.Timeout = value;
        }

        /// <summary> Reads asynchronously the service information. </summary>
        ///
        /// <returns> The service information. </returns>
        public async Task<ServiceInfo> InformationAsync()
        {
            return await ServiceHandler.InformationAsync(this.httpClient, this.endpoint);
        }

        /// <summary> Creates asynchronously a data entry. </summary>
        ///
        /// <param name="data"> The data entry to save. </param>
        ///
        /// <returns> True if successful. </returns>
        public async Task<HttpResponseMessage> CreateAsync(Pack data)
        {
            HttpResponseMessage response;
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");
                response = await this.httpClient.PostAsync(this.endpoint + this.resource, content);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                response = null;
            }

            return response;
        }

        /// <summary> Reads asynchronously a drawing entry. </summary>
        ///
        /// <param name="id"> The identifier of a drawing entry. </param>
        ///
        /// <returns> The drawing entry or null if the plan could not be found. </returns>
        public async Task<PackIt.Drawing.Drawing> ReadAsync(string id)
        {
            try
            {
                var response = await this.httpClient.GetAsync(this.endpoint + this.resource + "/" + id);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();

                // Get the content
                var content = await response.Content.ReadAsStringAsync();

                // Create a pack from the content
                return JsonConvert.DeserializeObject<PackIt.Drawing.Drawing>(content);
            }
            catch (Exception)
            {
                return default;
            }
        }

    }
}

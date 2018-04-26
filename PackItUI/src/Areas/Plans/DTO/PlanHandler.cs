// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Plans.DTO
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

    /// <summary> Plan I/O implementation. </summary>
    ///
    /// <seealso cref="T:PackItUI.Areas.Plans.DTO.IPlanHandler"/>
    public class PlanHandler : IPlanHandler
    {
        /// <summary> The HTTP client. </summary>
        private readonly HttpClient httpClient;

        /// <summary> The application endpoint. </summary>
        private readonly string endpoint;

        /// <summary>
        /// Initialises a new instance of the <see cref="PlanHandler" /> class.
        /// </summary>
        ///
        /// <param name="appSettings"> The application settings. </param>
        public PlanHandler(IOptions<AppSettings> appSettings) : this(appSettings, new HttpClientHandler())
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="PlanHandler" /> class.
        /// </summary>
        ///
        /// <param name="appSettings"> The application settings. </param>
        /// <param name="messageHandler"> The http message handler. </param>
        public PlanHandler(IOptions<AppSettings> appSettings, HttpMessageHandler messageHandler)
        {
            this.httpClient = new HttpClient(messageHandler);
            this.endpoint = appSettings.Value.ServiceEndpoints.Plans;
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

        /// <summary> Reads asynchronously the service information. </summary>
        ///
        /// <returns> The service information. </returns>
        public async Task<ServiceInfo> InformationAsync()
        {
            return await ServiceHandler.InformationAsync(this.httpClient, this.endpoint);
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
                HttpResponseMessage response = await this.httpClient.PostAsync(this.endpoint + "Plans", content);

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary> Reads asynchronously all plans. </summary>
        ///
        /// <returns> The plans. </returns>
        public async Task<IList<PackIt.Plan.Plan>> ReadAsync()
        {
            try
            {
                HttpResponseMessage response = await this.httpClient.GetAsync(this.endpoint + "Plans");

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();

                // Get the content
                string content = await response.Content.ReadAsStringAsync();

                // Create a plan from the content
                return JsonConvert.DeserializeObject<List<PackIt.Plan.Plan>>(content);
            }
            catch (Exception)
            {
                return null;
            }
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
                HttpResponseMessage response = await this.httpClient.GetAsync(this.endpoint + "Plans/" + id);

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
                HttpResponseMessage response = await this.httpClient.PutAsync(this.endpoint + "Plans/" + id, content);

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
                HttpResponseMessage response = await this.httpClient.DeleteAsync(this.endpoint + "Plans/" + id);

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

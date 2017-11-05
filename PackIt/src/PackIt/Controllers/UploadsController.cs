// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using PackIt.Models;

    /// <summary>   A controller for handling materials. </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UploadsController : Controller
    {
        /// <summary> The HTTP client. </summary>
        private readonly HttpClient httpClient;

        /// <summary>
        /// Initialises a new instance of the <see cref="UploadsController" /> class.
        /// </summary>
        ///
        /// <param name="appSettings"> The application settings. </param>
        public UploadsController(IOptions<AppSettings> appSettings) : this(appSettings, new HttpClientHandler())
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="UploadsController" /> class.
        /// </summary>
        ///
        /// <param name="appSettings"> The application settings. </param>
        /// <param name="messageHandler"> The http message handler. </param>
        public UploadsController(IOptions<AppSettings> appSettings, HttpMessageHandler messageHandler)
        {
            this.AppSettings = appSettings.Value;
            this.httpClient = new HttpClient(messageHandler);
        }

        /// <summary> Gets the application settings. </summary>
        ///
        /// <value> The application settings. </value>
        private AppSettings AppSettings { get; }

        /// <summary>   (An Action that handles HTTP POST requests) post this message. </summary>
        ///
        /// <param name="values">    Bulk upload of data to the databases. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        [HttpPost("{type}")]
        public async Task<IActionResult> Post([FromBody] Bulk values)
        {
            var pass = new List<string>();
            var fail = new List<string>();

            if (values == null)
            {
                return this.BadRequest();
            }

            foreach (Plan.Plan item in values.Plans)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(item);
                    var content = new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json");
                    HttpResponseMessage response = await this.httpClient.PostAsync(this.AppSettings.ServiceEndpoints.Plans + "Plans", content);

                    // Throw an exception if not successful
                    response.EnsureSuccessStatusCode();
                    pass.Add(item.PlanId);
                }
                catch (Exception)
                {
                    fail.Add(item.PlanId);
                }
            }

            foreach (Material.Material item in values.Materials)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(item);
                    var content = new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json");
                    HttpResponseMessage response = await this.httpClient.PostAsync(this.AppSettings.ServiceEndpoints.Materials + "Materials", content);

                    // Throw an exception if not successful
                    response.EnsureSuccessStatusCode();
                    pass.Add(item.MaterialId);
                }
                catch (Exception)
                {
                    fail.Add(item.MaterialId);
                }
            }

            foreach (Pack.Pack item in values.Packs)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(item);
                    var content = new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json");
                    HttpResponseMessage response = await this.httpClient.PostAsync(this.AppSettings.ServiceEndpoints.Packs + "Packs", content);

                    // Throw an exception if not successful
                    response.EnsureSuccessStatusCode();
                    pass.Add(item.PackId);
                }
                catch (Exception)
                {
                    fail.Add(item.PackId);
                }
            }

            if (fail.Count != 0)
            {
                return this.StatusCode((int)HttpStatusCode.Conflict, fail);
            }

            return this.StatusCode((int)HttpStatusCode.Created, pass);
        }

        /// <summary>
        /// Object describing data for bulk uploading
        /// </summary>
        public class Bulk
        {
            /// <summary>
            /// Initialises a new instance of the <see cref="Bulk" /> class.
            /// </summary>
            public Bulk()
            {
                this.Plans = new List<Plan.Plan>();
                this.Materials = new List<Material.Material>();
                this.Packs = new List<Pack.Pack>();
            }

            /// <summary>   Gets or sets the collection of Plans of this object. </summary>
            ///
            /// <value> Collection of Plans. </value>
            public IList<Plan.Plan> Plans { get; set; }

            /// <summary>   Gets or sets the collection of Materials of this object. </summary>
            ///
            /// <value> Collection of Materials. </value>
            public IList<Material.Material> Materials { get; set; }

            /// <summary>   Gets or sets the collection of Packs of this object. </summary>
            ///
            /// <value> Collection of Packs. </value>
            public IList<Pack.Pack> Packs { get; set; }
        }
    }
}

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
    using System.Text.Json;
    using System.Threading.Tasks;
    using Asp.Versioning;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using PackItLib.Models;

    /// <summary> A controller for handling materials. </summary>
    /// <remarks>
    /// Initialises a new instance of the <see cref="UploadsController" /> class.
    /// </remarks>
    ///
    /// <param name="logger"> The logger. </param>
    /// <param name="appSettings"> The application settings. </param>
    /// <param name="messageHandler"> The http message handler. </param>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UploadsController(ILogger<UploadsController> logger, IOptions<AppSettings> appSettings, HttpMessageHandler messageHandler) : Controller
    {
        /// <summary> The HTTP client. </summary>
        private readonly HttpClient httpClient = new(messageHandler);

        /// <summary> Gets the application settings. </summary>
        private readonly AppSettings appSettings = appSettings.Value;

        /// <summary> Save data. </summary>
        ///
        /// <param name="item"> The item to save. </param>
        /// <param name="endpoint"> The endpoint yo post to. </param>
        ///
        /// <returns> When async task completed. </returns>
        private async Task Save(object item, string endpoint)
        {
            var json = JsonSerializer.Serialize(item);
            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");
            var response = await this.httpClient.PostAsync(endpoint, content);

            // Throw an exception if not successful
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="UploadsController" /> class.
        /// </summary>
        ///
        /// <param name="logger"> The logger. </param>
        /// <param name="appSettings"> The application settings. </param>
        public UploadsController(ILogger<UploadsController> logger, IOptions<AppSettings> appSettings) : this(logger, appSettings, new HttpClientHandler())
        {
        }

        /// <summary> (An Action that handles HTTP POST requests) Post this message. </summary>
        ///
        /// <param name="values"> Bulk upload of data to the databases. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost("{type}")]
        public async Task<IActionResult> Post([FromBody] Bulk values)
        {
            logger.LogInformation("Post");
            List<string> pass = [];
            List<string> fail = [];

            if (values == null)
            {
                return this.BadRequest();
            }

            foreach (var item in values.Plans)
            {
                try
                {
                    await this.Save(item, this.appSettings.ServiceEndpoints.Plans + "Plans").ConfigureAwait(false);
                    pass.Add(item.PlanId);
                }
                catch (Exception)
                {
                    fail.Add(item.PlanId);
                }
            }

            foreach (var item in values.Materials)
            {
                try
                {
                    await this.Save(item, this.appSettings.ServiceEndpoints.Materials + "Materials").ConfigureAwait(false);
                    pass.Add(item.MaterialId);
                }
                catch (Exception)
                {
                    fail.Add(item.MaterialId);
                }
            }

            foreach (var item in values.Packs)
            {
                try
                {
                    await this.Save(item, this.appSettings.ServiceEndpoints.Packs + "Packs").ConfigureAwait(false);
                    pass.Add(item.PackId);
                }
                catch (Exception)
                {
                    fail.Add(item.PackId);
                }
            }

            Dictionary<string, List<string>> ret = new()
            {
                { "pass", pass },
                { "fail", fail }
            };
            var status = fail.Count != 0 ? HttpStatusCode.Conflict : HttpStatusCode.Created;
            return this.StatusCode((int)status, ret);
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
                this.Plans = new List<PackItLib.Plan.Plan>();
                this.Materials = new List<PackItLib.Material.Material>();
                this.Packs = new List<PackItLib.Pack.Pack>();
            }

            /// <summary> Gets or sets the collection of Plans of this object. </summary>
            ///
            /// <value> Collection of Plans. </value>
            public IList<PackItLib.Plan.Plan> Plans { get; set; }

            /// <summary> Gets or sets the collection of Materials of this object. </summary>
            ///
            /// <value> Collection of Materials. </value>
            public IList<PackItLib.Material.Material> Materials { get; set; }

            /// <summary> Gets or sets the collection of Packs of this object. </summary>
            ///
            /// <value> Collection of Packs. </value>
            public IList<PackItLib.Pack.Pack> Packs { get; set; }
        }
    }
}

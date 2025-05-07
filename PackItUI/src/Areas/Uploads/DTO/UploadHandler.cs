// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Uploads.DTO
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using PackItLib.Models;
    using PackItUI.Services;

    /// <summary> Upload I/O implementation. </summary>
    ///
    /// <seealso cref="T:PackItUI.Areas.Uploads.DTO.IUploadHandler"/>
    public class UploadHandler : IUploadHandler
    {
        /// <summary> The HTTP client. </summary>
        private readonly HttpClient httpClient;

        /// <summary> The application endpoint. </summary>
        private readonly string endpoint;

        /// <summary>
        /// Initialises a new instance of the <see cref="UploadHandler" /> class.
        /// </summary>
        ///
        /// <param name="appSettings"> The application settings. </param>
        public UploadHandler(IOptions<AppSettings> appSettings) : this(appSettings, new HttpClientHandler())
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="UploadHandler" /> class.
        /// </summary>
        ///
        /// <param name="appSettings"> The application settings. </param>
        /// <param name="messageHandler"> The http message handler. </param>
        public UploadHandler(IOptions<AppSettings> appSettings, HttpMessageHandler messageHandler)
        {
            this.httpClient = new(messageHandler);
            this.endpoint = appSettings.Value.ServiceEndpoints.Uploads;
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
    }
}

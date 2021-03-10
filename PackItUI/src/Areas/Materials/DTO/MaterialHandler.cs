// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Materials.DTO
{
    using System.Net.Http;
    using Microsoft.Extensions.Options;
    using PackItUI.Areas.App.Models;
    using PackItUI.Areas.Common.DTO;

    /// <summary> Material I/O implementation. </summary>
    ///
    /// <seealso cref="T:PackItUI.Areas.Common.DTO.DbServiceHandler{TData}"/>
    public class MaterialHandler : DbServiceHandler<PackIt.Material.Material>
    {
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
            : base(messageHandler, appSettings.Value.ServiceEndpoints.Materials, "Materials")
        {
        }
    }
}

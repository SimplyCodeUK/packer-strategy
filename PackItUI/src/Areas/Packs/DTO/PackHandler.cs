// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Packs.DTO
{
    using System.Net.Http;
    using Microsoft.Extensions.Options;
    using PackItUI.Areas.App.Models;
    using PackItUI.Areas.Common.DTO;

    /// <summary> Pack I/O implementation. </summary>
    ///
    /// <seealso cref="T:PackItUI.Areas.Common.DTO.DbServiceHandler{TData}"/>
    public class PackHandler : DbServiceHandler<PackIt.Pack.Pack>
    {
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
            : base(messageHandler, appSettings.Value.ServiceEndpoints.Packs, "Packs")
        {
        }
    }
}

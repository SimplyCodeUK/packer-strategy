// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Plans.DTO
{
    using System.Net.Http;
    using Microsoft.Extensions.Options;
    using PackItLib.Models;
    using PackItUI.Areas.Common.DTO;

    /// <summary> Plan I/O implementation. </summary>
    ///
    /// <seealso cref="T:PackItUI.Areas.Common.DTO.DbServiceHandler{TData}"/>
    public class PlanHandler : DbServiceHandler<PackItLib.Plan.Plan>
    {
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
            : base(messageHandler, appSettings.Value.ServiceEndpoints.Plans, "Plans")
        {
        }
    }
}

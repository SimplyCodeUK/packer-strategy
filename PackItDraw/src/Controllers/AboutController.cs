// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItDraw.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Asp.Versioning;

    /// <summary> The root controller of the service. </summary>
    /// <remarks>
    /// Initialises a new instance of the <see cref="AboutController" /> class.
    /// </remarks>
    ///
    /// <param name="logger"> The logger. </param>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}", Order = 16)]
    public class AboutController(ILogger<AboutController> logger) : Controller
    {
        /// <summary> Get service information. </summary>
        ///
        /// <returns> (An Action that handles HTTP GET requests) The service information. </returns>
        [HttpGet]
        public IActionResult Get()
        {
            logger.LogInformation("Get");
            return this.Ok(new AboutService());
        }

        /// <summary> Data about the service. </summary>
        public class AboutService
        {
            /// <summary>
            /// Initialises a new instance of the <see cref="AboutService"/> class.
            /// </summary>
            public AboutService()
            {
                this.Version = "1";
                this.About = "Drawing controller";
            }

            /// <summary> Gets the version version of the service. </summary>
            ///
            /// <value> The version of the service. </value>
            public string Version { get; }

            /// <summary> Gets information about the service. </summary>
            ///
            /// <value> The about information. </value>
            public string About { get; }
        }
    }
}

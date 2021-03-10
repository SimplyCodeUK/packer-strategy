// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItDraw.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary> The root controller of the service. </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}", Order = 16)]
    public class AboutController : Controller
    {
        /// <summary> The logger. </summary>
        private readonly ILogger<AboutController> logger;

        /// <summary>
        /// Initialises a new instance of the <see cref="AboutController" /> class.
        /// </summary>
        ///
        /// <param name="logger"> The logger. </param>
        public AboutController(ILogger<AboutController> logger)
        {
            this.logger = logger;
        }

        /// <summary> Get service information. </summary>
        ///
        /// <returns> (An Action that handles HTTP GET requests) The service information. </returns>
        [HttpGet]
        public IActionResult Get()
        {
            this.logger.LogInformation("Get");
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

// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary> The root controller of the service. </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}", Order = 16)]
    public class AboutController : Controller
    {
        /// <summary>
        /// (An Action that handles HTTP GET requests) enumerates the items in this collection that
        /// meet given criteria.
        /// </summary>
        ///
        /// <returns> An enumerator that allows foreach to be used to process the matched items. </returns>
        [HttpGet]
        public IActionResult Get()
        {
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
                this.About = "Single controller for Materials, Plans, Packs and Uploads";
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

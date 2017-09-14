// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>   The root contoller of the service. </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/about")]
    public class AboutController : Controller
    {
        public class AboutService
        {
            public string Version { get; }
            public string About { get; }

            public AboutService()
            {
                Version = "1";
                About = "Single controller for Materials, Plans and Packs";
            }
        }

        /// <summary>
        ///     (An Action that handles HTTP GET requests) enumerates the items in this collection that
        ///     meet given criteria.
        /// </summary>
        ///
        /// <returns>   An enumerator that allows foreach to be used to process the matched items. </returns>
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(new AboutService());
        }
    }
}

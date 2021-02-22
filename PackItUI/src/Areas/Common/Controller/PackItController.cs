// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Common.Controller
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary> A controller for handling the Materials Home Page. </summary>
    public class PackItController<TCategoryName> : Controller
    {
        /// <summary> The logger. </summary>
        protected readonly ILogger<TCategoryName> logger;

        public PackItController(ILogger<TCategoryName> logger)
        {
            this.logger = logger;
        }
    }
}

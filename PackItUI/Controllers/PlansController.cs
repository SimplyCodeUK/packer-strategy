﻿// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using PackItUI.Models;

    /// <summary>   A controller for handling the Plans Page. </summary>
    public class PlansController : Controller
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PlansController" /> class.
        /// </summary>
        ///
        /// <param name="appSettings"> The application settings. </param>
        public PlansController(IOptions<AppSettings> appSettings)
        {
            this.AppSettings = appSettings.Value;
        }

        /// <summary> Gets the application settings. </summary>
        ///
        /// <value> The application settings. </value>
        private AppSettings AppSettings { get; }

        /// <summary>   Handle the Plans view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        public IActionResult Plans()
        {
            return this.View(this.AppSettings.GetPlansViewModel().Result);
        }
    }
}

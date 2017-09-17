﻿// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using PackItUI.Models;

    /// <summary>   A controller for handling the Home Page. </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        ///
        /// <param name="appSettings"> The application settings. </param>
        public HomeController(IOptions<AppSettings> appSettings)
        {
            this.AppSettings = appSettings.Value;
        }

        /// <summary> Gets the application settings. </summary>
        ///
        /// <value> The application settings. </value>
        private AppSettings AppSettings { get; }

        /// <summary>   Handle the Index view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>   Handle the About view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        public async Task<IActionResult> About()
        {
            AboutViewModel model = await this.AppSettings.GetAboutViewModel();

            return this.View(model);
        }

        /// <summary>   Handle the Contact view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        public IActionResult Contact()
        {
            this.ViewData["Message"] = "Your contact page.";

            return this.View();
        }

        /// <summary>   Handle exceptions. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

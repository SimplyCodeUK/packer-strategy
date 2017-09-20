﻿// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Plans.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using PackItUI.Areas.App.Models;

    /// <summary>   A controller for handling the Plans Home Page. </summary>
    [Area("Plans")]
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

        /// <summary>   Handle the Plans view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public IActionResult Index()
        {
            return this.View(this.AppSettings.GetPlansViewModel().Result);
        }

        /// <summary> Display details of the specified plan. </summary>
        ///
        /// <param name="id"> The plan identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public IActionResult Details(string id)
        {
            return this.View();
        }

        /// <summary> Display the form to create a new plan. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        /// <summary> Creates a plan from the form collection. </summary>
        ///
        /// <param name="collection"> The form collection. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return this.RedirectToAction(nameof(Index));
            }
            catch
            {
                return this.View();
            }
        }

        /// <summary> Display a form to edit the specified plan. </summary>
        ///
        /// <param name="id"> The plan identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public IActionResult Edit(string id)
        {
            return this.View();
        }

        /// <summary> Save the edited plan. </summary>
        ///
        /// <param name="id"> The plan identifier. </param>
        /// <param name="collection"> The form collection. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return this.RedirectToAction(nameof(Index));
            }
            catch
            {
                return this.View();
            }
        }

        /// <summary> Display a delete form for the specified plan. </summary>
        ///
        /// <param name="id"> The plan identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public IActionResult Delete(string id)
        {
            return this.View();
        }

        /// <summary> Deletes the specified plan. </summary>
        ///
        /// <param name="id"> The plan identifier. </param>
        /// <param name="collection"> The form collection. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return this.RedirectToAction(nameof(Index));
            }
            catch
            {
                return this.View();
            }
        }
    }
}

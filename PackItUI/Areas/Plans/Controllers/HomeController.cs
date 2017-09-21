﻿// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Plans.Controllers
{
    using System.Threading.Tasks;
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
            this.Endpoint = appSettings.Value.ServiceEndpoints.Plans;
        }

        /// <summary> The endpoint. </summary>
        private readonly string Endpoint;

        /// <summary>   Handle the Plans view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public IActionResult Index()
        {
            Task<Models.HomeViewModel> model = Models.HomeViewModel.Create(this.Endpoint);
            return this.View(model.Result);
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

        /// <summary> Strores the plan from the form. </summary>
        ///
        /// <param name="data"> The plan to save. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PackIt.Plan.Plan data)
        {
            if (ModelState.IsValid)
            {
                await Models.HomeViewModel.Create(this.Endpoint, data).ConfigureAwait(false);

                return this.RedirectToAction(nameof(Index));
            }
            else
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

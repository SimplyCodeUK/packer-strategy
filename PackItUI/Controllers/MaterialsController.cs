// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using PackItUI.Models;

    /// <summary>   A controller for handling the Materials Page. </summary>
    public class MaterialsController : Controller
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MaterialsController" /> class.
        /// </summary>
        ///
        /// <param name="appSettings"> The application settings. </param>
        public MaterialsController(IOptions<AppSettings> appSettings)
        {
            this.AppSettings = appSettings.Value;
        }

        /// <summary> Gets the application settings. </summary>
        ///
        /// <value> The application settings. </value>
        private AppSettings AppSettings { get; }

        /// <summary>   Handle the Materials view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        public IActionResult Index()
        {
            return this.View(this.AppSettings.GetMaterialsViewModel().Result);
        }

        /// <summary> Display details of the specified material. </summary>
        ///
        /// <param name="id"> The material identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        public IActionResult Details(string id)
        {
            return this.View();
        }

        /// <summary> Display the form to create a new material. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        public IActionResult Create()
        {
            return this.View();
        }

        /// <summary> Creates a material from the form collection. </summary>
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

        /// <summary> Edits the specified material. </summary>
        ///
        /// <param name="id"> The material identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        public IActionResult Edit(string id)
        {
            return this.View();
        }

        /// <summary> Save the edited material. </summary>
        ///
        /// <param name="id"> The material identifier. </param>
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

        /// <summary> Display a delete form for the specified material. </summary>
        /// 
        /// <param name="id"> The plan identifier. </param>
        /// 
        /// <returns> An IActionResult. </returns>
        public IActionResult Delete(string id)
        {
            return this.View();
        }

        /// <summary> Deletes the specified material. </summary>
        ///
        /// <param name="id"> The material identifier. </param>
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

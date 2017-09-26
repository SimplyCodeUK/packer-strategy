// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Packs.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using PackItUI.Areas.App.Models;

    /// <summary>   A controller for handling the Packs Home Page. </summary>
    [Area("Packs")]
    public class HomeController : Controller
    {
        /// <summary> The packs service. </summary>
        private readonly Services.Packs service;

        /// <summary>
        /// Initialises a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        ///
        /// <param name="appSettings"> The application settings. </param>
        public HomeController(IOptions<AppSettings> appSettings)
        {
            this.service = new Services.Packs(appSettings.Value.ServiceEndpoints.Packs);
        }

        /// <summary>   Handle the Packs view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new Models.HomeViewModel(await this.service.InformationAsync(), await this.service.ReadAsync());
            return this.View(model);
        }

        /// <summary> Display details of the specified pack. </summary>
        ///
        /// <param name="id"> The pack identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var model = new Models.PackModel
            {
                Data = await this.service.ReadAsync(id)
            };

            return this.View(model);
        }

        /// <summary> Display the form to create a new pack. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public IActionResult Create()
        {
            var model = new Models.PackModel();
            return this.View(model);
        }

        /// <summary> Stores a pack from the form. </summary>
        ///
        /// <param name="model"> The pack to save. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.PackModel model)
        {
            if (ModelState.IsValid && await this.service.CreateAsync(model.Data))
            {
                return this.RedirectToAction(nameof(this.Index));
            }
            else
            {
                return this.View(model);
            }
        }

        /// <summary> Display a form to edit the specified pack. </summary>
        ///
        /// <param name="id"> The pack identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = new Models.PackModel
            {
                Data = await this.service.ReadAsync(id),
                Editable = true
            };

            return this.View(model);
        }

        /// <summary> Save the edited pack asynchronously. </summary>
        ///
        /// <param name="id"> The pack identifier. </param>
        /// <param name="model"> The pack to update. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Models.PackModel model)
        {
            if (await this.service.UpdateAsync(id, model.Data))
            {
                return this.RedirectToAction(nameof(this.Index));
            }
            else
            {
                return this.View(model);
            }
        }

        /// <summary> Display a delete form for the specified pack. </summary>
        ///
        /// <param name="id"> The pack identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var model = new Models.PackModel
            {
                Data = await this.service.ReadAsync(id),
                Deletable = true
            };

            return this.View(model);
        }

        /// <summary> Deletes the specified pack. </summary>
        ///
        /// <param name="id"> The pack identifier. </param>
        /// <param name="model"> The pack to delete. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, Models.PackModel model)
        {
            if (await this.service.DeleteAsync(id))
            {
                return this.RedirectToAction(nameof(this.Index));
            }
            else
            {
                return this.View(model);
            }
        }
    }
}

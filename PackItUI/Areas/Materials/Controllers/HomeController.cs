// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Materials.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using PackItUI.Areas.App.Models;

    /// <summary>   A controller for handling the Materials Home Page. </summary>
    [Area("Materials")]
    public class HomeController : Controller
    {
        /// <summary> The materials service. </summary>
        private readonly Services.Materials service;

        /// <summary>
        /// Initialises a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        ///
        /// <param name="appSettings"> The application settings. </param>
        public HomeController(IOptions<AppSettings> appSettings)
        {
            this.service = new Services.Materials(appSettings.Value.ServiceEndpoints.Materials);
        }

        /// <summary>   Handle the Materials view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new Models.HomeViewModel(await this.service.InformationAsync());
            return this.View(model);
        }

        /// <summary> Display details of the specified material. </summary>
        ///
        /// <param name="id"> The material identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var model = new Models.MaterialModel
            {
                Data = await this.service.ReadAsync(id)
            };

            return this.View(model);
        }

        /// <summary> Display the form to create a new material. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public IActionResult Create()
        {
            var model = new Models.MaterialModel();
            return this.View(model);
        }

        /// <summary> Stores the material from the form. </summary>
        ///
        /// <param name="model"> The material to save. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.MaterialModel model)
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

        /// <summary> Display a form to edit the specified material. </summary>
        ///
        /// <param name="id"> The material identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = new Models.MaterialModel
            {
                Data = await this.service.ReadAsync(id),
                Editable = true
            };

            return this.View(model);
        }

        /// <summary> Save the edited material asynchronously. </summary>
        ///
        /// <param name="id"> The material identifier. </param>
        /// <param name="model"> The material to update. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Models.MaterialModel model)
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

        /// <summary> Display a delete form for the specified material. </summary>
        ///
        /// <param name="id"> The material identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var model = new Models.MaterialModel
            {
                Data = await this.service.ReadAsync(id),
                Deletable = true
            };

            return this.View(model);
        }

        /// <summary> Deletes the specified material. </summary>
        ///
        /// <param name="id"> The material identifier. </param>
        /// <param name="model"> The material to delete. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, Models.MaterialModel model)
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

// <copyright company="Simply Code Ltd.">
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
        /// <summary> The plans service. </summary>
        private readonly Services.Plans service;

        /// <summary>
        /// Initialises a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        ///
        /// <param name="appSettings"> The application settings. </param>
        public HomeController(IOptions<AppSettings> appSettings)
        {
            this.service = new Services.Plans(appSettings.Value.ServiceEndpoints.Plans);
        }

        /// <summary>   Handle the Plans view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Models.HomeViewModel model = new Models.HomeViewModel(await this.service.InformationAsync());
            return this.View(model);
        }

        /// <summary> Display details of the specified plan. </summary>
        ///
        /// <param name="id"> The plan identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            Models.PlanModel model = new Models.PlanModel
            {
                Data = await this.service.ReadAsync(id)
            };

            return this.View(model);
        }

        /// <summary> Display the form to create a new plan. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public IActionResult Create()
        {
            Models.PlanModel model = new Models.PlanModel();
            return this.View(model);
        }

        /// <summary> Stores the plan from the form. </summary>
        ///
        /// <param name="model"> The plan to save. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.PlanModel model)
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

        /// <summary> Display a form to edit the specified plan. </summary>
        ///
        /// <param name="id"> The plan identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            Models.PlanModel model = new Models.PlanModel
            {
                Data = await this.service.ReadAsync(id),
                Editable = true
            };

            return this.View(model);
        }

        /// <summary> Save the edited plan asynchronously. </summary>
        ///
        /// <param name="id"> The plan identifier. </param>
        /// <param name="model"> The plan to update. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Models.PlanModel model)
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

        /// <summary> Display a delete form for the specified plan. </summary>
        ///
        /// <param name="id"> The plan identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            Models.PlanModel model = new Models.PlanModel
            {
                Data = await this.service.ReadAsync(id),
                Deletable = true
            };

            return this.View(model);
        }

        /// <summary> Deletes the specified plan. </summary>
        ///
        /// <param name="id"> The plan identifier. </param>
        /// <param name="model"> The plan to delete. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, Models.PlanModel model)
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

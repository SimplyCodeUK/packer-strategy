// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Plans.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using PackItUI.Areas.Common.Controller;
    using PackItUI.Areas.Common.DTO;
    using PackItUI.Areas.Plans.Models;

    /// <summary> A controller for handling the Plans Home Page. </summary>
    ///
    /// <seealso cref="T:PackItUI.Areas.Common.Controller.PackItController{TCategoryName, TData, TModel, TEditViewModel}"/>
    [Area("Plans")]
    public class HomeController : PackItController<HomeController, PackIt.Plan.Plan, PlanEditViewModel.Plan, PlanEditViewModel>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        ///
        /// <param name="logger"> The logger. </param>
        /// <param name="handler"> The I/O handler. </param>
        public HomeController(ILogger<HomeController> logger, DbServiceHandler<PackIt.Plan.Plan> handler)
            : base(logger, handler)
        {
        }

        /// <summary> Handle the Plans view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public override async Task<IActionResult> Index()
        {
            this.logger.LogInformation("Index");
            var model = new HomeViewModel(await this.handler.InformationAsync(), await this.handler.ReadAsync());
            return this.View("Index", model);
        }

        /// <summary> Stores the plan from the form. </summary>
        ///
        /// <param name="model"> The plan to save. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Create(PlanEditViewModel model)
        {
            var data = new PackIt.Plan.Plan();
            this.logger.LogInformation("Create Plan id {0}", data.PlanId);

            data = this.mapper.Map(model.Data, data);
            if (this.ModelState.IsValid && await this.handler.CreateAsync(data))
            {
                return this.RedirectToAction(nameof(this.Index));
            }
            else
            {
                return this.View("Create", model);
            }
        }

        /// <summary> Display a form to update the specified plan. </summary>
        ///
        /// <param name="id"> The plan identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            this.logger.LogInformation("Update id {0}", id);
            var model = new PlanEditViewModel
            {
                Data = this.mapper.Map<PlanEditViewModel.Plan>(await this.handler.ReadAsync(id))
            };

            return this.View("Update", model);
        }

        /// <summary> Save the updated plan asynchronously. </summary>
        ///
        /// <param name="id"> The plan identifier. </param>
        /// <param name="model"> The plan data to update. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string id, PlanEditViewModel model)
        {
            var data = await this.handler.ReadAsync(id);
            this.logger.LogInformation("Update id {0} Plan id {1}", id, model.Data.PlanId);

            data = this.mapper.Map(model.Data, data);
            if (await this.handler.UpdateAsync(id, data))
            {
                return this.RedirectToAction(nameof(this.Index));
            }
            else
            {
                return this.View("Update", model);
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
            this.logger.LogInformation("Delete id {0}", id);
            var model = new PlanEditViewModel
            {
                Data = this.mapper.Map<PlanEditViewModel.Plan>(await this.handler.ReadAsync(id))
            };

            return this.View("Delete", model);
        }
    }
}

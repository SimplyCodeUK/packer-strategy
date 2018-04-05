// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Plans.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using PackItUI.Areas.Plans.DTO;
    using PackItUI.Areas.Plans.Models;

    /// <summary> A controller for handling the Plans Home Page. </summary>
    [Area("Plans")]
    public class HomeController : Controller
    {
        /// <summary> The plans handler. </summary>
        private readonly IPlanHandler handler;

        /// <summary> The mapper to view model. </summary>
        private readonly IMapper mapper = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<PackIt.Plan.Plan, PlanEditViewModel.Plan>();
                cfg.CreateMap<PlanEditViewModel.Plan, PackIt.Plan.Plan>();
            }).CreateMapper();

        /// <summary>
        /// Initialises a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        ///
        /// <param name="handler"> The I/O handler. </param>
        public HomeController(IPlanHandler handler)
        {
            this.handler = handler;
        }

        /// <summary> Handle the Plans view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel(await this.handler.InformationAsync(), await this.handler.ReadAsync());
            return this.View("Index", model);
        }

        /// <summary> Display the form to create a new plan. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public IActionResult Create()
        {
            var model = new PlanEditViewModel();
            return this.View("Create", model);
        }

        /// <summary> Stores the plan from the form. </summary>
        ///
        /// <param name="model"> The plan to save. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlanEditViewModel model)
        {
            PackIt.Plan.Plan data = new PackIt.Plan.Plan();

            data = this.mapper.Map(model.Data, data);
            if (ModelState.IsValid && await this.handler.CreateAsync(data))
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
            PackIt.Plan.Plan data = await this.handler.ReadAsync(id);

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
            var model = new PlanEditViewModel
            {
                Data = this.mapper.Map<PlanEditViewModel.Plan>(await this.handler.ReadAsync(id))
            };

            return this.View("Delete", model);
        }

        /// <summary> Deletes the specified plan. </summary>
        ///
        /// <param name="id"> The plan identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DoDelete(string id)
        {
            await this.handler.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

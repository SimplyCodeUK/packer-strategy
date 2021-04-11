// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Packs.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using PackItUI.Areas.Common.Controller;
    using PackItUI.Areas.Common.DTO;
    using PackItUI.Areas.Packs.Models;

    /// <summary> A controller for handling the Packs Home Page. </summary>
    ///
    /// <seealso cref="T:PackItUI.Areas.Common.Controller.PackItController{TCategoryName, TData, TModel, TEditViewModel}"/>
    [Area("Packs")]
    public class HomeController : PackItController<HomeController, PackIt.Pack.Pack, PackEditViewModel.Pack, PackEditViewModel>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        ///
        /// <param name="logger"> The logger. </param>
        /// <param name="handler"> The I/O handler. </param>
        public HomeController(ILogger<HomeController> logger, DbServiceHandler<PackIt.Pack.Pack> handler)
            : base(logger, handler)
        {
        }

        /// <summary> Handle the Packs view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public override async Task<IActionResult> Index()
        {
            this.logger.LogInformation("Index");
            var model = new HomeViewModel(await this.handler.InformationAsync(), await this.handler.ReadAsync());
            return this.View("Index", model);
        }

        /// <summary> Stores the pack from the form. </summary>
        ///
        /// <param name="model"> The pack to save. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Create(PackEditViewModel model)
        {
            var data = new PackIt.Pack.Pack();
            this.logger.LogInformation("Create Pack id {0}", data.PackId);

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

        /// <summary> Display a form to update the specified pack. </summary>
        ///
        /// <param name="id"> The pack identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            this.logger.LogInformation("Update id {0}", id);
            var model = new PackEditViewModel
            {
                Data = this.mapper.Map<PackEditViewModel.Pack>(await this.handler.ReadAsync(id))
            };

            return this.View("Update", model);
        }

        /// <summary> Save the updated pack asynchronously. </summary>
        ///
        /// <param name="id"> The pack identifier. </param>
        /// <param name="model"> The pack data to update. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string id, PackEditViewModel model)
        {
            var data = await this.handler.ReadAsync(id);
            this.logger.LogInformation("Update id {0} Pack id {1}", id, model.Data.PackId);

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

        /// <summary> Display a delete form for the specified pack. </summary>
        ///
        /// <param name="id"> The pack identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            this.logger.LogInformation("Delete id {0}", id);
            var model = new PackEditViewModel
            {
                Data = this.mapper.Map<PackEditViewModel.Pack>(await this.handler.ReadAsync(id))
            };

            return this.View("Delete", model);
        }

        /// <summary> Display the specified pack. </summary>
        ///
        /// <param name="id"> The pack identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Display(string id)
        {
            this.logger.LogInformation("Display id {0}", id);
            var model = await this.handler.ReadAsync(id);

            return this.View("Display", model);
        }

        /// <summary> Save the updated pack asynchronously. </summary>
        ///
        /// <param name="id"> The pack identifier. </param>
        /// <param name="model"> The pack data to update. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Display(string id, PackEditViewModel model)
        {
            var data = await this.handler.ReadAsync(id);

            if (data == null)
            {
                this.logger.LogError("Display id {0}", id);
            }
            else
            {
                this.logger.LogInformation("Display id {0} Pack id {1}", id, data.PackId);

                data = this.mapper.Map(model.Data, data);
                if (await this.handler.UpdateAsync(id, data))
                {
                    return this.RedirectToAction(nameof(this.Index));
                }
            }

            return this.View("Update", model);
        }

        /// <summary> Get a costing row. Used when adding a new row to the html. </summary>
        ///
        /// <param name="body"> The body of the POST command. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        public IActionResult CostingRow([FromBody] Newtonsoft.Json.Linq.JObject body)
        {
            var index = body["index"];
            this.logger.LogInformation("CostingRow {0}", index);

            ViewBag.crud = Helpers.Crud.Create;
            ViewData.TemplateInfo.HtmlFieldPrefix = string.Format("Data.Costings[{0}]", index);

            var mod = new PackIt.Pack.Costing();
            var ret = this.PartialView("EditorTemplates/Costing", mod);

            return ret;
        }
    }
}

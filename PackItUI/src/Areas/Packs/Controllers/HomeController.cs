// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Packs.Controllers
{
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using PackItUI.Areas.Common.Controller;
    using PackItUI.Areas.Common.DTO;
    using PackItUI.Areas.Packs.DTO;
    using PackItUI.Areas.Packs.Models;

    /// <summary> A controller for handling the Packs Home Page. </summary>
    ///
    /// <seealso cref="T:PackItUI.Areas.Common.Controller.PackItController{TCategoryName, TData, TModel, TEditViewModel}"/>
    [Area("Packs")]
    public class HomeController : PackItController<HomeController, PackItLib.Pack.Pack, PackEditViewModel.Pack, PackEditViewModel>
    {
        private readonly DrawHandler drawHandler;

        /// <summary>
        /// Initialises a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        ///
        /// <param name="logger"> The logger. </param>
        /// <param name="handler"> The I/O handler. </param>
        /// <param name="drawHandler"> The drawing handler. </param>
        public HomeController(ILogger<HomeController> logger, DbServiceHandler<PackItLib.Pack.Pack> handler, DrawHandler drawHandler)
            : base(logger, handler)
        {
            this.drawHandler = drawHandler;
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
            var data = new PackItLib.Pack.Pack();
            this.logger.LogInformation("Create Pack id {PackId}", data.PackId);

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

        /// <summary> Display a form to update the specified pack. </summary>
        ///
        /// <param name="id"> The pack identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            this.logger.LogInformation("Update id {Id}", id.Replace(Environment.NewLine, "").Replace("\n", "").Replace("\r", ""));
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
            IActionResult ret;
            if (!ModelState.IsValid)
            {
                ret = View(model);
            }
            else
            {
                var data = await this.handler.ReadAsync(id);
                this.logger.LogInformation("Update id {Id} Pack id {PackId}", id.Replace(Environment.NewLine, "").Replace("\n", "").Replace("\r", ""), model.Data.PackId);

                data = this.mapper.Map(model.Data, data);
                if (await this.handler.UpdateAsync(id, data))
                {
                    ret = this.RedirectToAction(nameof(this.Index));
                }
                else
                {
                    ret = this.View("Update", model);
                }
            }
            return ret;
        }

        /// <summary> Display a delete form for the specified pack. </summary>
        ///
        /// <param name="id"> The pack identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            this.logger.LogInformation("Delete id {Id}", id.Replace(Environment.NewLine, "").Replace("\n", "").Replace("\r", ""));
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
            this.logger.LogInformation("Display id {Id}", id.Replace(Environment.NewLine, "").Replace("\n", "").Replace("\r", ""));
            PackDisplayViewModel model = new()
            {
                Pack = await this.handler.ReadAsync(id)
            };

            var created = await this.drawHandler.CreateAsync(model.Pack);
            if (created.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var content = created.Content.ReadAsStringAsync();
                content.Wait();
                JsonDocument cont = JsonDocument.Parse(content.Result);
                model.Drawing = await this.drawHandler.ReadAsync(cont.RootElement.GetProperty("DrawingId").ToString());
            }
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
            IActionResult ret;
            var data = await this.handler.ReadAsync(id);

            if (data == null)
            {
                this.logger.LogError("Display id {0}", id);
                ret = this.View("Update", model);
            }
            else
            {
                this.logger.LogInformation("Display id {Id} Pack id {PackId}", id.Replace(Environment.NewLine, "").Replace("\n", "").Replace("\r", ""), data.PackId);

                if (!ModelState.IsValid)
                {
                    ret = View(model);
                }
                else
                {
                    data = this.mapper.Map(model.Data, data);
                    if (await this.handler.UpdateAsync(id, data))
                    {
                        ret = this.RedirectToAction(nameof(this.Index));
                    }
                    else
                    {
                        ret = this.View("Update", model);
                    }
                }
            }

            return ret;
        }

        /// <summary> Get a costing row. Used when adding a new row to the html. </summary>
        ///
        /// <param name="body"> The body of the POST command. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CostingRow([FromBody] JsonDocument body)
        {
            IActionResult ret;
            if (!ModelState.IsValid)
            {
                ret = View(body);
            }
            else
            {
                var index = body.RootElement.GetProperty("index").ToString().Replace(Environment.NewLine, "").Replace("\n", "").Replace("\r", "");
                this.logger.LogInformation("CostingRow {Index}", index);

                this.ViewBag.crud = Helpers.Crud.Create;
                this.ViewData.TemplateInfo.HtmlFieldPrefix = string.Format("Data.Costings[{0}]", index);

                var mod = new PackItLib.Pack.Costing();
                ret = this.PartialView("EditorTemplates/Costing", mod);
            }

            return ret;
        }
    }
}

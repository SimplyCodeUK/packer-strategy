// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Materials.Controllers
{
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using PackItLib.Helpers.Enums;
    using PackItUI.Areas.Common.Controller;
    using PackItUI.Areas.Common.DTO;
    using PackItUI.Areas.Materials.Models;
    using PackItUI.Helpers;

    /// <summary> A controller for handling the Materials Home Page. </summary>
    ///
    /// <seealso cref="T:PackItUI.Areas.Common.Controller.PackItController{TCategoryName, TData, TModel, TEditViewModel}"/>
    [Area("Materials")]
    public class HomeController : PackItController<HomeController, PackItLib.Material.Material, MaterialEditViewModel.Material, MaterialEditViewModel>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        ///
        /// <param name="logger"> The logger. </param>
        /// <param name="handler"> The I/O handler. </param>
        public HomeController(ILogger<HomeController> logger, DbServiceHandler<PackItLib.Material.Material> handler)
            : base(logger, handler)
        {
        }

        /// <summary> Handle the Materials view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public override async Task<IActionResult> Index()
        {
            this.logger.LogInformation("Index");
            var model = new HomeViewModel(await this.handler.InformationAsync(), await this.handler.ReadAsync());
            return this.View("Index", model);
        }

        /// <summary> Stores the material from the form. </summary>
        ///
        /// <param name="model"> The material to save. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Create(MaterialEditViewModel model)
        {
            var data = new PackItLib.Material.Material();
            this.logger.LogInformation("Create Material id {MaterialId}", data.MaterialId);

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

        /// <summary> Display a form to update the specified material. </summary>
        ///
        /// <param name="id"> The material identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            this.logger.LogInformation("Update id {Id}", id.Replace(Environment.NewLine, "").Replace("\n", "").Replace("\r", ""));
            var model = new MaterialEditViewModel
            {
                Data = this.mapper.Map<MaterialEditViewModel.Material>(await this.handler.ReadAsync(id))
            };

            return this.View("Update", model);
        }

        /// <summary> Save the updated material asynchronously. </summary>
        ///
        /// <param name="id"> The material identifier. </param>
        /// <param name="model"> The material data to update. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string id, MaterialEditViewModel model)
        {
            IActionResult ret;
            if (!ModelState.IsValid)
            {
                ret = View(model);
            }
            else
            {
                var data = await this.handler.ReadAsync(id);
                this.logger.LogInformation("Update id {Id} Material id {MaterialId}", id.Replace(Environment.NewLine, "").Replace("\n", "").Replace("\r", ""), model.Data.MaterialId);

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

        /// <summary> Display a delete form for the specified material. </summary>
        ///
        /// <param name="id"> The material identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            this.logger.LogInformation("Delete id {Id}", id.Replace(Environment.NewLine, "").Replace("\n", "").Replace("\r", ""));
            var model = new MaterialEditViewModel
            {
                Data = this.mapper.Map<MaterialEditViewModel.Material>(await this.handler.ReadAsync(id))
            };

            return this.View("Delete", model);
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
                this.logger.LogInformation("CostingRow index {Index}", index);

                this.ViewBag.crud = Crud.Create;
                this.ViewData.TemplateInfo.HtmlFieldPrefix = string.Format("Data.Costings[{0}]", index);

                var mod = new PackItLib.Material.Costing();
                ret = this.PartialView("EditorTemplates/Costing", mod);
            }

            return ret;
        }

        /// <summary> Get a section row. Used when adding a new row to the html. </summary>
        ///
        /// <param name="body"> The body of the POST command. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SectionRow([FromBody] JsonDocument body)
        {
            IActionResult ret;
            if (!ModelState.IsValid)
            {
                ret = View(body);
            }
            else
            {
                var index = body.RootElement.GetProperty("index").ToString().Replace(Environment.NewLine, "").Replace("\n", "").Replace("\r", "");
                this.logger.LogInformation("SectionRow index {Index}", index);

                this.ViewBag.crud = Crud.Create;
                this.ViewBag.sectionTypes = new ListForFlag<SectionTypes>(0);
                this.ViewData.TemplateInfo.HtmlFieldPrefix = string.Format("Data.Sections[{0}]", index);

                var mod = new PackItLib.Material.Section();
                ret = this.PartialView("EditorTemplates/Section", mod);
            }

            return ret;
        }
    }
}

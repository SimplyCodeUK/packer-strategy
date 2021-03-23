// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Materials.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using PackIt.Helpers.Enums;
    using PackItUI.Areas.Common.Controller;
    using PackItUI.Areas.Common.DTO;
    using PackItUI.Areas.Materials.Models;
    using PackItUI.Helpers;

    /// <summary> A controller for handling the Materials Home Page. </summary>
    ///
    /// <seealso cref="T:PackItUI.Areas.Common.Controller.PackItController{TCategoryName, TData, TModel, TEditViewModel}"/>
    [Area("Materials")]
    public class HomeController : PackItController<HomeController, PackIt.Material.Material, MaterialEditViewModel.Material, MaterialEditViewModel>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        ///
        /// <param name="logger"> The logger. </param>
        /// <param name="handler"> The I/O handler. </param>
        public HomeController(ILogger<HomeController> logger, DbServiceHandler<PackIt.Material.Material> handler)
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
            var data = new PackIt.Material.Material();
            this.logger.LogInformation("Create Material id {0}", data.MaterialId);

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

        /// <summary> Display a form to update the specified material. </summary>
        ///
        /// <param name="id"> The material identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            this.logger.LogInformation("Update id {0}", id);
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
            var data = await this.handler.ReadAsync(id);
            this.logger.LogInformation("Update id {0} Material id {1}", id, model.Data.MaterialId);

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

        /// <summary> Display a delete form for the specified material. </summary>
        ///
        /// <param name="id"> The material identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            this.logger.LogInformation("Delete id {0}", id);
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
        public IActionResult CostingRow([FromBody]Newtonsoft.Json.Linq.JObject body)
        {
            var index = body["index"];
            this.logger.LogInformation("CostingRow index {0}", index);

            ViewBag.crud = Crud.Create;
            ViewData.TemplateInfo.HtmlFieldPrefix = string.Format("Data.Costings[{0}]", index);

            var mod = new PackIt.Material.Costing();
            var ret = this.PartialView("EditorTemplates/Costing", mod);

            return ret;
        }

        /// <summary> Get a section row. Used when adding a new row to the html. </summary>
        ///
        /// <param name="body"> The body of the POST command. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        public IActionResult SectionRow([FromBody]Newtonsoft.Json.Linq.JObject body)
        {
            var index = body["index"];
            this.logger.LogInformation("SectionRow index {0}", index);

            ViewBag.crud = Crud.Create;
            ViewBag.sectionTypes = new ListForFlag<SectionTypes>(0);
            ViewData.TemplateInfo.HtmlFieldPrefix = string.Format("Data.Sections[{0}]", index);

            var mod = new PackIt.Material.Section();
            var ret = this.PartialView("EditorTemplates/Section", mod);

            return ret;
        }
    }
}

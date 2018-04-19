// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Packs.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using PackItUI.Areas.Packs.DTO;
    using PackItUI.Areas.Packs.Models;

    /// <summary> A controller for handling the Packs Home Page. </summary>
    [Area("Packs")]
    public class HomeController : Controller
    {
        /// <summary> The packs handler. </summary>
        private readonly IPackHandler handler;

        /// <summary> The mapper to view model. </summary>
        private readonly IMapper mapper = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<PackIt.Pack.Pack, PackEditViewModel.Pack>();
                cfg.CreateMap<PackEditViewModel.Pack, PackIt.Pack.Pack>();
            }).CreateMapper();

        /// <summary>
        /// Initialises a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        ///
        /// <param name="handler"> The I/O handler. </param>
        public HomeController(IPackHandler handler)
        {
            this.handler = handler;
        }

        /// <summary> Handle the Packs view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel(await this.handler.InformationAsync(), await this.handler.ReadAsync());
            return this.View("Index", model);
        }

        /// <summary> Display the form to create a new pack. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public IActionResult Create()
        {
            var model = new PackEditViewModel();
            return this.View("Create", model);
        }

        /// <summary> Stores the pack from the form. </summary>
        ///
        /// <param name="model"> The pack to save. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PackEditViewModel model)
        {
            PackIt.Pack.Pack data = new PackIt.Pack.Pack();

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
            PackIt.Pack.Pack data = await this.handler.ReadAsync(id);

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
            var model = new PackEditViewModel
            {
                Data = this.mapper.Map<PackEditViewModel.Pack>(await this.handler.ReadAsync(id))
            };

            return this.View("Delete", model);
        }

        /// <summary> Deletes the specified pack. </summary>
        ///
        /// <param name="id"> The pack identifier. </param>
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

        /// <summary> Display the specified pack. </summary>
        ///
        /// <param name="id"> The pack identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Display(string id)
        {
            var model = await this.handler.ReadAsync(id);

            return this.View("Display", model);
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

            ViewBag.crud = Helpers.Crud.Create;
            ViewData.TemplateInfo.HtmlFieldPrefix = string.Format("Data.Costings[{0}]", index);

            var mod = new PackIt.Pack.Costing();
            var ret = this.PartialView("EditorTemplates/Costing", mod);

            return ret;
        }
    }
}

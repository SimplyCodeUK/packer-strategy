// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Materials.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using PackItUI.Areas.Materials.DTO;
    using PackItUI.Areas.Materials.Models;

    /// <summary>   A controller for handling the Materials Home Page. </summary>
    [Area("Materials")]
    public class HomeController : Controller
    {
        /// <summary> The materials handler. </summary>
        private readonly IMaterialHandler handler;

        /// <summary> The mapper to view model. </summary>
        private readonly IMapper mapper = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<PackIt.Material.Material, MaterialUpdateViewModel.Material>();
                cfg.CreateMap<MaterialUpdateViewModel.Material, PackIt.Material.Material>();
            }).CreateMapper();

        /// <summary>
        /// Initialises a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        ///
        /// <param name="handler"> The I/O handler. </param>
        public HomeController(IMaterialHandler handler)
        {
            this.handler = handler;
        }

        /// <summary>   Handle the Materials view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel(await this.handler.InformationAsync(), await this.handler.ReadAsync());
            return this.View("Index", model);
        }

        /// <summary> Display the form to create a new material. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public IActionResult Create()
        {
            var model = new MaterialViewModel();
            return this.View("Create", model);
        }

        /// <summary> Stores the material from the form. </summary>
        ///
        /// <param name="model"> The material to save. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MaterialViewModel model)
        {
            if (ModelState.IsValid && await this.handler.CreateAsync(model.Data))
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
            var model = new MaterialUpdateViewModel
            {
                Data = this.mapper.Map<MaterialUpdateViewModel.Material>(await this.handler.ReadAsync(id))
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
        public async Task<IActionResult> Update(string id, MaterialUpdateViewModel model)
        {
            PackIt.Material.Material data = await this.handler.ReadAsync(id);

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
            var model = new MaterialViewModel
            {
                Data = await this.handler.ReadAsync(id)
            };

            return this.View("Delete", model);
        }

        /// <summary> Deletes the specified material. </summary>
        ///
        /// <param name="id"> The material identifier. </param>
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

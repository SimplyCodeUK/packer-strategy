// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.Common.Controller
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using PackItUI.Areas.Common.DTO;
    using System.Threading.Tasks;

    /// <summary> A base class for MVC controller handling data. </summary>
    ///
    /// <typeparam name="TCategoryName"> The logger category. </typeparam>
    /// <typeparam name="TData"> The type of the data. </typeparam>
    /// <typeparam name="TModel"> Controller Model. </typeparam>
    /// <typeparam name="TEditViewModel"> Edit view model. </typeparam>

    public abstract class PackItController<TCategoryName, TData, TModel, TEditViewModel> : Controller
        where TData : new()
        where TEditViewModel : new()
    {
        /// <summary> The logger. </summary>
        protected readonly ILogger<TCategoryName> logger;

        /// <summary> The materials handler. </summary>
        protected readonly DbServiceHandler<TData> handler;

        /// <summary> The mapper to view model. </summary>
        protected readonly IMapper mapper;

        /// <summary>
        /// Initialises a new instance of the <see cref="PackItController{TCategoryName, TData, TModel, TEditViewModel}" /> class.
        /// </summary>
        ///
        /// <param name="logger"> The logger. </param>
        /// <param name="handler"> The I/O handler. </param>
        public PackItController(ILogger<TCategoryName> logger, DbServiceHandler<TData> handler)
        {
            this.logger = logger;
            this.handler = handler;
            this.mapper = new MapperConfiguration(
                            cfg =>
                            {
                                cfg.CreateMap<TData, TModel>();
                                cfg.CreateMap<TModel, TData>();
                            }).CreateMapper();
        }

        /// <summary> Handle the Materials view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public abstract Task<IActionResult> Index();

        /// <summary> Stores the plan from the form. </summary>
        ///
        /// <param name="model"> The plan to save. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public abstract Task<IActionResult> Create(TEditViewModel model);

        /// <summary> Display the form to create a new data item. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public IActionResult Create()
        {
            this.logger.LogInformation("Create");
            var model = new TEditViewModel();
            return this.View("Create", model);
        }

        /// <summary> Deletes the specified data. </summary>
        ///
        /// <param name="id"> The data identifier. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DoDelete(string id)
        {
            this.logger.LogInformation("DoDelete id {0}", id);
            await this.handler.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

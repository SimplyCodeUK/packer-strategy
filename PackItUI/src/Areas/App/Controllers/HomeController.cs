// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Areas.App.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using PackItUI.Areas.App.Models;
    using PackItUI.Areas.Common.DTO;
    using PackItUI.Areas.Uploads.DTO;
    using PackItUI.Services;

    /// <summary> A controller for handling the Home Page. </summary>
    [Area("App")]
    public class HomeController : Controller
    {
        /// <summary> The logger. </summary>
        private readonly ILogger<HomeController> logger;

        /// <summary> The dictionary of all services. </summary>
        private readonly Dictionary<string, IServiceHandler> services;

        /// <summary>
        /// Initialises a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        ///
        /// <param name="logger"> The logger. </param>
        /// <param name="materialHandler"> The Material service handler. </param>
        /// <param name="packHandler"> The Pack service handler. </param>
        /// <param name="planHandler"> The Plan service handler. </param>
        /// <param name="uploadHandler"> The Upload service handler. </param>
        /// <param name="drawHandler"> The Draw service handler. </param>
        public HomeController(ILogger<HomeController> logger, DbServiceHandler<PackIt.Material.Material> materialHandler, DbServiceHandler<PackIt.Pack.Pack> packHandler, DbServiceHandler<PackIt.Plan.Plan> planHandler, IUploadHandler uploadHandler, Packs.DTO.DrawHandler drawHandler)
        {
            this.logger = logger;
            this.services = new()
            {
                { "Materials", materialHandler },
                { "Packs", packHandler },
                { "Plans", planHandler },
                { "Uploads", uploadHandler },
                { "Drawings", drawHandler },
            };
        }

        /// <summary> Handle the Index view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public IActionResult Index()
        {
            this.logger.LogInformation("Index");
            return this.View("Index");
        }

        /// <summary> Handle the About view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public async Task<IActionResult> About()
        {
            this.logger.LogInformation("About");
            var model = new AboutViewModel();
            await model.Create(this.services);
            return this.View("About", model);
        }

        /// <summary> Handle the Contact view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public IActionResult Contact()
        {
            this.logger.LogInformation("Contact");
            return this.View("Contact");
        }

        /// <summary> Handle exceptions. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpGet]
        public IActionResult Error()
        {
            this.logger.LogInformation("Error");
            return this.View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}

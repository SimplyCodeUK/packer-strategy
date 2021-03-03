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

    /// <summary> A base class for MVC controller handling data. </summary>
    public class PackItController<TCategoryName, TData, TModel> : Controller
        where TData : new()
    {
        /// <summary> The logger. </summary>
        protected readonly ILogger<TCategoryName> logger;

        /// <summary> The materials handler. </summary>
        protected readonly DbServiceHandler<TData> handler;

        /// <summary> The mapper to view model. </summary>
        protected readonly IMapper mapper;

        /// <summary>
        /// Initialises a new instance of the <see cref="PackItController{TCategoryName, TData}" /> class.
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
    }
}

// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItDraw.Controllers
{
    using System;
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using PackIt.DTO;
    using PackIt.Pack;

    /// <summary> The root controller of the service. </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DrawingsController : Controller
    {
        /// <summary> The logger. </summary>
        private readonly ILogger<DrawingsController> logger;

        /// <summary> The repository. </summary>
        private readonly IDrawingRepository repository;

        /// <summary>
        /// Initialises a new instance of the <see cref="AboutController" /> class.
        /// </summary>
        ///
        /// <param name="logger"> The logger. </param>
        /// <param name="repository"> The repository. </param>
        public DrawingsController(ILogger<DrawingsController> logger, IDrawingRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        /// <summary> Get service information. </summary>
        ///
        /// <returns> (An Action that handles HTTP GET requests) The service information. </returns>
        [HttpGet]
        public IActionResult Get()
        {
            this.logger.LogInformation("Get");
            return this.Ok(this.repository.GetAll());
        }

        /// <summary>
        /// (An Action that handles HTTP GET requests) Gets an IActionResult using the given
        /// identifier containing a pack.
        /// </summary>
        ///
        /// <param name="id"> The identifier. </param>
        ///
        /// <returns> An IActionResult containing the Pack if it exists. </returns>
        [HttpGet("{id}")]
        [Route("{id}", Name = "GetDrawing")]
        [ProducesResponseType(typeof(Pack), 200)]
        public IActionResult Get(string id)
        {
            this.logger.LogInformation("Get id {0}", id);
            var item = this.repository.Find(id);

            if (item == null)
            {
                return this.NotFound(id);
            }

            return this.Ok(item);
        }

        /// <summary> (An Action that handles HTTP POST requests) Post a new Pack. </summary>
        ///
        /// <param name="value"> The new Pack. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        public IActionResult Post([FromBody] Pack value)
        {
            IActionResult result;

            if (value != null)
            {
                this.logger.LogInformation("Post Pack id {0}", value.PackId);
                try
                {
                    this.repository.Add(value);
                    result = this.CreatedAtRoute("GetDrawing", new { id = value.PackId }, value);
                }
                catch (Exception)
                {
                    result = this.StatusCode((int)HttpStatusCode.Conflict);
                }
            }
            else
            {
                result = this.BadRequest();
            }

            return result;
        }

        /// <summary> (An Action that handles HTTP DELETE requests) Deletes a Pack. </summary>
        ///
        /// <param name="id"> The identifier of the Pack. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            this.logger.LogInformation("Delete id {0}", id);
            if (this.repository.Find(id) == null)
            {
                return this.NotFound(id);
            }

            this.repository.Remove(id);
            return this.Ok();
        }
    }
}

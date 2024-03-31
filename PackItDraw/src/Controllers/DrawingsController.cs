// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItDraw.Controllers
{
    using System;
    using System.Net;
    using Asp.Versioning;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using PackIt.Drawing;
    using PackIt.DTO;
    using PackIt.Models;
    using PackIt.Pack;

    /// <summary> The root controller of the service. </summary>
    /// <remarks>
    /// Initialises a new instance of the <see cref="DrawingsController" /> class.
    /// </remarks>
    ///
    /// <param name="logger"> The logger. </param>
    /// <param name="repository"> The repository. </param>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DrawingsController(ILogger<DrawingsController> logger, IDrawingRepository repository) : Controller
    {
        /// <summary> Get service information. </summary>
        ///
        /// <returns> (An Action that handles HTTP GET requests) The service information. </returns>
        [HttpGet]
        public IActionResult Get()
        {
            logger.LogInformation("Get");
            return this.Ok(repository.GetAll());
        }

        /// <summary>
        /// (An Action that handles HTTP GET requests) Gets an IActionResult using the given
        /// identifier containing a drawing.
        /// </summary>
        ///
        /// <param name="id"> The identifier. </param>
        ///
        /// <returns> An IActionResult containing the Drawing if it exists. </returns>
        [HttpGet("{id}")]
        [Route("{id}", Name = "GetDrawing")]
        [ProducesResponseType(typeof(Drawing), 200)]
        public IActionResult Get(string id)
        {
            logger.LogInformation("Get id {Id}", id);
            var item = repository.Find(id);

            if (item == null)
            {
                return this.NotFound(id);
            }

            return this.Ok(item);
        }

        /// <summary> (An Action that handles HTTP POST requests) Post a new Drawing. </summary>
        ///
        /// <param name="pack"> The new Pack to create the drawing for. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        public IActionResult Post([FromBody] Pack pack)
        {
            IActionResult result;
            Drawing value = new(pack);

            if (pack != null)
            {
                logger.LogInformation("Post Drawing id {DrawingId}", value.DrawingId);
                try
                {
                    repository.Add(value);
                    // Start thread to create drawing
                    DoDrawing.Start(value.DrawingId, repository);
                    result = this.CreatedAtRoute("GetDrawing", new { id = value.DrawingId }, value);
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

        /// <summary> (An Action that handles HTTP DELETE requests) Deletes a Drawing. </summary>
        ///
        /// <param name="id"> The identifier of the Drawing. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            logger.LogInformation("Delete id {Id}", id);
            if (repository.Find(id) == null)
            {
                return this.NotFound(id);
            }

            repository.Remove(id);
            return this.Ok();
        }
    }
}

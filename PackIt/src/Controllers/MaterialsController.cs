// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Controllers
{
    using System;
    using System.Net;
    using Asp.Versioning;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using PackIt.DTO;
    using PackIt.Material;

    /// <summary> A controller for handling materials. </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MaterialsController : Controller
    {
        /// <summary> The logger. </summary>
        private readonly ILogger<MaterialsController> logger;

        /// <summary> The repository. </summary>
        private readonly IMaterialRepository repository;

        /// <summary>
        /// Initialises a new instance of the <see cref="MaterialsController" /> class.
        /// </summary>
        ///
        /// <param name="logger"> The logger. </param>
        /// <param name="repository"> The repository. </param>
        public MaterialsController(ILogger<MaterialsController> logger, IMaterialRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        /// <summary>
        /// (An Action that handles HTTP GET requests) Enumerates all items in this collection.
        /// </summary>
        ///
        /// <returns> An enumerator that allows foreach to be used to process the matched items. </returns>
        [HttpGet]
        public IActionResult Get()
        {
            this.logger.LogInformation("Get");
            return this.Ok(this.repository.GetAll());
        }

        /// <summary>
        /// (An Action that handles HTTP GET requests) Gets an IActionResult using the given
        /// identifier containing a material.
        /// </summary>
        ///
        /// <param name="id"> The identifier. </param>
        ///
        /// <returns> An IActionResult containing the Material if it exists. </returns>
        [HttpGet("{id}")]
        [Route("{id}", Name = "GetMaterial")]
        [ProducesResponseType(typeof(Material), 200)]
        public IActionResult Get(string id)
        {
            this.logger.LogInformation("Get id {Id}", id);
            var item = this.repository.Find(id);

            if (item == null)
            {
                return this.NotFound(id);
            }

            return this.Ok(item);
        }

        /// <summary> (An Action that handles HTTP POST requests) Post a new Material. </summary>
        ///
        /// <param name="value"> The new Material. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Post([FromBody] Material value)
        {
            IActionResult result;

            if (value != null)
            {
                this.logger.LogInformation("Post Material id {MaterialId}", value.MaterialId);
                try
                {
                    this.repository.Add(value);
                    result = this.CreatedAtRoute("GetMaterial", new { id = value.MaterialId }, value);
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

        /// <summary>
        /// (An Action that handles HTTP PUT requests) Put an update to an existing Material.
        /// </summary>
        ///
        /// <param name="id"> The identifier. </param>
        /// <param name="value"> The value. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Material value)
        {
            this.logger.LogInformation("Put id {Id} for Material id {MaterialId}", id, value.MaterialId);
            var item = this.repository.Find(id);

            if (item == null)
            {
                return this.NotFound(id);
            }

            item = value;
            item.MaterialId = id;
            this.repository.Update(item);
            return this.Ok();
        }

        /// <summary> (An Action that handles HTTP DELETE requests) Deletes a Material. </summary>
        ///
        /// <param name="id"> The identifier of the Material. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            this.logger.LogInformation("Delete id {Id}", id);
            if (this.repository.Find(id) == null)
            {
                return this.NotFound(id);
            }

            this.repository.Remove(id);
            return this.Ok();
        }

        /// <summary>
        /// (An Action that handles HTTP PATCH requests) Patches an existing Material.
        /// </summary>
        ///
        /// <param name="id"> The identifier. </param>
        /// <param name="update"> The update. </param>
        ///
        /// <returns> An IActionResult containing the updated Material. </returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(string id, [FromBody] JsonPatchDocument<Material> update)
        {
            this.logger.LogInformation("Patch id {Id}", id);
            var item = this.repository.Find(id);

            if (item == null)
            {
                return this.NotFound(id);
            }

            update.ApplyTo(item);
            this.repository.Update(item);
            return this.Ok(item);
        }
    }
}

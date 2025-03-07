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
    /// <remarks>
    /// Initialises a new instance of the <see cref="MaterialsController" /> class.
    /// </remarks>
    ///
    /// <param name="logger"> The logger. </param>
    /// <param name="repository"> The repository. </param>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MaterialsController(ILogger<MaterialsController> logger, IMaterialRepository repository) : Controller
    {
        /// <summary>
        /// (An Action that handles HTTP GET requests) Enumerates all items in this collection.
        /// </summary>
        ///
        /// <returns> An enumerator that allows foreach to be used to process the matched items. </returns>
        [HttpGet]
        public IActionResult Get()
        {
            logger.LogInformation("Get");
            return this.Ok(repository.GetAll());
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
            logger.LogInformation("Get id {Id}", id.Replace(Environment.NewLine, "").Replace("\n", "").Replace("\r", ""));
            var item = repository.Find(id);

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
            IActionResult ret;

            if (value != null)
            {
                logger.LogInformation("Post Material id {MaterialId}", value.MaterialId);
                if (!ModelState.IsValid)
                {
                    ret = this.BadRequest();
                }
                else
                {
                    try
                    {
                        repository.Add(value);
                        ret = this.CreatedAtRoute("GetMaterial", new { id = value.MaterialId }, value);
                    }
                    catch (Exception)
                    {
                        ret = this.StatusCode((int)HttpStatusCode.Conflict);
                    }
                }
            }
            else
            {
                ret = this.BadRequest();
            }

            return ret;
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
            logger.LogInformation("Put id {Id} for Material id {MaterialId}", id.Replace(Environment.NewLine, "").Replace("\n", "").Replace("\r", ""), value.MaterialId);
            IActionResult ret;
            var item = repository.Find(id);

            if (item == null)
            {
                ret = this.NotFound(id);
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    ret = this.BadRequest();
                }
                else
                {
                    item = value;
                    item.MaterialId = id;
                    repository.Update(item);
                    ret = this.Ok();
                }
            }

            return ret;
        }

        /// <summary> (An Action that handles HTTP DELETE requests) Deletes a Material. </summary>
        ///
        /// <param name="id"> The identifier of the Material. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            logger.LogInformation("Delete id {Id}", id.Replace(Environment.NewLine, "").Replace("\n", "").Replace("\r", ""));
            if (repository.Find(id) == null)
            {
                return this.NotFound(id);
            }

            repository.Remove(id);
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
            logger.LogInformation("Patch id {Id}", id.Replace(Environment.NewLine, "").Replace("\n", "").Replace("\r", ""));
            IActionResult ret;
            var item = repository.Find(id);

            if (item == null)
            {
                ret = this.NotFound(id);
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    ret = this.BadRequest();
                }
                else
                {
                    update.ApplyTo(item);
                    repository.Update(item);
                    ret = this.Ok(item);
                }
            }
            return ret;
        }
    }
}

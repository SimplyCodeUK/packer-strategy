// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Controllers
{
    using System;
    using System.Net;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using PackIt.DTO;
    using PackIt.Pack;

    /// <summary>   A controller for handling Packs. </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PacksController : Controller
    {
        /// <summary>   The repository. </summary>
        private readonly IPackRepository repository;

        /// <summary>
        /// Initialises a new instance of the <see cref="PacksController" /> class.
        /// </summary>
        ///
        /// <param name="repository">   The repository. </param>
        public PacksController(IPackRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        ///     (An Action that handles HTTP GET requests) enumerates the items in this collection that
        ///     meet given criteria.
        /// </summary>
        ///
        /// <returns>   An enumerator that allows foreach to be used to process the matched items. </returns>
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(this.repository.GetAll());
        }

        /// <summary>
        ///     (An Action that handles HTTP GET requests) gets an IActionResult using the given
        ///     identifier containing a pack.
        /// </summary>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        [HttpGet("{id}")]
        [Route("{id}", Name = "GetPack")]
        [ProducesResponseType(typeof(Pack), 200)]
        public IActionResult Get(string id)
        {
            var item = this.repository.Find(id);

            if (item == null)
            {
                return this.NotFound(id);
            }

            return this.Ok(item);
        }

        /// <summary>   (An Action that handles HTTP POST requests) post this message. </summary>
        ///
        /// <param name="value">    The value. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        [HttpPost]
        public IActionResult Post([FromBody] Pack value)
        {
            IActionResult result;

            if (value != null)
            {
                try
                {
                    this.repository.Add(value);
                    result = this.CreatedAtRoute("GetPack", new { id = value.PackId }, value);
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

        /// <summary>   Updates an existing Pack. </summary>
        ///
        /// <param name="id">       The identifier. </param>
        /// <param name="value">    The value. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Pack value)
        {
            Pack item = this.repository.Find(id);

            if (item == null)
            {
                return this.NotFound(id);
            }

            item = value;
            item.PackId = id;
            this.repository.Update(item);
            return this.Ok();
        }

        /// <summary>   Deletes the given ID. </summary>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (this.repository.Find(id) == null)
            {
                return this.NotFound(id);
            }

            this.repository.Remove(id);
            return this.Ok();
        }

        /// <summary>   Patches an existing Pack. </summary>
        ///
        /// <param name="id">       The identifier. </param>
        /// <param name="update">   The update. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(string id, [FromBody]JsonPatchDocument<Pack> update)
        {
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

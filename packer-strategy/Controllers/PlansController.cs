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
    using PackIt.Models.Plan;

    /// <summary>   A controller for handling plans. </summary>
    [Route("api/[controller]")]
    public class PlansController : Controller
    {
        /// <summary>   The repository. </summary>
        private readonly IPlanRepository repository;

        /// <summary>
        /// Initialises a new instance of the <see cref="PlansController" /> class.
        /// </summary>
        ///
        /// <param name="repository">   The repository. </param>
        public PlansController(IPlanRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>   (An Action that handles HTTP GET requests) gets the get. </summary>
        ///
        /// <returns>   An IActionResult. </returns>
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(this.repository.GetAll());
        }

        /// <summary>
        ///     (An Action that handles HTTP GET requests) gets an i action result using the given
        ///     identifier.
        /// </summary>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        [HttpGet("{id}")]
        [Route("{id}", Name = "GetPlan")]
        [ProducesResponseType(typeof(Plan), 200)]
        public IActionResult Get(string id)
        {
            var item = this.repository.Find(id);
            IActionResult result;

            if (item == null)
            {
                result = this.NotFound(id);
            }
            else
            {
                result = this.Ok(item);
            }

            return result;
        }

        /// <summary>   (An Action that handles HTTP POST requests) post this message. </summary>
        ///
        /// <param name="value">    The value. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        [HttpPost]
        public IActionResult Post([FromBody] Plan value)
        {
            IActionResult result;

            if (value != null)
            {
                try
                {
                    this.repository.Add(value);
                    result = this.CreatedAtRoute("GetPlan", new { id = value.Id }, value);
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

        /// <summary>   Updates an existing Plan. </summary>
        ///
        /// <param name="id">       The identifier. </param>
        /// <param name="value">    The value. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Plan value)
        {
            Plan item = this.repository.Find(id);
            IActionResult result;

            if (item != null)
            {
                item = value;
                item.Id = id;

                this.repository.Update(item);

                result = this.Ok();
            }
            else
            {
                result = this.NotFound(id);
            }

            return result;
        }

        /// <summary>   Deletes the given ID. </summary>
        ///
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            IActionResult result;

            if (this.repository.Find(id) != null)
            {
                this.repository.Remove(id);
                result = this.Ok();
            }
            else
            {
                result = this.NotFound(id);
            }

            return result;
        }

        /// <summary>   Patches an existing Plan. </summary>
        ///
        /// <param name="id">       The identifier. </param>
        /// <param name="update">   The update. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(string id, [FromBody]JsonPatchDocument<Plan> update)
        {
            var item = this.repository.Find(id);
            IActionResult result;

            if (item != null)
            {
                update.ApplyTo(item);
                result = this.Ok(item);
            }
            else
            {
                result = this.NotFound(id);
            }

            return result;
        }
    }
}

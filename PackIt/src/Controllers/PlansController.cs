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
    using PackIt.Plan;

    /// <summary> A controller for handling plans. </summary>
    /// <remarks>
    /// Initialises a new instance of the <see cref="PlansController" /> class.
    /// </remarks>
    ///
    /// <param name="logger"> The logger. </param>
    /// <param name="repository"> The repository. </param>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PlansController(ILogger<PlansController> logger, IPlanRepository repository) : Controller
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
        /// identifier containing a plan.
        /// </summary>
        ///
        /// <param name="id"> The identifier. </param>
        ///
        /// <returns> An IActionResult containing the Pack if it exists. </returns>
        [HttpGet("{id}")]
        [Route("{id}", Name = "GetPlan")]
        [ProducesResponseType(typeof(Plan), 200)]
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

        /// <summary> (An Action that handles HTTP POST requests) Post a new Plan. </summary>
        ///
        /// <param name="value"> The new Plan. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Post([FromBody] Plan value)
        {
            IActionResult result;

            if (value != null)
            {
                logger.LogInformation("Post Plan id {PlanId}", value.PlanId);
                try
                {
                    repository.Add(value);
                    result = this.CreatedAtRoute("GetPlan", new { id = value.PlanId }, value);
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
        /// (An Action that handles HTTP PUT requests) Put an update to an existing Plan.
        /// </summary>
        ///
        /// <param name="id"> The identifier. </param>
        /// <param name="value"> The value. </param>
        ///
        /// <returns> An IActionResult. </returns>
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Plan value)
        {
            logger.LogInformation("Put id {id} for Plan id {PlanId}", id, value.PlanId);
            var item = repository.Find(id);

            if (item == null)
            {
                return this.NotFound(id);
            }

            item = value;
            item.PlanId = id;
            repository.Update(item);
            return this.Ok();
        }

        /// <summary> (An Action that handles HTTP DELETE requests) Deletes a Plan. </summary>
        ///
        /// <param name="id"> The identifier of the Plan. </param>
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

        /// <summary>
        /// (An Action that handles HTTP PATCH requests) Patches an existing Plan.
        /// </summary>
        ///
        /// <param name="id"> The identifier. </param>
        /// <param name="update"> The update. </param>
        ///
        /// <returns> An IActionResult containing the updated Plan. </returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(string id, [FromBody] JsonPatchDocument<Plan> update)
        {
            logger.LogInformation("Patch id {Id}", id);
            var item = repository.Find(id);

            if (item == null)
            {
                return this.NotFound(id);
            }

            update.ApplyTo(item);
            repository.Update(item);
            return this.Ok(item);
        }
    }
}

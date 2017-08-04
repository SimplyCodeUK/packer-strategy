//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using System;
using System.Collections.Generic;
using System.Net;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using packer_strategy.Models;
using packer_strategy.Models.Plan;

namespace packer_strategy.Controllers
{
    /// <summary>   A controller for handling plans. </summary>
    [Route("api/[controller]")]
    public class PlansController : Controller
    {
        /// <summary>   The repository. </summary>
        private readonly IPlanRepository _repository;

        /// <summary>   Constructor. </summary>
        ///
        /// <param name="repository">   The repository. </param>
        public PlansController(IPlanRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        ///     (An Action that handles HTTP GET requests) enumerates the items in this collection that
        ///     meet given criteria.
        /// </summary>
        ///
        /// <returns>   An enumerator that allows foreach to be used to process the matched items. </returns>
        [HttpGet]
        public IEnumerable<Plan> Get()
        {
            return _repository.GetAll();
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
            var           item = _repository.Find(id);
            IActionResult result;

            if (item == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(item);
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
                    _repository.Add(value);
                    result = CreatedAtRoute("GetPlan", new { id = value.Id }, value);
                }
                catch (Exception)
                {
                    result = StatusCode((int)HttpStatusCode.Conflict);
                }
            }
            else
            {
                result = BadRequest();
            }

            return result;
        }

        /// <summary>   Puts. </summary>
        ///
        /// <param name="id">       The identifier. </param>
        /// <param name="value">    The value. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Plan value)
        {
            Plan          plan = _repository.Find(id);
            IActionResult result;

            if (plan != null)
            {
                plan = value;
                plan.Id = id;

                _repository.Update(plan);

                result = Ok();
            }
            else
            {
                result = NotFound();
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

            if (_repository.Find(id) != null)
            {
                _repository.Remove(id);
                result = Ok();
            }
            else
            {
                result = NotFound();
            }
            return result;
        }

        /// <summary>   Patches. </summary>
        ///
        /// <param name="id">       The identifier. </param>
        /// <param name="update">   The update. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(string id, [FromBody]JsonPatchDocument<Plan> update)
        {
            var           item = _repository.Find(id);
            IActionResult result;

            if (item != null)
            {
                update.ApplyTo(item);
                result = Ok(item);
            }
            else
            {
                result = NotFound();
            }

            return result;
        }
    }
}

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
    /** A controller for handling plans. */
    [Route("api/[controller]")]
    public class PlansController : Controller
    {
        private readonly IPlanRepository _repository;   ///< The repository

        /**
         * Constructor.
         *
         * @param   repository  The repository.
         */
        public PlansController(IPlanRepository repository)
        {
            _repository = repository;
        }

        /**
         * (An Action that handles HTTP GET requests) enumerates the items in this collection that meet
         * given criteria.
         *
         * @return  An enumerator that allows foreach to be used to process the matched items.
         */
        [HttpGet]
        public IEnumerable<Plan> Get()
        {
            return _repository.GetAll();
        }

        /**
         * (An Action that handles HTTP GET requests) gets an i action result using the given identifier.
         *
         * @param   id  The Identifier to get.
         *
         * @return  An IActionResult.
         */
        [HttpGet("{id}")]
        [Route("{id}", Name = "GetPlan")]
        [ProducesResponseType(typeof(Plan), 200)]
        public IActionResult Get(string id)
        {
            IActionResult result = NotFound();
            var           item = _repository.Find(id);

            if (item != null)
            {
                result = Ok(item);
            }

            return result;
        }

        /**
         * (An Action that handles HTTP POST requests) post this message.
         *
         * @param   value   The value.
         *
         * @return  An IActionResult.
         */
        [HttpPost]
        public IActionResult Post([FromBody] Plan value)
        {
            IActionResult result = BadRequest();

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
            return result;
        }

        /**
         * Puts.
         *
         * @param   id      The identifier.
         * @param   value   The value.
         *
         * @return  An IActionResult.
         */
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Plan value)
        {
            IActionResult result = NotFound();
            Plan          plan = _repository.Find(id);

            if (plan != null)
            {
                plan.Id = id;
                plan.Name = value.Name;
                plan.Notes = value.Notes;
                plan.Time = value.Time;

                _repository.Update(plan);

                result = Ok();
            }

            return result;
        }

        /**
         * Deletes the given ID.
         *
         * @param   id  The Identifier to delete.
         *
         * @return  An IActionResult.
         */
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            IActionResult result = NotFound();

            if (_repository.Find(id) != null)
            {
                _repository.Remove(id);
                result = Ok();
            }
            return result;
        }

        /**
         * Patches.
         *
         * @param   id      The identifier.
         * @param   patch   The patch.
         *
         * @return  An IActionResult.
         */
        [HttpPatch("{id}")]
        public IActionResult Patch(string id, [FromBody]JsonPatchDocument<Plan> patch)
        {
            IActionResult result = NotFound();
            var item = _repository.Find(id);

            if (item != null)
            {
                patch.ApplyTo(item);
                result = Ok(item);
            }

            return result;
        }
    }
}

//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using packer_strategy.Models;
using packer_strategy.Models.Plan;

namespace packer_strategy.Controllers
{
    /*! A controller for handling plans. */
    [Route("api/[controller]")]
    public class PlansController : Controller
    {
        /*! The repository */
        private readonly IPlanRepository _repository;

        /*!
         * Constructor.
         *
         * @param   repository  The repository.
         */
        public PlansController(IPlanRepository repository)
        {
            _repository = repository;
        }

        /*!
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

        /*!
         * (An Action that handles HTTP GET requests) gets an i action result using the given
         * identifier.
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
                result = Ok();
            }

            return result;
        }

        /*!
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
                    result = CreatedAtRoute("GetPlan", new { id = value.ID }, value);
                }
                catch (Exception)
                {
                    return Forbid();
                }
            }
            return result;
        }

        /*!
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
            IActionResult result = BadRequest();

            if (id != null)
            {
                Plan plan = _repository.Find(id);

                if (plan == null)
                {
                    result = NotFound();
                }
                else
                {
                    plan.ID = id;
                    plan.Name = value.Name;
                    plan.Notes = value.Notes;
                    plan.Time = value.Time;

                    _repository.Update(plan);

                    result = Ok();
                }
            }
            return result;
        }

        /*!
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
    }
}

//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using System;
using System.Collections.Generic;
using System.Net;

using Microsoft.AspNetCore.Mvc;

using packer_strategy.Models;
using packer_strategy.Models.Plan;

namespace packer_strategy.Controllers
{
    /*!
     * \class   PlansController
     *
     * \brief   A controller for handling plans.
     */
    [Route("api/[controller]")]
    public class PlansController : Controller
    {
        /*! \brief   The repository */
        private readonly IPlanRepository _repository;

        /*!
         * \fn  public PlansController(IPlanRepository repository)
         *
         * \brief   Constructor.
         *
         * \param   repository  The repository.
         */
        public PlansController(IPlanRepository repository)
        {
            _repository = repository;
        }

        /*!
         * \fn  public IEnumerable<Plan> Get()
         *
         * \brief   GET api/strategies.
         *
         * \return  An enumerator that allows foreach to be used to process the matched items.
         */
        [HttpGet]
        public IEnumerable<Plan> Get()
        {
            return _repository.GetAll();
        }

        /*!
         * \fn  public IActionResult Get(string id)
         *
         * \brief   GET api/strategies/5.
         *
         * \param   id  The Identifier to get.
         *
         * \return  An IActionResult.
         */
        [HttpGet("{id}")]
        [Route("{id}", Name = "GetStrategy")]
        [ProducesResponseType(typeof(Plan), 200)]
        public IActionResult Get(string id)
        {
            var item = _repository.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        /*!
         * \fn  public IActionResult Post([FromBody] Plan value)
         *
         * \brief   POST api/strategies.
         *
         * \param   value   The value.
         *
         * \return  An IActionResult.
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
                    result = CreatedAtRoute("GetStrategy", new { id = value.Id }, value);
                }
                catch (Exception)
                {
                    return Forbid();
                }
            }
            return result;
        }

        /*!
         * \fn  public IActionResult Put(string id, [FromBody] Plan value)
         *
         * \brief   PUT api/strategies/5.
         *
         * \param   id      The identifier.
         * \param   value   The value.
         *
         * \return  An IActionResult.
         */
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Plan value)
        {
            if (value == null || value.Id != id)
            {
                return BadRequest();
            }

            var todo = _repository.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Name = value.Name;
            todo.Notes = value.Notes;
            todo.Time = value.Time;

            _repository.Update(todo);
            return new NoContentResult();
        }

        /*!
         * \fn  public IActionResult Delete(string id)
         *
         * \brief   DELETE api/strategies/5.
         *
         * \param   id  The Identifier to delete.
         *
         * \return  An IActionResult.
         */
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var todo = _repository.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _repository.Remove(id);
            return new NoContentResult();
        }
    }
}

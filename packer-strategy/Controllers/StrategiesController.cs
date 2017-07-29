//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using packer_strategy.Models;

namespace packer_strategy.Controllers
{
    [Route("api/[controller]")]
    public class StrategiesController : Controller
    {
        private readonly IStrategyRepository _repository;

        public StrategiesController(IStrategyRepository repository)
        {
            _repository = repository;
        }

        // GET api/strategies
        [HttpGet]
        public IEnumerable<Strategy> Get()
        {
            return _repository.GetAll();
        }

        // GET api/strategies/5
        [HttpGet("{id}")]
        [Route("{id}", Name = "GetStrategy")]
        [ProducesResponseType(typeof(Strategy), 200)]
        public IActionResult Get(string id)
        {
            var item = _repository.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/strategies
        [HttpPost]
        public IActionResult Post([FromBody] Strategy value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            _repository.Add(value);

            return CreatedAtRoute("GetStrategy", new { id = value.Id }, value);
        }

        // PUT api/strategies/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Strategy value)
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

        // DELETE api/strategies/5
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

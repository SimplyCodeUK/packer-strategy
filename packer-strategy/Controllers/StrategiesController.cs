using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            return CreatedAtRoute("GetStrategy", new { id = value.Key }, value);
        }

        // PUT api/strategies/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Strategy value)
        {
            if (value == null || value.Key != id)
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

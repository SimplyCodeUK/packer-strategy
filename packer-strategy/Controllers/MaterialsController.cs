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
using packer_strategy.Models.Material;

namespace packer_strategy.Controllers
{
    /// <summary>   A controller for handling materials. </summary>
    [Route("api/[controller]")]
    public class MaterialsController : Controller
    {
        /// <summary>   The repository. </summary>
        private readonly IMaterialRepository _repository;

        /// <summary>   Constructor. </summary>
        ///
        /// <param name="repository">   The repository. </param>
        public MaterialsController(IMaterialRepository repository)
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
        public IEnumerable<Material> Get(Material.Type type)
        {
            return _repository.GetAll(type);
        }

        /// <summary>
        ///     (An Action that handles HTTP GET requests) gets an i action result using the given
        ///     identifier.
        /// </summary>
        ///
        /// <param name="type"> The type. </param>
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        [HttpGet("{id}")]
        [Route("{id}", Name = "GetMaterial")]
        [ProducesResponseType(typeof(Material), 200)]
        public IActionResult Get(Material.Type type, string id)
        {
            var           item = _repository.Find(type, id);
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
        public IActionResult Post([FromBody] Material value)
        {
            IActionResult result;

            if (value != null)
            {
                try
                {
                    _repository.Add(value);
                    result = CreatedAtRoute("GetMaterial", new { type=value.IdType, id = value.Id }, value);
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
        /// <param name="type">     The type. </param>
        /// <param name="id">       The identifier. </param>
        /// <param name="value">    The value. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        [HttpPut("{id}")]
        public IActionResult Put(Material.Type type, string id, [FromBody] Material value)
        {
            Material      item = _repository.Find(type, id);
            IActionResult result;

            if (item != null)
            {
                item = value;
                item.Id = id;

                _repository.Update(item);

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
        /// <param name="type"> The type. </param>
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Material.Type type, string id)
        {
            IActionResult result;

            if (_repository.Find(type, id) != null)
            {
                _repository.Remove(type, id);
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
        /// <param name="type">     The type. </param>
        /// <param name="id">       The identifier. </param>
        /// <param name="update">   The update. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(Material.Type type, string id, [FromBody]JsonPatchDocument<Material> update)
        {
            var           item = _repository.Find(type, id);
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

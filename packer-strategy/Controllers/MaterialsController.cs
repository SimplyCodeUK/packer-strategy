//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using DTO;
    using Helpers;
    using Helpers.Enums;
    using Models.Material;

    /// <summary>   A controller for handling materials. </summary>
    [Route("api/[controller]")]
    public class MaterialsController : Controller
    {
        /// <summary>   The repository. </summary>
        private readonly IMaterialRepository _repository;
        private readonly Dictionary<string, MaterialType> _types;
        private readonly List<string> _typeNames;

        /// <summary>   Constructor. </summary>
        ///
        /// <param name="repository">   The repository. </param>
        public MaterialsController(IMaterialRepository repository)
        {
            _repository = repository;
            _types = new Dictionary<string, MaterialType>();
            _typeNames = new List<string>();
            for (MaterialType type = MaterialType.Min; type < MaterialType.Max; ++type)
            {
                string urlName = Attributes.UrlName(type);
                _types[urlName] = type;
                _typeNames.Add(urlName);
            }
        }

        /// <summary>   (An Action that handles HTTP GET requests) gets the get. </summary>
        ///
        /// <returns>   An IActionResult. </returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_typeNames);
        }

        /// <summary>
        ///     (An Action that handles HTTP GET requests) enumerates the items in this collection that
        ///     meet given criteria.
        /// </summary>
        ///
        /// <param name="type"> The type. </param>
        ///
        /// <returns>   An enumerator that allows foreach to be used to process the matched items. </returns>
        [HttpGet("{type}")]
        public IActionResult Get(string type)
        {
            IActionResult result;

            if (_types.ContainsKey(type))
            {
                result = Ok(_repository.GetAll(_types[type]));
            }
            else
            {
                result = BadRequest();
            }
            return result;
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
        [HttpGet("{type}/{id}")]
        [Route("{type}/{id}", Name = "GetMaterial")]
        [ProducesResponseType(typeof(Material), 200)]
        public IActionResult Get(string type, string id)
        {
            IActionResult result;

            if (_types.ContainsKey(type))
            {
                var item = _repository.Find(_types[type], id);
                if (item == null)
                {
                    result = NotFound(id);
                }
                else
                {
                    result = Ok(item);
                }
            }
            else
            {
                result = BadRequest();
            }
            return result;
        }

        /// <summary>   (An Action that handles HTTP POST requests) post this message. </summary>
        ///
        /// <param name="type">     The type. </param>
        /// <param name="value">    The value. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        [HttpPost("{type}")]
        public IActionResult Post(string type, [FromBody] Material value)
        {
            IActionResult result;

            if (value != null)
            {
                if (_types.ContainsKey(type))
                {
                    try
                    {
                        value.IdType = _types[type];
                        _repository.Add(value);
                        result = CreatedAtRoute("GetMaterial", new { type, value.Id }, value);
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
        [HttpPut("{type}/{id}")]
        public IActionResult Put(string type, string id, [FromBody] Material value)
        {
            IActionResult result;

            if (_types.ContainsKey(type))
            {
                Material item = _repository.Find(_types[type], id);

                if (item != null)
                {
                    item = value;
                    item.Id = id;

                    _repository.Update(item);

                    result = Ok();
                }
                else
                {
                    result = NotFound(id);
                }
            }
            else
            {
                result = BadRequest();
            }

            return result;
        }

        /// <summary>   Deletes the given ID. </summary>
        ///
        /// <param name="type"> The type. </param>
        /// <param name="id">   The identifier. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        [HttpDelete("{type}/{id}")]
        public IActionResult Delete(string type, string id)
        {
            IActionResult result;

            if (_types.ContainsKey(type))
            {
                if (_repository.Find(_types[type], id) != null)
                {
                    _repository.Remove(_types[type], id);
                    result = Ok();
                }
                else
                {
                    result = NotFound(id);
                }
            }
            else
            {
                result = BadRequest();
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
        [HttpPatch("{type}/{id}")]
        public IActionResult Patch(string type, string id, [FromBody]JsonPatchDocument<Material> update)
        {
            IActionResult result;

            if (_types.ContainsKey(type))
            {
                var item = _repository.Find(_types[type], id);

                if (item != null)
                {
                    update.ApplyTo(item);
                    result = Ok(item);
                }
                else
                {
                    result = NotFound(id);
                }
            }
            else
            {
                result = BadRequest();
            }

            return result;
        }
    }
}

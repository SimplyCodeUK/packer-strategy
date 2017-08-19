//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//

namespace packer_strategy.Controllers
{
    using System;
    using System.Net;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using packer_strategy.Models;
    using packer_strategy.Models.Package;

    /// <summary>   A controller for handling packages. </summary>
    [Route("api/[controller]")]
    public class PackagesController : Controller
    {
        /// <summary>   The repository. </summary>
        private readonly IPackageRepository _repository;

        /// <summary>   Constructor. </summary>
        ///
        /// <param name="repository">   The repository. </param>
        public PackagesController(IPackageRepository repository)
        {
            _repository = repository;
        }

        /// <summary>   (An Action that handles HTTP GET requests) gets the get. </summary>
        ///
        /// <returns>   An IActionResult. </returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetAll());
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
        [Route("{id}", Name = "GetPackage")]
        [ProducesResponseType(typeof(Package), 200)]
        public IActionResult Get(string id)
        {
            var item = _repository.Find(id);
            IActionResult result;

            if (item == null)
            {
                result = NotFound(id);
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
        public IActionResult Post([FromBody] Package value)
        {
            IActionResult result;

            if (value != null)
            {
                try
                {
                    _repository.Add(value);
                    result = CreatedAtRoute("GetPackage", new { id = value.Id }, value);
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
        public IActionResult Put(string id, [FromBody] Package value)
        {
            Package item = _repository.Find(id);
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
                result = NotFound(id);
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
                result = NotFound(id);
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
        public IActionResult Patch(string id, [FromBody]JsonPatchDocument<Package> update)
        {
            var item = _repository.Find(id);
            IActionResult result;

            if (item != null)
            {
                update.ApplyTo(item);
                result = Ok(item);
            }
            else
            {
                result = NotFound(id);
            }

            return result;
        }
    }
}

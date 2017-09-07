// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using PackIt.DTO;
    using PackIt.Helpers;
    using PackIt.Helpers.Enums;
    using PackIt.Models.Material;

    /// <summary>   A controller for handling materials. </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MaterialsController : Controller
    {
        /// <summary>   The repository. </summary>
        private readonly IMaterialRepository repository;

        /// <summary>   The types. </summary>
        private readonly Dictionary<string, MaterialType> types;

        /// <summary>   The type names. </summary>
        private readonly List<string> typeNames;

        /// <summary>
        /// Initialises a new instance of the <see cref="MaterialsController" /> class.
        /// </summary>
        ///
        /// <param name="repository">   The repository. </param>
        public MaterialsController(IMaterialRepository repository)
        {
            this.repository = repository;
            this.types = new Dictionary<string, MaterialType>();
            this.typeNames = new List<string>();
            for (MaterialType type = MaterialType.Min; type < MaterialType.Max; ++type)
            {
                string urlName = Attributes.UrlName(type);
                this.types[urlName] = type;
                this.typeNames.Add(urlName);
            }
        }

        /// <summary>   (An Action that handles HTTP GET requests) gets the get. </summary>
        ///
        /// <returns>   An IActionResult. </returns>
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(this.typeNames);
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

            if (this.types.ContainsKey(type))
            {
                result = this.Ok(this.repository.GetAll(this.types[type]));
            }
            else
            {
                result = this.BadRequest();
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

            if (this.types.ContainsKey(type))
            {
                var item = this.repository.Find(this.types[type], id);
                if (item == null)
                {
                    result = this.NotFound(id);
                }
                else
                {
                    result = this.Ok(item);
                }
            }
            else
            {
                result = this.BadRequest();
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
                if (this.types.ContainsKey(type))
                {
                    try
                    {
                        value.IdType = this.types[type];
                        this.repository.Add(value);
                        result = this.CreatedAtRoute("GetMaterial", new { type, value.Id }, value);
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
            }
            else
            {
                result = this.BadRequest();
            }

            return result;
        }

        /// <summary>   Updates an existing Material. </summary>
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

            if (this.types.ContainsKey(type))
            {
                Material item = this.repository.Find(this.types[type], id);

                if (item != null)
                {
                    item = value;
                    item.Id = id;
                    this.repository.Update(item);
                    result = this.Ok();
                }
                else
                {
                    result = this.NotFound(id);
                }
            }
            else
            {
                result = this.BadRequest();
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

            if (this.types.ContainsKey(type))
            {
                if (this.repository.Find(this.types[type], id) != null)
                {
                    this.repository.Remove(this.types[type], id);
                    result = this.Ok();
                }
                else
                {
                    result = this.NotFound(id);
                }
            }
            else
            {
                result = this.BadRequest();
            }

            return result;
        }

        /// <summary>   Patches an existing Material. </summary>
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

            if (this.types.ContainsKey(type))
            {
                var item = this.repository.Find(this.types[type], id);

                if (item != null)
                {
                    update.ApplyTo(item);
                    this.repository.Update(item);
                    result = this.Ok(item);
                }
                else
                {
                    result = this.NotFound(id);
                }
            }
            else
            {
                result = this.BadRequest();
            }

            return result;
        }
    }
}

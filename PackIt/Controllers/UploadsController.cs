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
    using Microsoft.AspNetCore.Mvc;
    using PackIt.DTO;

    /// <summary>   A controller for handling materials. </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UploadsController : Controller
    {
        /// <summary>   The plan repository. </summary>
        private readonly IPlanRepository planRepo;

        /// <summary>   The material repository. </summary>
        private readonly IMaterialRepository materialRepo;

        /// <summary>   The pack repository. </summary>
        private readonly IPackRepository packRepo;

        /// <summary>
        /// Initialises a new instance of the <see cref="UploadsController" /> class.
        /// </summary>
        ///
        /// <param name="planRepo"> The plan repository.</param>
        /// <param name="materialRepo"> The material repository.</param>
        /// <param name="packRepo"> The pack repository.</param>
        public UploadsController(IPlanRepository planRepo, IMaterialRepository materialRepo, IPackRepository packRepo)
        {
            this.planRepo = planRepo;
            this.materialRepo = materialRepo;
            this.packRepo = packRepo;
        }

        /// <summary>   (An Action that handles HTTP POST requests) post this message. </summary>
        ///
        /// <param name="values">    Bulk upload of data to the databases. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        [HttpPost("{type}")]
        public IActionResult Post([FromBody] Bulk values)
        {
            IActionResult result;

            List<string> pass = new List<string>();
            List<string> fail = new List<string>();

            if (values != null)
            {
                foreach (Models.Plan.Plan item in values.Plans)
                {
                    try
                    {
                        this.planRepo.Add(item);
                        pass.Add(item.Id);
                    }
                    catch (Exception)
                    {
                        fail.Add(item.Id);
                    }
                }

                foreach (Models.Material.Material item in values.Materials)
                {
                    try
                    {
                        this.materialRepo.Add(item);
                        pass.Add(item.Id);
                    }
                    catch (Exception)
                    {
                        fail.Add(item.Id);
                    }
                }

                foreach (Models.Pack.Pack item in values.Packs)
                {
                    try
                    {
                        this.packRepo.Add(item);
                        pass.Add(item.PackId);
                    }
                    catch (Exception e)
                    {
                        Console.Out.Write(e.ToString());
                        fail.Add(item.PackId);
                    }
                }

                if (fail.Count == 0)
                {
                    result = this.StatusCode((int)HttpStatusCode.Created, pass);
                }
                else
                {
                    result = this.StatusCode((int)HttpStatusCode.Conflict, fail);
                }
            }
            else
            {
                result = this.BadRequest();
            }

            return result;
        }

        /// <summary>
        /// Object describing data for bulk uploading
        /// </summary>
        public class Bulk
        {
            /// <summary>
            /// Initialises a new instance of the <see cref="Bulk" /> class.
            /// </summary>
            public Bulk()
            {
                this.Plans = new List<Models.Plan.Plan>();
                this.Materials = new List<Models.Material.Material>();
                this.Packs = new List<Models.Pack.Pack>();
            }

            /// <summary>   Gets or sets the list of Plans of this object. </summary>
            ///
            /// <value> List of Plans. </value>
            public List<Models.Plan.Plan> Plans { get; set; }

            /// <summary>   Gets or sets the list of Materials of this object. </summary>
            ///
            /// <value> List of Materials. </value>
            public List<Models.Material.Material> Materials { get; set; }

            /// <summary>   Gets or sets the list of Packs of this object. </summary>
            ///
            /// <value> List of Packs. </value>
            public List<Models.Pack.Pack> Packs { get; set; }
        }
    }
}

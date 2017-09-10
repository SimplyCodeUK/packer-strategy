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

        /// <summary>   The package repository. </summary>
        private readonly IPackageRepository packageRepo;

        /// <summary>
        /// Initialises a new instance of the <see cref="UploadsController" /> class.
        /// </summary>
        ///
        /// <param name="planRepo"> The plan repository.</param>
        /// <param name="materialRepo"> The material repository.</param>
        /// <param name="packageRepo"> The package repository.</param>
        public UploadsController(IPlanRepository planRepo, IMaterialRepository materialRepo, IPackageRepository packageRepo)
        {
            this.planRepo = planRepo;
            this.materialRepo = materialRepo;
            this.packageRepo = packageRepo;
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

                foreach (Models.Package.Package item in values.Packs)
                {
                    try
                    {
                        this.packageRepo.Add(item);
                        pass.Add(item.Id);
                    }
                    catch (Exception e)
                    {
                        Console.Out.Write(e.ToString());
                        fail.Add(item.Id);
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
                this.Packs = new List<Models.Package.Package>();
            }

            /// <summary>   Gets or sets the list of Plans of this object. </summary>
            ///
            /// <value> List of plans. </value>
            public List<Models.Plan.Plan> Plans { get; set; }

            /// <summary>   Gets or sets the list of Materials of this object. </summary>
            ///
            /// <value> List of materials. </value>
            public List<Models.Material.Material> Materials { get; set; }

            /// <summary>   Gets or sets the list of Packages of this object. </summary>
            ///
            /// <value> List of packages. </value>
            public List<Models.Package.Package> Packs { get; set; }
        }
    }
}

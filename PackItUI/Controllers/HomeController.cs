// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Controllers
{
    using System.Diagnostics;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using PackItUI.Models;

    /// <summary>   A controller for handling the Home Page. </summary>
    public class HomeController : Controller
    {
        private readonly AppSettings AppSettings;

        /// <summary>
        /// Initialises a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        ///
        /// <param name="appOptionsAccessor">   The app optins accessor. </param>
        public HomeController(IOptions<AppSettings> appSettings)
        {
            this.AppSettings = appSettings.Value;
        }

        /// <summary>   Handle the Index view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>   Handle the Materials view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        public async Task<IActionResult> Materials()
        {
            string url = AppSettings.ServiceUrls.Materials;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url+"about");

            // Throw an exception if not successful
            response.EnsureSuccessStatusCode();

            string body = await response.Content.ReadAsStringAsync();

            return this.View(JsonConvert.DeserializeObject<MaterialsViewModel>(body));
        }

        /// <summary>   Handle the Packs view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        public async Task<IActionResult> Packs()
        {
            string url = AppSettings.ServiceUrls.Packs;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url + "about");

            // Throw an exception if not successful
            response.EnsureSuccessStatusCode();

            string body = await response.Content.ReadAsStringAsync();

            return this.View(JsonConvert.DeserializeObject<PacksViewModel>(body));
        }

        /// <summary>   Handle the Plans view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        public async Task<IActionResult> Plans()
        {
            string url = AppSettings.ServiceUrls.Plans;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url + "about");

            // Throw an exception if not successful
            response.EnsureSuccessStatusCode();

            string body = await response.Content.ReadAsStringAsync();

            return this.View(JsonConvert.DeserializeObject<PlansViewModel>(body));
        }

        /// <summary>   Handle the About view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        public IActionResult About()
        {
            this.ViewData["Message"] = "Your application description page.";

            return this.View();
        }

        /// <summary>   Handle the Contact view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        public IActionResult Contact()
        {
            this.ViewData["Message"] = "Your contact page.";

            return this.View();
        }

        /// <summary>   Handle exceptions. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

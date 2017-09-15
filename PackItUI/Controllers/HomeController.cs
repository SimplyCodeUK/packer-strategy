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
        private HttpClient HttpClient;

        /// <summary>
        /// Initialises a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        ///
        /// <param name="appOptionsAccessor">   The app optins accessor. </param>
        public HomeController(IOptions<AppSettings> appSettings)
        {
            this.AppSettings = appSettings.Value;
            this.HttpClient = new HttpClient();
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
            HttpResponseMessage response = await this.HttpClient.GetAsync(AppSettings.ServiceUrls.Materials + "about");

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
            HttpResponseMessage response = await this.HttpClient.GetAsync(AppSettings.ServiceUrls.Packs + "about");

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
            HttpResponseMessage response = await this.HttpClient.GetAsync(AppSettings.ServiceUrls.Plans + "about");

            // Throw an exception if not successful
            response.EnsureSuccessStatusCode();

            string body = await response.Content.ReadAsStringAsync();

            return this.View(JsonConvert.DeserializeObject<PlansViewModel>(body));
        }

        /// <summary>   Handle the About view request. </summary>
        ///
        /// <returns> An IActionResult. </returns>
        public async Task<IActionResult> About()
        {
            AboutViewModel model = new AboutViewModel();

            string[] urls = new string[] { AppSettings.ServiceUrls.Materials, AppSettings.ServiceUrls.Packs, AppSettings.ServiceUrls.Plans };

            foreach (string url in urls)
            {
                HttpResponseMessage response = await this.HttpClient.GetAsync(AppSettings.ServiceUrls.Materials + "about");

                // Throw an exception if not successful
                response.EnsureSuccessStatusCode();

                string body = await response.Content.ReadAsStringAsync();

                model.Services.Add(JsonConvert.DeserializeObject<ServiceViewModel>(body));
            }

            return this.View(model);
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

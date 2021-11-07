// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItBdd.Steps
{
    using Microsoft.Extensions.Configuration;
    using NUnit.Framework;
    using PackItBdd.Drivers;
    using TechTalk.SpecFlow;

    /// <summary> Generic service steps </summary>
    [Binding]
    public sealed class ServicesStepDefinitions
    {
        /// <summary> The Scenario context. </summary>
        private readonly ScenarioContext scenarioContext;

        /// <summary> The configuration. Contains service endpoint urls. </summary>
        private readonly IConfiguration config;

        /// <summary> The driver for HTTP requests to a service. </summary>
        private readonly HttpHandler httpHandler;

        /// <summary> Constructor </summary>
        ///
        /// <param name="scenarioContext">The scenario context</param>
        /// <param name="config">Configuration settings</param>
        /// <param name="httpHandler">HTTP request handler</param>
        public ServicesStepDefinitions(ScenarioContext scenarioContext, IConfiguration config, HttpHandler httpHandler)
        {
            this.scenarioContext = scenarioContext;
            this.config = config;
            this.httpHandler = httpHandler;
        }

        /// <summary> Check the service is running </summary>
        ///
        /// <param name="serviceName">The name of the service to check</param>
        [Given("the (.*) service")]
        public void GivenTheServiceIsRunning(string serviceName)
        {
            var obj = this.config.GetSection("service");
            Assert.NotNull(obj);
            obj = obj.GetSection(serviceName);
            Assert.NotNull(obj);
            obj = obj.GetSection("url");
            Assert.NotNull(obj);
            this.httpHandler.ServiceName = serviceName;
        }

        /// <summary> Perform a HTTP Get to get the service version </summary>
        [When("we request the service version")]
        public void WhenWeRequestTheServiceVersion()
        {
            var obj = this.config.GetSection("service");
            obj = obj.GetSection(this.httpHandler.ServiceName);
            obj = obj.GetSection("url");
            this.httpHandler.Get(obj.Value);
        }

        /// <summary> Check that the HTTP status code from the last request is correct </summary>
        ///
        /// <param name="statusCode">The expected status code</param>
        [Then("we get the HTTP status code (.*)")]
        public void ThenWeGetTheHTTPStatusCode(int statusCode)
        {
            Assert.AreEqual(statusCode, this.httpHandler.ResponseStatusCode());
        }
    }
}

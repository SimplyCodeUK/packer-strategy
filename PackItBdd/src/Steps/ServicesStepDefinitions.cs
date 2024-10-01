// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItBdd.Steps
{
    using Microsoft.Extensions.Configuration;
    using Xunit;
    using PackItBdd.Drivers;
    using Reqnroll;

    /// <summary> Generic service steps </summary>
    /// <remarks> Constructor </remarks>
    ///
    /// <param name="config">Configuration settings</param>
    /// <param name="httpHandler">HTTP request handler</param>
    [Binding]
    public class ServicesStepDefinitions(IConfiguration config, HttpHandler httpHandler)
    {
        /// <summary> Check the service is running </summary>
        ///
        /// <param name="serviceName">The name of the service to check</param>
        [Given("the (.*) service")]
        public void TheServiceIsRunning(string serviceName)
        {
            var obj = config.GetSection("service");
            Assert.NotNull(obj);
            obj = obj.GetSection(serviceName);
            Assert.NotNull(obj);
            obj = obj.GetSection("url");
            Assert.NotNull(obj);
            httpHandler.ServiceName = serviceName;
        }

        /// <summary> Perform a HTTP Get to get the service version </summary>
        [When("we request the service version")]
        public void WeRequestTheServiceVersion()
        {
            var obj = config.GetSection("service");
            obj = obj.GetSection(httpHandler.ServiceName);
            obj = obj.GetSection("url");
            httpHandler.Get(obj.Value);
        }

        /// <summary> Check that the HTTP status code from the last request is correct </summary>
        ///
        /// <param name="statusCode">The expected status code</param>
        [Then("we get the HTTP status code (.*)")]
        public void WeGetTheHTTPStatusCode(int statusCode)
        {
            Assert.Equal(statusCode, httpHandler.ResponseStatusCode());
        }
    }
}

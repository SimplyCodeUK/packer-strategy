// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItBdd.Drivers
{
    using System.Net.Http;

    /// <summary> Handles client side HTTP requests </summary>
    /// <remarks> Constructor </remarks>
    ///
    /// <param name="clientHandler">The HttpClient Handler. @see HttpClient</param>
    public class HttpHandler(HttpClientHandler clientHandler)
    {
        /// <summary> Client for HTTP requests </summary>
        private readonly HttpClient client = new HttpClient(clientHandler);

        /// <summary> Response of the last HTTP requests </summary>
        private HttpResponseMessage response = null;

        /// <summary> The service name </summary>
        public string ServiceName { get; set; }

        /// <summary> Perform a HTTP Get </summary>
        ///
        /// <param name="endpoint">The endpoint to perform the Get on</param>
        public void Get(string endpoint)
        {
            var task = this.client.GetAsync(endpoint);
            task.Wait();
            this.response = task.Result;
        }

        /// <summary> Get the response status code of the last request </summary>
        ///
        /// <returns>The response status code of the last request</returns>
        public int ResponseStatusCode()
        {
            return (int)this.response.StatusCode;
        }
    }
}

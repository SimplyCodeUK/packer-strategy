// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItBdd.Drivers
{
    using System.Net.Http;

    /// <summary> Handles client side HTTP requests </summary>
    public class HttpHandler
    {
        /// <summary> Client for HTTP requests </summary>
        private readonly HttpClient client;

        /// <summary> Response of the last HTTP requests </summary>
        private HttpResponseMessage response;

        /// <summary> Constructor </summary>
        ///
        /// <param name="client">Http Client</param>
        public HttpHandler(HttpClient client = null)
        {
            this.client = client;
            if (this.client == null )
                this.client = new HttpClient();
            this.response = null;
        }

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

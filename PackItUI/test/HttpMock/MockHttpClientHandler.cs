﻿// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Test.HttpMock
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Mock of the default message handler used by System.Net.Http.HttpClient.
    /// </summary>
    public class MockHttpClientHandler : HttpClientHandler
    {
        /// <summary> Mocked requests. </summary>
        private readonly List<MockRequest> requests;

        /// <summary>
        /// Initialises a new instance of the <see cref="MockHttpClientHandler" /> class.
        /// </summary>
        public MockHttpClientHandler()
        {
            this.requests = new List<MockRequest>();
        }

        /// <summary>
        /// Add a request to be mocked.
        /// </summary>
        ///
        /// <param name="method"> The Http method. </param>
        /// <param name="requestUri"> The Uri used for the HTTP request. </param>
        ///
        /// <returns> The request. </returns>
        public MockRequest AddRequest(HttpMethod method, string requestUri)
        {
            MockRequest request = new MockRequest(method, requestUri);
            this.requests.Add(request);

            return request;
        }

        /// <summary>
        /// Creates an instance of System.Net.Http.HttpResponseMessage based on the information
        /// provided in the System.Net.Http.HttpRequestMessage as an operation that will
        /// not block.
        /// </summary>
        ///
        /// <param name="request"> The HTTP request message. </param>
        /// <param name="cancellationToken"> A cancellation token to cancel the operation. </param>
        ///
        /// <returns> The task object representing the asynchronous operation. </returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Search for the request
            foreach (MockRequest mock in this.requests)
            {
                if (mock.Method == request.Method && mock.RequestUri == request.RequestUri)
                {
                    // We can process it.
                    HttpResponseMessage responseMessage = new HttpResponseMessage
                    {
                        Content = mock.Content
                    };

                    if (mock.Limit > 0)
                    {
                        --mock.Limit;
                        if (mock.Limit == 1)
                        {
                            // delete it as it has been used for the last time
                            this.requests.Remove(mock);
                        }
                        else
                        {
                            --mock.Limit;
                        }
                    }

                    return Task.FromResult(responseMessage);
                }
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
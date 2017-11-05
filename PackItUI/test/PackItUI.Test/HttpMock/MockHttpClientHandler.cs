// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Test.HttpMock
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Mock of the default message handler used by System.Net.Http.HttpClient.
    /// </summary>
    public class MockHttpClientHandler : HttpClientHandler
    {
        /// <summary> Mocked requests. </summary>
        private List<Request> requests;

        /// <summary>
        /// Initialises a new instance of the <see cref="MockHttpClientHandler" /> class.
        /// </summary>
        public MockHttpClientHandler()
        {
            this.requests = new List<Request>();
        }

        /// <summary>
        /// Add a request to be mocked.
        /// </summary>
        ///
        /// <param name="method"> The Http method. </param>
        /// <param name="requestUri"> The Uri used for the HTTP request. </param>
        /// <param name="content"> The response content for the request. </param>
        /// <param name="mediaType"> The response media type for the request. </param>
        public void AddMock(HttpMethod method, string requestUri, string content, string mediaType)
        {
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, mediaType);
            this.requests.Add(new Request(method, requestUri, httpContent));
        }

        /// <summary>
        /// Add a request to be mocked where the media type is application/json
        /// </summary>
        ///
        /// <param name="method"> The Http method. </param>
        /// <param name="requestUri"> The Uri used for the HTTP request. </param>
        /// <param name="content"> The response content for the request. </param>
        public void AddMockJson(HttpMethod method, string requestUri, string content)
        {
            this.AddMock(method, requestUri, content, "application/json");
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
            foreach (Request mock in this.requests)
            {
                if (mock.Method == request.Method && mock.RequestUri == request.RequestUri)
                {
                    // We can process it.
                    HttpResponseMessage responseMessage = new HttpResponseMessage
                    {
                        Content = mock.Content
                    };

                    return Task.FromResult(responseMessage);
                }
            }

            return base.SendAsync(request, cancellationToken);
        }

        /// <summary> A mocked request. </summary>
        public class Request
        {
            /// <summary>
            /// Initialises a new instance of the <see cref="Request" /> class.
            /// </summary>
            ///
            /// <param name="method"> The Http method. </param>
            /// <param name="requestUri"> The Uri used for the HTTP request. </param>
            /// <param name="content"> The response content for the request. </param>
            public Request(HttpMethod method, string requestUri, HttpContent content) : this(method, new Uri(requestUri), content)
            {
            }

            /// <summary>
            /// Initialises a new instance of the <see cref="Request" /> class.
            /// </summary>
            ///
            /// <param name="method"> The Http method. </param>
            /// <param name="requestUri"> The Uri used for the HTTP request. </param>
            /// <param name="content"> The response content for the request. </param>
            public Request(HttpMethod method, Uri requestUri, HttpContent content)
            {
                this.Method = method;
                this.RequestUri = requestUri;
                this.Content = content;
            }

            /// <summary> Gets the Http method. </summary>
            public HttpMethod Method { get; }

            /// <summary> Gets the Uri used for the HTTP request. </summary>
            public Uri RequestUri { get; }

            /// <summary> Gets the response content. </summary>
            public HttpContent Content { get; }
        }
    }
}

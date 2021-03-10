// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItUI.Test.HttpMock
{
    using System;
    using System.Net.Http;
    using System.Text;

    /// <summary> A mocked request. </summary>
    public class MockRequest
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MockRequest" /> class.
        /// </summary>
        ///
        /// <param name="method"> The Http method. </param>
        /// <param name="requestUri"> The Uri used for the HTTP request. </param>
        public MockRequest(HttpMethod method, string requestUri) : this(method, new Uri(requestUri))
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="MockRequest" /> class.
        /// </summary>
        ///
        /// <param name="method"> The Http method. </param>
        /// <param name="requestUri"> The Uri used for the HTTP request. </param>
        public MockRequest(HttpMethod method, Uri requestUri)
        {
            this.Method = method;
            this.RequestUri = requestUri;
            this.Content = null;
            this.Limit = 0;
        }

        /// <summary> Gets the Http method. </summary>
        public HttpMethod Method { get; }

        /// <summary> Gets the Uri used for the HTTP request. </summary>
        public Uri RequestUri { get; }

        /// <summary> Gets the response content. </summary>
        public HttpContent Content { get; private set; }

        /// <summary> Gets or sets the limit on the number of times this request can be called. </summary>
        public int Limit { get; set; }

        /// <summary> Set the json contents of the request. </summary>
        ///
        /// <param name="content"> The content. </param>
        ///
        /// <returns> The updated request</returns>
        public MockRequest ContentsJson(string content)
        {
            return this.Contents(content, "application/json");
        }

        /// <summary> Set the contents of the request. </summary>
        ///
        /// <param name="content"> The content. </param>
        /// <param name="mediaType"> The media type of the contents. </param>
        ///
        /// <returns> The updated request</returns>
        public MockRequest Contents(string content, string mediaType)
        {
            this.Content = new StringContent(content, Encoding.UTF8, mediaType);

            return this;
        }
    }
}

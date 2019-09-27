// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    /// <summary> A program. </summary>
    public class Program
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="Program" /> class from being created.
        /// </summary>
        private Program()
        {
        }

        /// <summary> Main entry-point for this application. </summary>
        ///
        /// <param name="args"> An array of command-line argument strings. </param>
        public static void Main(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder<Startup>(args)
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    config.AddJsonFile("appsettings.local.json", optional: true);
                })
                .Build();

            host.Run();
        }
    }
}

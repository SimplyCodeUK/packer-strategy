// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItBdd.Hooks
{
    using Microsoft.Extensions.Configuration;
    using Reqnroll;
    using Reqnroll.BoDi;

    /// <summary> Configuration files setup hooks </summary>
    /// <remarks> Constructor </remarks>
    ///
    /// <param name="container">The object container</param>
    [Binding]
    internal class ConfigSetupHooks(IObjectContainer container)
    {
        /// <summary> The configuration. Read before the start of a sceanrio. </summary>
        private static IConfiguration _config;

        /// <summary> Setup configuration </summary>
        [BeforeScenario]
        public void SetupConfiguration()
        {
            _config ??= new ConfigurationBuilder()
                    .AddJsonFile("reqnroll.json", optional: false, reloadOnChange: true)
                    .AddJsonFile("reqnroll.local.json", optional: true, reloadOnChange: true)
                    .Build();

            container.RegisterInstanceAs(_config);
        }
    }
}

// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackItBdd.Hooks
{
    using BoDi;
    using Microsoft.Extensions.Configuration;
    using TechTalk.SpecFlow;

    /// <summary> Configuration files setup hooks </summary>
    /// <remarks> Constructor </remarks>
    ///
    /// <param name="container">The object container</param>
    [Binding]
    internal class ConfigSetupHooks(IObjectContainer container)
    {
        /// <summary> The configuration. Read before the start of a sceanrio. </summary>
        private static IConfiguration _config = null;

        /// <summary> Setup configuration </summary>
        [BeforeScenario]
        public void SetupConfiguration()
        {
            _config ??= new ConfigurationBuilder()
                    .AddJsonFile("specflow.json", optional: false, reloadOnChange: true)
                    .AddJsonFile("specflow.local.json", optional: true, reloadOnChange: true)
                    .Build();

            container.RegisterInstanceAs(_config);
        }
    }
}

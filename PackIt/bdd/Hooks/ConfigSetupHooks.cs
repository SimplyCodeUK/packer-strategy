// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PackIt.Bdd.Hooks
{
    using BoDi;
    using Microsoft.Extensions.Configuration;
    using TechTalk.SpecFlow;

    /// <summary> Configuration files setup hooks </summary>
    [Binding]
    class ConfigSetupHooks
    {
        /// <summary> The container for the configuration. </summary>
        private IObjectContainer container;

        /// <summary> The configuration. Read before the start of a sceanrio. </summary>
        private static IConfiguration _config;

        /// <summary> Constructor </summary>
        ///
        /// <param name="container">The object container</param>
        public ConfigSetupHooks(IObjectContainer container)
        {
            this.container = container;
        }

        /// <summary> Setup configuration </summary>
        [BeforeScenario]
        public void SetupConfiguration()
        {
            if (_config == null)
            {
                _config = new ConfigurationBuilder()
                    .AddJsonFile("specflow.json", optional: false, reloadOnChange: true)
                    .AddJsonFile("specflow.local.json", optional: true, reloadOnChange: true)
                    .Build();
            }

            this.container.RegisterInstanceAs<IConfiguration>(_config);
        }
    }
}

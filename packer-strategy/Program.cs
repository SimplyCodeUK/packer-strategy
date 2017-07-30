//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using System.IO;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace packer_strategy
{
    /*!
     * \class   Program
     *
     * \brief   A program.
     */
    public class Program
    {
        /*!
         * \fn  public static void Main(string[] args)
         *
         * \brief   Main entry-point for this application.
         *
         * \param   args    An array of command-line argument strings.
         */
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }

        /*!
         * \fn  private Program()
         *
         * \brief   class cannot be created Main is the public static entry point.
         */
        private Program()
        {
        }
    }
}

// <copyright company="Simply Code Ltd.">
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
// </copyright>
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using PackItDraw;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false)
                     .AddJsonFile("appsettings.local.json", optional: true);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();
startup.Configure(app, builder.Environment);
app.Run();

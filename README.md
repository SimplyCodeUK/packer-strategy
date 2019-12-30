﻿# PackIt

[![license](https://img.shields.io/github/license/mashape/apistatus.svg)](./LICENSE.md)
[![HitCount](http://hits.dwyl.io/SimplyCodeUK/packer-strategy.svg)](http://hits.dwyl.io/SimplyCodeUK/packer-strategy)

## Status

| Build                       | Result |
| :----                       | :----- |
| Azure Devops                | [![Visual Studio Badge](https://simplycodeuk.visualstudio.com/_apis/public/build/definitions/e0e00fa3-b395-4320-937a-56af7d655cc5/1/badge)](https://simplycodeuk.visualstudio.com/packer-strategy/_build/index?context=mine&path=%5C&definitionId=1&_a=completed) |
| Travis Linux                | [![Build Status](https://travis-ci.org/SimplyCodeUK/packer-strategy.png)](https://travis-ci.org/SimplyCodeUK/packer-strategy) |
| Codacy Quality              | [![Codacy Badge](https://api.codacy.com/project/badge/Grade/d7a5a9f269a744d38dcda165f328517a)](https://www.codacy.com/app/SimplyCodeUK/packer-strategy?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=SimplyCodeUK/packer-strategy&amp;utm_campaign=Badge_Grade) |
| Code Climate Quality        | [![Maintainability](https://api.codeclimate.com/v1/badges/429a3e46a3799c29b0b0/maintainability)](https://codeclimate.com/github/SimplyCodeUK/packer-strategy/maintainability) |
| Sonarcloud Status           | [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=SimplyCodeUK_packer-strategy&metric=alert_status)](https://sonarcloud.io/dashboard?id=SimplyCodeUK_packer-strategy) |

## Development environment

### Tools

- [GIT](https://git-scm.com/)
- [Node.Js Version 10.17.0](https://nodejs.org/)
- [Yarn Version 1.12.3](https://yarnpkg.com/)
- [.NET Core SDK 3.0.100](https://dotnet.microsoft.com/)
- [Visual Studio 2019 Version 16.4.2](https://www.visualstudio.com/)
  - Languages
    - C#
    - JavaScript
  - Extensions
    - StyleCop
    - Bundler & Minifier
    - Markdown Editor
- [Powershell Version 6.2.3](https://docs.microsoft.com/en-us/powershell/)
- [VirtualBox Version 6.0.12](https://www.virtualbox.org/)
- [Vagrant Version 2.2.5](https://www.vagrantup.com/)
  - Box ubuntu/bionic64 version 20191218.0.0

### Vagrant environment

To create the environemnt, from PowerShell run the command
```
vagrant up
```

Once it is created the environement can be updated with the command
```
vagrant provision
```

To tear down the environment use the command
```
vagrant destroy
```

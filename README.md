# PackIt

## Index

- [Github](#github)
- [Java Script Dependencies](#java-script-dependencies)
- [Status](#status)
- [Development Environment](#development-environment)
- [Vagrant](#vagrant)
- [Documentation](./docs/DOXYGENHOME.md)

<a name="github"></a>

## Github

[Project Location](https://github.com/SimplyCodeUK/packer-strategy)

[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](./LICENSE.md)

<a name="java-script-dependencies"></a>

## Java Script Dependencies

[![BabylonJS](https://img.shields.io/badge/BabylonJS-v4.2.0-green.svg)](https://www.jsdelivr.com/package/npm/babylonjs)

[![Bootstrap](https://img.shields.io/badge/Bootstrap-v5.0.1-green.svg)](https://www.jsdelivr.com/package/npm/bootstrap)

[![Font Awesome 5](https://img.shields.io/badge/FontAwesome-v5.4.1-green.svg)](https://www.jsdelivr.com/package/npm/font-awesome-5-css)

[![JQuery](https://img.shields.io/badge/JQuery-v3.6.0-green.svg)](https://www.jsdelivr.com/package/npm/jquery)

[![JQuery Validation](https://img.shields.io/badge/JQueryValidation-v1.19.3-green.svg)](https://www.jsdelivr.com/package/npm/jquery-validation)

[![JQuery Validation Unobtrusive](https://img.shields.io/badge/JQueryValidationUnobtrusive-v3.2.12-green.svg)](https://www.jsdelivr.com/package/npm/jquery-validation-unobtrusive)

[![@popperjs/core](https://img.shields.io/badge/@PopperJsCore-v2.9.2-green.svg)](https://www.jsdelivr.com/package/npm/@popperjs/core)

<a name="status"></a>

## Status

| Build                 | Result | Tests | Coverage |
| :----                 | :----- | :---- | :------- |
| Azure DevOps Build    | [![Azure DevOps Build](https://simplycodeuk.visualstudio.com/_apis/public/build/definitions/e0e00fa3-b395-4320-937a-56af7d655cc5/1/badge)](https://simplycodeuk.visualstudio.com/packer-strategy/_build/index?context=mine&path=%5C&definitionId=1&_a=completed) | ![Azure DevOps Tests](https://img.shields.io/azure-devops/tests/simplycodeuk/packer-strategy/1) | ![Azure DevOps Coverage](https://img.shields.io/azure-devops/coverage/simplycodeuk/packer-strategy/1) |
| Travis Linux          | [![Travis Linux](https://travis-ci.com/SimplyCodeUK/packer-strategy.svg)](https://travis-ci.com/SimplyCodeUK/packer-strategy) | | |
| Appveyor              | [![Appveyor](https://ci.appveyor.com/api/projects/status/h2ii287cd49liemf?svg=true)](https://ci.appveyor.com/project/louisnayegon/packer-strategy) | | |
| CircleCI              | [![CircleCI](https://circleci.com/gh/SimplyCodeUK/packer-strategy.svg?style=shield)](https://circleci.com/gh/SimplyCodeUK/packer-strategy) | |
| Codacy Quality        | [![Codacy Quality](https://api.codacy.com/project/badge/Grade/d7a5a9f269a744d38dcda165f328517a)](https://app.codacy.com/manual/SimplyCodeUK/packer-strategy/dashboard) | | ![Codacy Coverage](https://app.codacy.com/project/badge/Coverage/d7a5a9f269a744d38dcda165f328517a) |
| Code Climate Quality  | [![Code Climate Quality](https://api.codeclimate.com/v1/badges/429a3e46a3799c29b0b0/maintainability)](https://codeclimate.com/github/SimplyCodeUK/packer-strategy) | | ![Code Climate Coverage](https://img.shields.io/codeclimate/coverage/SimplyCodeUK/packer-strategy) |
| Sonarcloud Status     | [![Sonarcloud Status](https://sonarcloud.io/api/project_badges/measure?project=SimplyCodeUK_packer-strategy&metric=alert_status)](https://sonarcloud.io/dashboard?id=SimplyCodeUK_packer-strategy) | | ![Coverage](https://sonarcloud.io/api/project_badges/measure?project=SimplyCodeUK_packer-strategy&metric=coverage) |

<a name="development-environment"></a>

## Development Environment

- [GIT](https://git-scm.com/)
- [Node.Js Version 12.16.1](https://nodejs.org/)
- [Yarn Version 1.22.4](https://yarnpkg.com/)
- [.NET Core SDK 5.0.103](https://dotnet.microsoft.com/)
- [Visual Studio 2019 Version 16.10.3](https://www.visualstudio.com/)
  - Languages
    - C#
    - JavaScript
  - Extensions
    - StyleCop
    - Markdown Editor
- [Nuget Version 5.9.1](https://www.nuget.org/)
- [Powershell Version 7.1.3](https://docs.microsoft.com/en-us/powershell/)
- [VirtualBox Version 6.1.18](https://www.virtualbox.org/)
- [Vagrant Version 2.2.14](https://www.vagrantup.com/)
  - Box ubuntu/bionic64 version 20210224.0.0
- [Doxygen Version 1.9.1](https://www.doxygen.nl/)

<a name="vagrant"></a>

## Vagrant

<a name="one-time-initialisation"></a>

### One Time Initialisation

From an elevated Powershell run the following command a reboot may be required
afterwards

```cmd
vagrant plugin install vagrant-vbguest
(Invoke-WebRequest -Uri https://raw.githubusercontent.com/mitchellh/vagrant/master/keys/vagrant.pub -UseBasicParsing).Content > "$env:USERPROFILE\.ssh/authorized_keys"
ssh-keygen
Copy-Item $env:USERPROFILE\.ssh\id_rsa $env:USERPROFILE\.vagrant.d\insecure_private_key -Force
bcdedit /set hypervisorlaunchtype off
Disable-WindowsOptionalFeature -Online -FeatureName Microsoft-Hyper-V
```

<a name="creating-the-environment"></a>

### Creating The Environment

From an elevated PowerShell run the command

```cmd
vagrant up
```

<a name="updating-the-environment"></a>

### Updating The Environement

Once it is created the environment can be updated.
From an elevated PowerShell run the command

```cmd
vagrant provision
```

<a name="destroying-the-environment"></a>

### Destroying The Environment

From an elevated PowerShell run the command

```cmd
vagrant destroy
```

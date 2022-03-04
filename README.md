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

[![BabylonJS](https://img.shields.io/badge/BabylonJS-v5.0.0-green.svg)](https://www.jsdelivr.com/package/npm/babylonjs)

[![Bootstrap](https://img.shields.io/badge/Bootstrap-v5.1.3-green.svg)](https://www.jsdelivr.com/package/npm/bootstrap)

[![Font Awesome 5](https://img.shields.io/badge/FontAwesome-v5.4.1-green.svg)](https://www.jsdelivr.com/package/npm/font-awesome-5-css)

[![JQuery](https://img.shields.io/badge/JQuery-v3.6.0-green.svg)](https://www.jsdelivr.com/package/npm/jquery)

[![JQuery Validation](https://img.shields.io/badge/JQueryValidation-v1.19.3-green.svg)](https://www.jsdelivr.com/package/npm/jquery-validation)

[![JQuery Validation Unobtrusive](https://img.shields.io/badge/JQueryValidationUnobtrusive-v3.2.12-green.svg)](https://www.jsdelivr.com/package/npm/jquery-validation-unobtrusive)

[![@popperjs/core](https://img.shields.io/badge/@PopperJsCore-v2.11.2-green.svg)](https://www.jsdelivr.com/package/npm/@popperjs/core)

<a name="status"></a>

## Status

| Build                       | Result | Tests | Coverage |
| :----                       | :----- | :---- | :------- |
| Azure DevOps Build          | [![Azure DevOps Build](https://simplycodeuk.visualstudio.com/_apis/public/build/definitions/e0e00fa3-b395-4320-937a-56af7d655cc5/1/badge)](https://simplycodeuk.visualstudio.com/packer-strategy/_build/index?context=mine&path=%5C&definitionId=1&_a=completed) | [![Azure DevOps Tests](https://img.shields.io/azure-devops/tests/simplycodeuk/packer-strategy/1)](https://simplycodeuk.visualstudio.com/packer-strategy/_test/analytics?definitionId=1&contextType=build) | [![Azure DevOps Coverage](https://img.shields.io/azure-devops/coverage/simplycodeuk/packer-strategy/1)](https://simplycodeuk.visualstudio.com/packer-strategy/_build?definitionId=1&_a=summary) |
| Appveyor                    | [![Appveyor](https://ci.appveyor.com/api/projects/status/h2ii287cd49liemf?svg=true)](https://ci.appveyor.com/project/louisnayegon/packer-strategy) | | |
| Github Actions              | [![Github Actions](https://github.com/SimplyCodeUK/packer-strategy/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/SimplyCodeUK/packer-strategy/actions/workflows/build-and-test.yml) | | |
| Github Actions Code Quality | [![code-quality](https://github.com/SimplyCodeUK/packer-strategy/actions/workflows/code-quality.yml/badge.svg)](https://github.com/SimplyCodeUK/packer-strategy/actions/workflows/code-quality.yml) | | |
| Code Climate Quality        | [![Code Climate Quality](https://api.codeclimate.com/v1/badges/429a3e46a3799c29b0b0/maintainability)](https://codeclimate.com/github/SimplyCodeUK/packer-strategy) | | [![Code Climate Coverage](https://img.shields.io/codeclimate/coverage/SimplyCodeUK/packer-strategy)](https://codeclimate.com/github/SimplyCodeUK/packer-strategy) |
| Sonarcloud Status           | [![Sonarcloud Status](https://sonarcloud.io/api/project_badges/measure?project=SimplyCodeUK_packer-strategy&metric=alert_status)](https://sonarcloud.io/dashboard?id=SimplyCodeUK_packer-strategy) | | [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=SimplyCodeUK_packer-strategy&metric=coverage)](https://sonarcloud.io/dashboard?id=SimplyCodeUK_packer-strategy) |

<a name="development-environment"></a>

## Development Environment

- [GIT](https://git-scm.com/)
- [Node.Js Version 12.16.1](https://nodejs.org/)
- [Yarn Version 1.22.4](https://yarnpkg.com/)
- [.NET Core SDK 6.0.200](https://dotnet.microsoft.com/)
- [Visual Studio 2022 Version 17.1.0](https://www.visualstudio.com/)
  - Languages
    - C#
    - JavaScript
  - Extensions
    - StyleCop
    - Markdown Editor
- [Nuget Version 6.0.0](https://www.nuget.org/)
- [Powershell Version 7.2.0](https://docs.microsoft.com/en-us/powershell/)
- [VirtualBox Version 6.1.28](https://www.virtualbox.org/)
  - Oracle VM VirtualBox Extension Pack version 6.1.28
- [Vagrant Version 2.2.19](https://www.vagrantup.com/)
  - Box generic/ubuntu2004 version 3.5.2
- [Doxygen Version 1.9.2](https://www.doxygen.nl/)

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

### Updating The Environment

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

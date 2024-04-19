# PackIt

## Index

- [Acronyms](#acronyms)
- [Github](#github)
- [Status](#status)
- [Development Environment](#development-environment)
- [WSL Setup](#wsl-setup)
- [Documentation](./docs/DOXYGENHOME.md)

<a name="acronyms"></a>

## Acronyms

| Acronyn | Description                 |
| :------ | :----------                 |
| SDK     | Software Development Kit    |
| WSL     | Windows Subsystem for Linux |

<a name="github"></a>

## Github

[Project Location](https://github.com/SimplyCodeUK/packer-strategy)

[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](./LICENSE.md)

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
- [Visual Studio 2022 Version 17.9.6](https://www.visualstudio.com/)
  - Languages
    - C#
    - JavaScript
  - Extensions
    - StyleCop
    - Markdown Editor
    - SpecFlow for Visual Studio 2022
- [Powershell Version 7.2.0](https://docs.microsoft.com/en-us/powershell/)
- [VirtualBox Version 7.0.12](https://www.virtualbox.org/)
  - Oracle VM VirtualBox Extension Pack version 7.0.12r159484
- [Vagrant Version 2.4.0](https://www.vagrantup.com/)
  - Box generic/ubuntu2204 version 4.3.2
- [Doxygen Version 1.9.5](https://www.doxygen.nl/)


<a name="wsl-setup"></a>

## WSL Setup

```bash
sudo apt install g++ gdb make ninja-build rsync zip
sudo apt install doxygen
```

# PackIt

[![license](https://img.shields.io/github/license/mashape/apistatus.svg)](./LICENSE.md)
[![HitCount](http://hits.dwyl.io/SimplyCodeUK/packer-strategy.svg)](http://hits.dwyl.io/SimplyCodeUK/packer-strategy)

[![BabylonJS](https://img.shields.io/badge/BabylonJS-v4.2.0-green.svg)](https://www.jsdelivr.com/package/npm/babylonjs)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-v4.6.0-green.svg)](https://www.jsdelivr.com/package/npm/bootstrap)
[![Font Awesome 5](https://img.shields.io/badge/FontAwesome-v5.4.1-green.svg)](https://www.jsdelivr.com/package/npm/font-awesome-5-css)
[![JQuery](https://img.shields.io/badge/JQuery-v3.5.1-green.svg)](https://www.jsdelivr.com/package/npm/jquery)
[![JQuery Validation](https://img.shields.io/badge/JQueryValidation-v1.19.3-green.svg)](https://www.jsdelivr.com/package/npm/jquery-validation)
[![JQuery Validation Unobtrusive](https://img.shields.io/badge/JQueryValidationUnobtrusive-v3.2.12-green.svg)](https://www.jsdelivr.com/package/npm/jquery-validation-unobtrusive)
[![PopperJS](https://img.shields.io/badge/PopperJS-v1.16.1-green.svg)](https://www.jsdelivr.com/package/npm/popper.js)

## Index

- [Status](#status)
- [Development Environment](#development-environment)
- [Vagrant](#vagrant)

## Status

| Build                       | Result |
| :----                       | :----- |
| Azure Devops                | [![Visual Studio Badge](https://simplycodeuk.visualstudio.com/_apis/public/build/definitions/e0e00fa3-b395-4320-937a-56af7d655cc5/1/badge)](https://simplycodeuk.visualstudio.com/packer-strategy/_build/index?context=mine&path=%5C&definitionId=1&_a=completed) |
| Travis Linux                | [![Build Status](https://travis-ci.com/SimplyCodeUK/packer-strategy.svg)](https://travis-ci.com/SimplyCodeUK/packer-strategy) |
| Codacy Quality              | [![Codacy Badge](https://api.codacy.com/project/badge/Grade/d7a5a9f269a744d38dcda165f328517a)](https://www.codacy.com/app/SimplyCodeUK/packer-strategy?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=SimplyCodeUK/packer-strategy&amp;utm_campaign=Badge_Grade) |
| Code Climate Quality        | [![Maintainability](https://api.codeclimate.com/v1/badges/429a3e46a3799c29b0b0/maintainability)](https://codeclimate.com/github/SimplyCodeUK/packer-strategy/maintainability) |
| Sonarcloud Status           | [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=SimplyCodeUK_packer-strategy&metric=alert_status)](https://sonarcloud.io/dashboard?id=SimplyCodeUK_packer-strategy) |

## Development Environment

- [GIT](https://git-scm.com/)
- [Node.Js Version 12.16.1](https://nodejs.org/)
- [Yarn Version 1.22.4](https://yarnpkg.com/)
- [.NET Core SDK 5.0.103](https://dotnet.microsoft.com/)
- [Visual Studio 2019 Version 16.8.5](https://www.visualstudio.com/)
  - Languages
    - C#
    - JavaScript
  - Extensions
    - StyleCop
    - Markdown Editor
- [Powershell Version 7.0.0](https://docs.microsoft.com/en-us/powershell/)
- [VirtualBox Version 6.1.18](https://www.virtualbox.org/)
- [Vagrant Version 2.2.14](https://www.vagrantup.com/)
  - Box ubuntu/bionic64 version 20210224.0.0

## Vagrant

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

### Creating The Environment

From an elevated PowerShell run the command

```cmd
vagrant up
```

### Updating The Environement

Once it is created the environment can be updated.
From an elevated PowerShell run the command

```cmd
vagrant provision
```

### Destroying The Environment

From an elevated PowerShell run the command

```cmd
vagrant destroy
```

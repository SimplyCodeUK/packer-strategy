# PackIt

[![license](https://img.shields.io/github/license/mashape/apistatus.svg)](./LICENSE)
[![HitCount](http://hits.dwyl.io/SimplyCodeUK/packer-strategy.svg)](http://hits.dwyl.io/SimplyCodeUK/packer-strategy)

## Status

| Build                 | Result |
| :-------------------- | :----- |
| Travis Linux          | [![Build Status](https://travis-ci.org/SimplyCodeUK/packer-strategy.png)](https://travis-ci.org/SimplyCodeUK/packer-strategy) |
| Visual Studio Windows | [![Visual Studio Badge](https://simplycodeuk.visualstudio.com/_apis/public/build/definitions/e0e00fa3-b395-4320-937a-56af7d655cc5/1/badge)](https://simplycodeuk.visualstudio.com/packer-strategy/_build/index?context=mine&path=%5C&definitionId=1&_a=completed) |
| Appveyor Windows      | [![Build status](https://ci.appveyor.com/api/projects/status/jnv1j6y779o0r3ox?svg=true)](https://ci.appveyor.com/project/louisnayegon/packer-strategy) |
| Codacy Quality        | [![Codacy Badge](https://api.codacy.com/project/badge/Grade/d7a5a9f269a744d38dcda165f328517a)](https://www.codacy.com/app/SimplyCodeUK/packer-strategy?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=SimplyCodeUK/packer-strategy&amp;utm_campaign=Badge_Grade) |

## Development environment

### Tools

- [GIT](https://git-scm.com/)
- [Visual Studio 2017 Version 15.6.7](https://www.visualstudio.com/)
  - Languages
    - C#
    - JavaScript
  - Extensions
    - StyleCop
    - SonarLint
    - Bundler & Minifier
    - Markdown Editor
- [VirtualBox Version 5.2.8](https://www.virtualbox.org/)
- [Vagrant Version 2.0.4](https://www.vagrantup.com/)

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

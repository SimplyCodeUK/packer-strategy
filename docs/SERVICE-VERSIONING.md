# How to version a service

+ [Acronyms](#acronyms)
+ [Basics](#basics)
+ [Incompatible API changes](#incompatible-api-changes)
+ [API deprecation](#api-deprecation)
+ [API deletion](#api-deletion)

<a name="acronyms"></a>

## Acronyms

| Acronym | Description                   |
| :------ | :----------                   |
| API     | Application Program Interface |

<a name="basics"></a>

## Basics

The version of services will be following Semantic Versioning 2.0.0, details of
which can be found [here](<http://semver.org/spec/v2.0.0.html>)

Versions will take the format **MAJOR.MINOR.PATCH** These values will change under
the following conditions

+ **MAJOR** version when you make incompatible API changes
+ **MINOR** version when you add functionality in a backwards-compatible manner
+ **PATCH** version when you make backwards-compatible bug fixes

An update to the **MAJOR** version should see the **MINOR** and **PATCH** version reset to zero.

An update to the **MINOR** version should see the **PATCH** version reset to zero.

Services maintained in [Github](http://github.com/) should have a tagged release
for each version that follows this naming convention.

An additional part can be added to the version as a reference understandable to
the source code management system e.g.:

+ The subversion revision
+ The git short revision

<a name="incompatible-api-changes"></a>

## Incompatible API changes

API breaks include:

+ Removing an API
+ Changing the input to an API
+ Changing the output of an API
+ Changing the behaviour of an API

Prior to an incompatible API change, the following needs to be adhered to:

+ The forthcoming change must be communicated early
+ System tests to be updated
+ Optional migration tools must be supplied
+ The migration path must be documented

<a name="api-deprecation"></a>

## API deprecation

On deprecation an API will still be available and working, but unsupported,
with a documented migration path. This should produce a **MINOR** change.

<a name="api-deletion"></a>

## API deletion

Deletion of an an API follows a deprecation, users are expected to follow the
migration path documented in the deprecation migration path. This will produce
a **MAJOR** change.

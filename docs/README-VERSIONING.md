# Hub, Service, and API versioning

+ [Acronyms](#acronyms)
+ [Overview](#overview)
+ [Relationship between components](#relationship-between-components)
+ [Use Cases when updating the version of the Hub, API and Services](#use-cases-when-updating-the-version-of-the-hub-api-and-services)
+ [Version change examples](#version-change-examples)
  + [Defect fix on the plans service](#defect-fix-on-the-plans-service)
  + [Add functionality to the plans service](#add-functionality-to-the-plans-service)

<a name="acronyms"></a>

## Acronyms

| Acronym | Description                   |
| :------ | :----------                   |
| API     | Application Program Interface |

<a name="overview"></a>

## Overview

A PackIt Platform release consists of a specific versioned set of
services, which expose RESTful APIs. All RESTful APIs are also
versioned.

Release notes are published for each Hub release summarising the
release changes.

Versioning policies are important internally, to manage software
changes and ensure that correct component versions are deployed
together, and externally, to ensure compatibility for external
clients.

This document summarises the relevant versioning policies.

<a name="relationship-between-components"></a>

## Relationship between components

The following rules ensure consistent numbering of Hub, Service, and
API versions:

+ Hub version &mdash; the MAJOR version of the Hub __should__ match
  the API version of all Hub APIs

>
Example: for Hub version 2.3.4 __all__ APIs are at version v2

For Hub versioning policies, see [How to version a Hub](./hub-versioning.html)

+ Service version &mdash; the MAJOR, MINOR, and PATCH versions increment independently for each service

>
Example: for a given Hub version __all__ Services are versioned independently based on semantic versioning principles. __No__ Service MAJOR version may increment without incrementing the Hub MAJOR version

For Service versioning policies, see [How to version a Service](./service-versioning.html)

+ API version &mdash; the VERSION of __all__ APIs in a Hub release is the MAJOR version of the release

>
Example: if __any__ API is at v2 then __all__ APIs are at v2 and the Hub version is 2.x.x

For Service versioning policies, see [How to version an API](./api-versioning.html)

For more about release notes, see [Release Notes Process](./release-notes-process.html)

<a name="use-cases-when-updating-the-version-of-the-hub-api-and-services"></a>

## Use Cases when updating the version of the Hub, API and Services

| Use Case             | Service Version                   | Hub Version  | API Version |
| :--------            | :--------------                   | :----------  | :---------- |
| Defect Fix           | Patch change to services impacted | Patch change | No change   |
| Add functionality    | Minor change to services impacted | Minor change | No change   |
| Remove functionality | Major change to services impacted | Major change | Increment   |
| Change functionality | Major change to services impacted | Major change | Increment   |

<a name="version-change-examples"></a>

## Version change examples

<a name="defect-fix-on-the-plans-service"></a>

### Defect fix on the plans service

|             | Service Version     | Hub Version | API Version |
| :----       | :--------------     | :---------- | :---------- |
| Pre change  | Plans service 1.0.0 | 1.0.1       | v1          |
| Post change | Plans service 1.0.1 | 1.0.2       | v1          |

<a name="add-functionality-to-the-plans-service"></a>

### Add functionality to the plans service

|             | Service Version     | Hub Version | API Version |
| :----       | :--------------     | :---------- | :---------- |
| Pre change  | Plans service 1.0.1 | 1.0.2       | v1          |
| Post change | Plans service 1.1.0 | 1.1.0       | v1          |

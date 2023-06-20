A drop in library of common projects, providing the infrastructure to build applications using the Clean Architecture development principles. This library builds on the excellent work in Jason Taylor's **[CleanArchitecture](https://github.com/jasontaylordev/CleanArchitecture)** template and makes heavy use of Jimmy Bogard's brilliant [MediatR](https://github.com/jbogard/MediatR) library.

## Technologies

- .NET Core 7
- [Entity Framework Core 7](https://docs.microsoft.com/en-us/ef/core/) (optionally)
- [MediatR](https://github.com/jbogard/MediatR)
- [AutoMapper](https://automapper.org/)
- [FluentValidation](https://fluentvalidation.net/)

## Overview

The library consists of the following 3 projects:

### Domain

Simply contains a BaseEntity, BaseEvent and ValueObject class.

### Application

Provides the resources for implementing application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project.

### EntityFramework

This project contains the concrete classes for implementing data storage using Entity Framework.

## Samples

### [Todo Lists](https://github.com/cyclst/CleanArchitecture/tree/main/samples/TodoLists)

A small application providing basic todo list functionality using CQRS and Angular

## License

This project is licensed with the [MIT license](LICENSE).

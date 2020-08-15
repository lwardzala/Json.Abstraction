# Json.Abstraction

Abstraction conversion functionalities for the .NET System.Text.Json library.

![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Json.Abstraction)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](License)

### Includes:
- Support for deleted polymorphism in System.Text.Json;
- Automate searching in the abstract type's assembly for appropriate types convertion;
- Adding types convertion to a global registry;
- Converter for abstractions based on discriminator;
- Converter for interfaces;
- Converter factory;
- Synchronization with JsonSerializerOptions.

## Table of content

- [Json.Abstraction](#jsonabstraction)
    - [Includes:](#includes)
  - [Table of content](#table-of-content)
  - [Installation](#installation)
    - [NuGet](#nuget)
  - [Configuration](#configuration)
    - [JsonSerializerOptions setup](#jsonserializeroptions-setup)
    - [MVC AddJsonOptions](#mvc-addjsonoptions)
  - [Working with interface convertion](#working-with-interface-convertion)
  - [Working with abstraction convertion](#working-with-abstraction-convertion)
  - [Authors](#authors)

## Installation

### NuGet

https://www.nuget.org/packages/Json.Abstraction/

## Configuration

To initialize the abstraction converter factory, the only thing to configure is to add the JsonAbstractionConverter instancs to a System.Text.Json JsonSerializerOptions. Same as the JsonStringEnumConverter.

### JsonSerializerOptions setup

```csharp
using Json.Abstraction;

new JsonSerializerOptions
{
    IgnoreNullValues = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    Converters = { new JsonAbstractionConverter(), new JsonStringEnumConverter() }
};
```

### MVC AddJsonOptions

[when configuring services]
```csharp
services.AddControllers().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    opts.JsonSerializerOptions.IgnoreNullValues = true;
    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    opts.JsonSerializerOptions.Converters.Add(new JsonAbstractionConverter());
});
```

## Working with interface convertion

The interface converter tries to detect automatically an appropriate class.
If the interface and class exists in the same assembly, there is no need to register that types convertion.
Also there is no need to use discriminators if only there is only one class that implements the interface.
If there are more than one class, the converter factory tries to use AbstractionConverter with discriminators.

Anyway, there is an option to register missing interface convertion types by JsonAbstractSerializer:
```csharp
using Json.Abstraction.Serializers;

JsonAbstractSerializer.RegisterInterfaceConversion(typeof(IModel), typeof(Model));
// OR
JsonAbstractSerializer.RegisterInterfaceConversion<IModel, Model>();
```

## Working with abstraction convertion

The abstraction converter tries to detect automatically an appropriate class.
If the JSON data includes "_t" discriminator prameter with the class name, converter tries to locate possible class type by reflection.
However, when the abstraction and it's class is not located in the same assembly, it's possible to register that types convertion in the global registry.

```csharp
using Json.Abstraction.Serializers;

JsonAbstractSerializer.RegisterAbstractionConversion<ModelBase>(typeof(Model1), typeof(Model2));
// OR separately in different locations
JsonAbstractSerializer.RegisterAbstractionConversion<ModelBase>(typeof(Model1)); // In one file
JsonAbstractSerializer.RegisterAbstractionConversion<ModelBase>(typeof(Model2)); // In another file
```

Abstraction converter writes JSON structure with "_t" discriminator parameter based on class name.
So in case of model:

```csharp
public abstract class NestedBase
{
    public int Param1 { get; set; }
}

public class Nested : NestedBase
{
    public string Param2 { get; set; }
}

```

Serialization of object:

```csharp
new Nested
{
    Param1 = 3,
    Param2 = "Test"
}

// in resource:

public class TestResource
{
    public NestedBase NestedObject { get; set; }
}
```

is going to produce:

```json
{
    "nestedObject": {
        "_t": "Nested",
        "param2": "Test",
        "param1": 3
    }
}
```

Abstraction converter works both with abstract classes and interfaces.

## Authors
- Lukasz Wardzala - [github](https://github.com/lwardzala)
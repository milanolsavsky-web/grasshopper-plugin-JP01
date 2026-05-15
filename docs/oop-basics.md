# OOP basics for Grasshopper plugins

Object-oriented programming (OOP) is the style of programming that C# uses. You do not need to become an OOP expert to write Grasshopper components, but you need to understand a few concepts that come up constantly.

## Everything is a class

In C#, all code lives inside classes. You cannot write a loose function or a standalone variable floating in a file. Every piece of data and every piece of behavior belongs to a class. A class bundles together its data and the code that operates on that data. This bundling is called **encapsulation**: the class controls what is visible to the outside and what stays internal.

In Grasshopper plugin development, every component, every parameter, and every custom type you write is its own class:

```csharp
public class GH_AddNumbers : GH_Component
{
    // all data and behavior for this component lives here
}
```

## Fields

A field is a variable that belongs to a class. It stores data that the class needs to do its work. Fields are usually `private`, meaning only code inside the same class can access them:

```csharp
public class GH_ExampleType : IGH_Goo
{
    private string _value;    // this is a field
}
```

Private fields use the `_camelCase` naming convention (underscore + lowercase start). Other code accesses the data through properties instead of touching the field directly.

## Properties

A property looks like a field from the outside but runs a small piece of code when you read or write it. You will see them used for component metadata:

```csharp
public override GH_Exposure Exposure => GH_Exposure.primary;
public override Guid ComponentGuid => new Guid("...");
```

The `=>` syntax is a shorthand for "this property returns this value." Properties can also have a full getter and setter:

```csharp
public string Value
{
    get { return _value; }
    set { _value = value; }
}
```

The difference from a field: a property can validate data, compute values, or hide internal details. From the outside, using a property looks the same as using a field.

## Methods

A method is a function that belongs to a class. The most important method in a component is `SolveInstance`, which runs every time Grasshopper needs to compute results:

```csharp
protected override void SolveInstance(IGH_DataAccess DA)
{
    // your logic goes here
}
```

The keyword `override` means you are replacing the default behavior defined by the parent class with your own version.

## Inheritance (the `: base` part)

When you write `GH_AddNumbers : GH_Component`, you are saying "GH_AddNumbers is a type of GH_Component." Your class **inherits** everything that `GH_Component` already knows how to do (drawing itself on the canvas, managing wires, etc.), and you only need to fill in the parts specific to your component.

The `base(...)` call in the constructor passes information up to the parent class:

```csharp
public GH_AddNumbers()
    : base("Add Numbers", "Add", "Adds two numbers.", "PluginName", "Math")
{
}
```

This tells `GH_Component` the name, nickname, description, category, and subcategory.

You will see inheritance in three places:
- Components inherit from `GH_Component`
- Parameters inherit from `GH_PersistentParam<T>`
- Types implement `IGH_Goo` (an interface, see below)

## Interfaces

An interface is a contract. When a class implements an interface, it promises to provide certain methods and properties. `IGH_Goo` is the interface that all Grasshopper data types must implement:

```csharp
public class GH_ExampleType : IGH_Goo
{
    // must provide: IsValid, Duplicate, CastFrom, CastTo, Read, Write, etc.
}
```

You do not need to understand interfaces deeply. Just know that when you create a custom type, the compiler will tell you exactly which methods are missing.

## Access modifiers

- `public` - accessible from anywhere
- `private` - only accessible inside the same class
- `protected` - accessible inside the class and its subclasses

When you override methods from `GH_Component`, match the access modifier the parent class uses (`protected override` for `SolveInstance`, `public override` for `Exposure`, etc.).

## That is all you need

Grasshopper plugin development uses a small, repetitive subset of OOP. You will mostly:
1. Create classes that inherit from `GH_Component`
2. Add private fields to store data
3. Override a few methods (`RegisterInputParams`, `RegisterOutputParams`, `SolveInstance`)
4. Return values from properties (`Exposure`, `Icon`, `ComponentGuid`)

Read more at https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/

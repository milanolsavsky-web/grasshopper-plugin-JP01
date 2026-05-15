# C# basics

C# (pronounced "C sharp") is the programming language used for Grasshopper plugins. This page covers the minimum you need to get started.

## Variables

Variables store data. In C#, every variable has a type:

```csharp
int count = 5;              // whole number
double length = 3.14;       // decimal number
string name = "hello";      // text
bool isValid = true;         // true or false
```

You must declare the type when creating a variable. This helps catch mistakes early.

## Arrays and lists

An array holds a fixed number of items. A list can grow:

```csharp
int[] numbers = new int[] { 1, 2, 3 };     // array (fixed size)
List<string> names = new List<string>();     // list (can add/remove)
names.Add("Alice");
names.Add("Bob");
```

## If / else

```csharp
if (count > 10)
{
    // do something
}
else if (count > 5)
{
    // do something else
}
else
{
    // default case
}
```

Always use curly braces `{}`, even for single-line bodies.

## For and foreach loops

```csharp
// when you need the index
for (int i = 0; i < names.Count; i++)
{
    string name = names[i];
}

// when you just need each item
foreach (string name in names)
{
    // use name
}
```

## Methods (functions)

A method is a reusable block of code:

```csharp
private double Add(double a, double b)
{
    return a + b;
}
```

- `private` - who can call it (only this class)
- `double` - what it returns
- `Add` - the name
- `(double a, double b)` - the inputs (parameters)

## Null

`null` means "no value." It is a common source of errors. Always check for null when you receive data from Grasshopper inputs:

```csharp
string text = null;
if (!DA.GetData(0, ref text))
{
    return;  // input not connected, stop here
}
if (text == null)
{
    AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "Input is null.");
    return;
}
```

## ref and out

Some Grasshopper methods use `ref` (the variable must already have a value) and `out` (the method assigns a value to the variable):

```csharp
string text = null;
DA.GetData(0, ref text);    // ref: text is passed by reference

if (source.CastTo<string>(out string result))  // out: result is assigned inside CastTo
{
    // use result
}
```

You do not need to understand the details - just follow the patterns in the example files.

## Using statements

`using` at the top of a file imports other code libraries:

```csharp
using System;
using Grasshopper.Kernel;
```

Without these, you would have to write `System.Guid` and `Grasshopper.Kernel.GH_Component` everywhere.

## Namespaces

A namespace groups related classes. Your plugin uses `PluginName` as the root namespace:

```csharp
namespace PluginName.Components
{
    public class GH_ExampleComponent : GH_Component
    {
        // ...
    }
}
```

Namespaces match the folder structure: `Components/` folder uses `PluginName.Components` namespace.

## Common Grasshopper types you will use

| C# type | What it is | GH parameter method |
|---------|-----------|-------------------|
| `int` | Whole number | `AddIntegerParameter` |
| `double` | Decimal number | `AddNumberParameter` |
| `string` | Text | `AddTextParameter` |
| `bool` | True/false | `AddBooleanParameter` |
| `Point3d` | 3D point | `AddPointParameter` |
| `Curve` | Any curve | `AddCurveParameter` |
| `Mesh` | Triangle mesh | `AddMeshParameter` |
| `Brep` | Surface/solid | `AddBrepParameter` |

Read more at https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/

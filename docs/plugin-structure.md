# Plugin structure

This page explains what each file in the project does and how to add new things.

## File overview

```
src/
  PluginName.csproj           Project configuration (dependencies, build settings)
  PluginNameInfo.cs           Plugin identity (name, author, version, GUID)
  AssemblyInfo.cs             Assembly-level attributes (loading behavior)
  Tools/
    IconGenerator.cs          Generates 24x24 icons at runtime from component names
  Components/
    GH_ExampleComponent.cs    A component (the boxes on the Grasshopper canvas)
  Types/
    GH_ExampleType.cs         A custom data type (the data that flows through wires)
  Parameters/
    GH_ExampleParameter.cs    A parameter (lets users place your type on the canvas)
```

## What each file type does

### PluginNameInfo.cs

This file tells Grasshopper about your plugin: its name, version, author, and a unique identifier (GUID). There is exactly one of these per plugin. You edit it once when you start a new plugin and rarely touch it again.

### Components (the most common file you will create)

A component is a box on the Grasshopper canvas. It has:
- **Inputs** on the left (data coming in)
- **Outputs** on the right (data going out)
- **SolveInstance** method (your logic that transforms inputs into outputs)

The example component (`GH_ExampleComponent`) takes a text string as input and outputs it reversed.

### Types

A type defines a new kind of data that can flow through Grasshopper wires. You only need custom types when the built-in types (numbers, strings, points, curves, etc.) are not enough.

The example type (`GH_ExampleType`) wraps a simple string. A real plugin might wrap something more complex, like a custom mesh or a data structure.

### Parameters

A parameter lets users place your custom type directly on the canvas as a standalone input. Every custom type needs a matching parameter. If you only use built-in types, you do not need custom parameters.

### IconGenerator.cs

Generates simple 24x24 icons at runtime from component and parameter names. Components get a dark gray rectangle with white initials. Parameters get a dark hexagon with white initials. No image files needed.

## How to add a new component

1. Copy `src/Components/GH_ExampleComponent.cs`
2. Rename the file and the class (e.g. `GH_AddNumbers.cs` / `GH_AddNumbers`)
3. Update the constructor with your component's name, nickname, description, category, and subcategory:
   ```csharp
   public GH_AddNumbers()
       : base("Add Numbers", "Add", "Adds two numbers.", "PluginName", "Math") { }
   ```
4. Generate a new GUID. Use one of:
   - Terminal: `uuidgen` (Mac/Linux) or `[System.Guid]::NewGuid()` (PowerShell)
   - VS Code: `Ctrl+Shift+P` > "Insert GUID" (requires the "Insert GUID" extension)
5. Paste the GUID into `ComponentGuid`
6. Define inputs in `RegisterInputParams` and outputs in `RegisterOutputParams`
7. Write your logic in `SolveInstance`
8. Build and test

The **category** (e.g. "PluginName") determines which tab the component appears under in Grasshopper. The **subcategory** (e.g. "Math") determines the panel within that tab.

## How to add a new type + parameter pair

1. Copy `src/Types/GH_ExampleType.cs` and rename
2. Replace the internal data (the `_value` field) with your data
3. Update `CastFrom` to handle conversions from related types
4. Update `Read`/`Write` for saving/loading from Grasshopper files
5. Copy `src/Parameters/GH_ExampleParameter.cs` and rename
6. Change the generic type to your new type: `GH_PersistentParam<GH_YourType>`
7. Generate new GUIDs for both

## Icons

Icons are generated automatically by `IconGenerator.cs`. Each component and parameter gets a simple icon with its initials drawn on a dark background. You do not need to create any image files.

The icon is wired up in the `Icon` property:

```csharp
// For components:
protected override Bitmap Icon => IconGenerator.GenerateComponentIcon(Name);

// For parameters:
protected override Bitmap Icon => IconGenerator.GenerateParameterIcon(Name);
```

If you later want custom-designed icons, you can replace these calls with embedded PNG resources.

## GUIDs

Every component, parameter, and the plugin itself needs a globally unique identifier (GUID). This is how Grasshopper keeps track of which component is which, even if you rename it. Never reuse a GUID between different classes, and never type one by hand.

### How to generate a GUID

- **VS Code:** Install the "Insert GUID" extension, then `Ctrl+Shift+P` > "Insert GUID"
- **macOS/Linux terminal:** `uuidgen`
- **Windows PowerShell:** `[System.Guid]::NewGuid()`

### GUIDs you must replace

The template ships with placeholder GUIDs that must be replaced before you publish your plugin. Search for `// TODO: Replace this GUID` in the source code. Here is the full list:

| File | Property | Placeholder GUID |
|------|----------|-----------------|
| `PluginNameInfo.cs` | `Id` | `49841C68-DD5E-432F-908E-24705E32982C` |
| `GH_ExampleComponent.cs` | `ComponentGuid` | `96375036-9DA4-4B3C-846A-37BB60386E58` |
| `GH_ExampleParameter.cs` | `ComponentGuid` | `20BC4F06-BAC8-4CAD-B0DE-A5DE04BF2DC6` |

Generate a fresh GUID for each one. Do not copy GUIDs between classes.

Read more about the Grasshopper component anatomy at https://developer.rhino3d.com/guides/grasshopper/your-first-component-windows/

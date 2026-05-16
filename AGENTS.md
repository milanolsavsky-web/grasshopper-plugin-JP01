# PluginName - Codex Instructions

## Project structure

```
src/                    C# source code
  PluginName.csproj     Project file (NuGet refs, build targets)
  PluginNameInfo.cs     Plugin metadata (name, version, author, GUID)
  Tools/IconGenerator.cs  Generates 24x24 icons at runtime from component names
  Types/                Custom Grasshopper types (IGH_Goo implementations)
  Parameters/           Custom parameters (GH_PersistentParam<T> subclasses)
  Components/           Grasshopper components (GH_Component subclasses)
scripts/                Build scripts (.sh + .ps1 pairs)
build/                  Build output (gitignored)
docs/                   Developer documentation
```

## Build and verify

```bash
./scripts/build.sh --release        # Release build (macOS/Linux)
./scripts/build.sh --debug          # Debug build
./scripts/build.sh --clean --debug  # Clean + Debug
dotnet csharpier --check src/       # Verify formatting
```

On Windows use `scripts/build.ps1 -Release`, `-Debug`, `-Clean`.

After making changes, always run the build and the format check to confirm no regressions.

Never commit files under `build/` or `src/obj/`.

## C# style rules

This project targets .NET Framework 4.8 with C# 7.3. The following language features are NOT available and must not be used:
- File-scoped namespaces (`namespace Foo;`)
- Nullable reference types (`string?`)
- Target-typed `new()` 
- `using` declarations (`using var x = ...`)
- Default interface implementations

### Mandatory rules

- No `var` for primitive types. Write `int x = 0;` not `var x = 0;`. Use `var` only when the type is obvious from the right-hand side.
- Braces always required. Single-line `if`/`for`/`foreach` bodies still get braces.
- Block-scoped namespaces only. `namespace Foo { }`, never `namespace Foo;`.
- No LINQ. Use explicit `foreach` loops. Only `.ToArray()` and `.ToList()` are allowed.
- No single-use helpers. Do not extract a function unless it is called from at least two places.
- No default arguments. Every parameter must be passed explicitly at every call site.
- No XML doc comments (`///`). No boilerplate that restates the signature.
- No block comments. Always use `//` line comments, never `/* */`.
- Generate GUIDs with a tool (`uuidgen` or `[System.Guid]::NewGuid()`). Never invent one.
- Every file must start with a `//` comment block explaining what the file contains, why it exists, and whether it should be modified.

### Naming conventions

| What | Convention | Example |
|------|-----------|---------|
| Components | `GH_<WhatItDoes>` | `GH_ReverseText` |
| Parameters | `GH_<Type>Parameter` | `GH_ExampleParameter` |
| Types | `GH_<TypeName>` | `GH_ExampleType` |
| Private fields | `_camelCase` | `_value` |
| Constants | `PascalCase` | `MaxIterations` |
| Param indices | `InParam_<Name>`, `OutParam_<Name>` | `InParam_Text` |

### Component file order

1. Constructor
2. Exposure property
3. Icon property
4. ComponentGuid property
5. Input param index constants
6. RegisterInputParams
7. Output param index constants
8. RegisterOutputParams
9. SolveInstance
10. Private helpers (if any)

## Formatting

CSharpier is the formatter (config: `.csharpierrc.yaml`, print width 100). It runs automatically on commit via a pre-commit hook. To run manually:

```bash
dotnet csharpier src/        # Format all
dotnet csharpier --check src/ # Check without modifying
```

## Adding new components, types, and parameters

1. Copy the matching example file from `Types/`, `Parameters/`, or `Components/`.
2. Rename the class. Update the constructor arguments (name, nickname, description, category, subcategory).
3. Generate a new GUID and paste it into `ComponentGuid`.
4. For types: update the data fields, CastFrom, Read/Write.
5. For components: define inputs, outputs, and SolveInstance logic.

## Icons

Icons are generated at runtime by `IconGenerator` in `src/Tools/IconGenerator.cs`:
- `IconGenerator.GenerateComponentIcon(Name)` for components (dark gray rectangle + white initials)
- `IconGenerator.GenerateParameterIcon(Name)` for parameters (dark hexagon + white initials)

## Review checklist

When asked to review or fix the codebase, check all of the following:

1. Build: `dotnet build src/PluginName.csproj` must pass with 0 errors and 0 warnings.
2. Format: `dotnet csharpier --check src/` must pass.
3. GUIDs: every `ComponentGuid` property must return a unique GUID. Flag duplicates.
4. Naming: classes must follow the naming conventions above.
5. Style: flag `var` for primitives, missing braces, LINQ (except ToArray/ToList), file-scoped namespaces, `///` XML docs, `/* */` block comments.
6. Dead code: flag unused private members or fields.
7. Param indices: `InParam_` and `OutParam_` constants must match parameter registration order.

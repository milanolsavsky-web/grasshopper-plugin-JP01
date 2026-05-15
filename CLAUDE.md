# PluginName - Claude Code Instructions

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

## Build

```bash
./scripts/build.sh --release        # Release build (macOS/Linux)
./scripts/build.sh --debug          # Debug build
./scripts/build.sh --clean --debug  # Clean + Debug
```

On Windows use `scripts/build.ps1 -Release`, `-Debug`, `-Clean`.

Never commit files under `build/` or `src/obj/`.

## C# style rules

- **Target:** .NET 4.8, C# 7.3 (no file-scoped namespaces, no nullable reference types).
- **No `var` for primitives.** Write `int x = 0;` not `var x = 0;`.
- **Braces always required.** Single-line `if` bodies still get braces.
- **Block-scoped namespaces.** `namespace Foo { }`, not `namespace Foo;`.
- **No LINQ.** Use explicit `foreach` loops. Only `.ToArray()` and `.ToList()` are allowed.
- **No single-use helpers.** Do not extract a function unless it is called from at least two places.
- **No default arguments.** Every parameter must be passed explicitly at every call site.
- **No doc comments or boilerplate.** No `///` XML docs. No wrapper code that restates the signature.
- **No block comments.** Always use `//` line comments, never `/* */`.
- **Generate GUIDs with a tool.** Run `uuidgen` or `[System.Guid]::NewGuid()`. Never type one by hand.

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

CSharpier is the formatter. It runs automatically on commit via a git pre-commit hook (configured by MSBuild on first `dotnet restore`). To run manually:

```bash
dotnet csharpier src/        # Format all
dotnet csharpier --check src/ # Check without modifying
```

## Adding new components, types, and parameters

1. Copy the matching example file from `Types/`, `Parameters/`, or `Components/`.
2. Rename the class. Update the constructor arguments (name, nickname, description, category, subcategory).
3. Run `uuidgen` and paste the result into `ComponentGuid`.
4. For types: update the data fields, CastFrom, Read/Write.
5. For components: define inputs, outputs, and SolveInstance logic.

## Icons

Icons are generated at runtime by `IconGenerator` in `src/Tools/IconGenerator.cs`. It draws 24x24 bitmaps with the component name's initials on a dark background:
- `IconGenerator.GenerateComponentIcon(Name)` for components (dark gray rectangle)
- `IconGenerator.GenerateParameterIcon(Name)` for parameters (dark hexagon)

## Response style

One sentence of intent before the first tool call. Two-sentence summary at the end. No running commentary.

## Interaction rules

Zero sycophancy. If I am wrong, say so directly and explain why.

## /review skill

When the user types `/review`, perform all of the following checks on the current codebase and fix any issues found:

1. **Build check:** Run `dotnet build src/PluginName.csproj` and fix any errors or warnings.
2. **Format check:** Run `dotnet csharpier --check src/` and fix any formatting violations.
3. **GUID check:** Verify every `ComponentGuid` property returns a unique GUID. Flag duplicates.
4. **Naming check:** Verify classes follow the naming conventions (GH_ prefix for components/params/types, _camelCase private fields, PascalCase constants).
5. **Style check:** Flag any use of `var` for primitive types, missing braces, LINQ methods (except ToArray/ToList), file-scoped namespaces, XML doc comments, or `/* */` block comments.
6. **Dead code:** Flag unused private members or fields.
7. **Param index check:** Verify `InParam_` and `OutParam_` constants match the order of parameter registration.

Report a summary of what was found and fixed. If everything passes, say so.

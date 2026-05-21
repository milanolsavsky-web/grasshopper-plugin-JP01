# PluginName

A Grasshopper plugin template for Rhino 7+, targeting both Windows and macOS.

## What's inside

- A working Grasshopper plugin with example component, type, and parameter
- Build scripts for macOS (bash) and Windows (PowerShell)
- VS Code tasks for building, deploying, and debugging in Rhino 7/8
- Auto-formatting with CSharpier on every commit
- Runtime icon generator (no image files needed)

## Quick start

1. Install the required software (see [docs/software-requirements.md](docs/software-requirements.md))
2. Clone this repo and open it in VS Code
3. Build: press `Ctrl+Shift+B` (or `Cmd+Shift+B` on Mac)
4. Copy `build/PluginName.gha` to your Grasshopper libraries folder (see [docs/setting-up-rhino.md](docs/setting-up-rhino.md))
5. Open Rhino, start Grasshopper, find your component under the "PluginName" tab

## Documentation

| Topic | Link |
|-------|------|
| Software to install | [docs/software-requirements.md](docs/software-requirements.md) |
| Setting up Rhino | [docs/setting-up-rhino.md](docs/setting-up-rhino.md) |
| Plugin structure explained | [docs/plugin-structure.md](docs/plugin-structure.md) |
| VS Code tasks and debugging | [docs/vscode-tasks-and-debugging.md](docs/vscode-tasks-and-debugging.md) |
| OOP basics for GH plugins | [docs/oop-basics.md](docs/oop-basics.md) |
| C# basics | [docs/csharp-basics.md](docs/csharp-basics.md) |
| Git basics | [docs/git-basics.md](docs/git-basics.md) |
| Help and resources | [docs/resources-and-help.md](docs/resources-and-help.md) |

## Building

Choose the build mode based on your needs:

- **`--release` / `-Release`** – Optimized build with optimizations enabled. Use this for distribution and final testing.
- **`--debug` / `-Debug`** – Debug build with symbols, slower but includes debugging information. Use this while developing.
- **`--clean` / `-Clean`** – Clean build (removes old artifacts before building). Useful if you encounter strange build issues.

**macOS / Linux:**
```bash
./scripts/build.sh --release    # Optimized build
./scripts/build.sh --debug      # Debug build
./scripts/build.sh --clean --debug  # Clean + debug build
```

**Windows (PowerShell):**
```powershell
.\scripts\build.ps1 -Release    # Optimized build
.\scripts\build.ps1 -Debug      # Debug build
.\scripts\build.ps1 -Clean -Debug  # Clean + debug build
```

### Build output

The compiled plugin appears at `build/PluginName.gha`.

To use the plugin, copy it to your Grasshopper libraries folder:

- **Windows:** `%APPDATA%\Grasshopper\Libraries\` (or use Grasshopper's built-in plugin manager)
- **macOS:** `~/Library/Application\ Support/Grasshopper/Libraries/`

See [docs/setting-up-rhino.md](docs/setting-up-rhino.md) for more details.

### Building in VS Code

You can also build directly from VS Code by pressing `Ctrl+Shift+B` (Windows/Linux) or `Cmd+Shift+B` (Mac). This runs the build task configured in `.vscode/tasks.json`.

## License

MIT

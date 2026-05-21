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

asdifhdsoif
dsoifhdasoi
iosdfudoisa
dsaoifoias

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

**macOS / Linux:**
```bash
./scripts/build.sh --release
./scripts/build.sh --debug
```
sdlkjhadslkf
dsklfjadslf
sajdaskljflas


**Windows (PowerShell):**
```powershell
.\scripts\build.ps1 -Release
.\scripts\build.ps1 -Debug
```

The compiled plugin appears at `build/PluginName.gha`.

## License

MIT

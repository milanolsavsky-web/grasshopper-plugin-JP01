# VS Code tasks and debugging

## What are VS Code tasks?

Tasks are shortcuts for running terminal commands without leaving the editor. Instead of opening a terminal and typing `./scripts/build.sh --release`, you run a task and VS Code does it for you. Tasks are defined in `.vscode/tasks.json`.

### How to run a task

- Press `Ctrl+Shift+B` (or `Cmd+Shift+B` on Mac) to run the default build task
- Press `Ctrl+Shift+P` (or `Cmd+Shift+P`), type "Run Task", and pick from the list

### Available tasks

| Task | What it does |
|------|-------------|
| **build: Release** | Builds the plugin in Release mode (optimized, no debug info). This is the default build task triggered by `Ctrl+Shift+B`. |
| **build: Debug** | Builds the plugin in Debug mode (includes debug symbols so you can set breakpoints). Use this during development. |
| **build: Clean + Release** | Deletes all previous build output, then builds fresh in Release mode. Use when the build seems broken or outdated. |
| **format: Check (CSharpier)** | Checks whether all C# files are correctly formatted. Reports violations but does not change any files. |
| **format: Fix (CSharpier)** | Formats all C# files with CSharpier. This is what the pre-commit hook runs automatically, but you can also run it manually. |
| **clean: All artifacts** | Deletes the `build/` and `src/obj/` folders. Use to start completely fresh. |

## Debugging

Debugging lets you pause your plugin while it is running inside Rhino, inspect variable values, and step through your code line by line. This is far more effective than adding print statements to figure out what is happening.

### What is a breakpoint?

A breakpoint is a marker you place on a line of code. When Rhino reaches that line while running your plugin, it pauses and gives control back to VS Code. You can then:

- See the current value of every variable
- Step to the next line (`F10`)
- Step into a method call (`F11`)
- Continue running until the next breakpoint (`F5`)

### How to set a breakpoint

1. Open a C# file in VS Code
2. Click in the gutter (the narrow column to the left of the line numbers). A red dot appears.
3. That line is now a breakpoint. Click the red dot again to remove it.

You can set as many breakpoints as you want. A good first breakpoint is the first line inside `SolveInstance` in one of your components.

### Debug workflow step by step

1. Set a breakpoint (red dot) on the line you want to inspect
2. Open the Run and Debug sidebar (`Ctrl+Shift+D` or `Cmd+Shift+D`)
3. At the top, pick the launch configuration that matches your setup:
   - **Launch Rhino 7 (Windows)** or **Launch Rhino 7 (macOS)**
   - **Launch Rhino 8 (Windows)** or **Launch Rhino 8 (macOS)**
4. Press `F5` (or click the green play button)
5. VS Code builds the plugin in Debug mode, then launches Rhino
6. In Rhino, open Grasshopper and use your component
7. When execution hits your breakpoint, VS Code comes to the foreground with the line highlighted in yellow

### What you see when paused at a breakpoint

- **Variables panel** (left sidebar): shows all local variables and their current values
- **Watch panel**: add expressions you want to monitor (e.g. `text.Length`)
- **Call Stack panel**: shows which methods called the current method
- **Debug toolbar** (top): buttons for Continue (`F5`), Step Over (`F10`), Step Into (`F11`), Step Out (`Shift+F11`), and Stop (`Shift+F5`)

### Debug controls

| Key | Action | When to use |
|-----|--------|-------------|
| `F5` | Continue | Run until the next breakpoint or end of execution |
| `F10` | Step Over | Execute the current line and move to the next one |
| `F11` | Step Into | If the current line calls a method, jump inside that method |
| `Shift+F11` | Step Out | Finish the current method and return to the caller |
| `Shift+F5` | Stop | Stop debugging and close Rhino |

### Tips

- **Debug mode is slower** than Release mode. That is normal. The compiler includes extra information so the debugger can map the running code back to your source files.
- **Hover over variables** while paused. VS Code shows their current value in a tooltip.
- **Conditional breakpoints**: right-click a breakpoint and select "Edit Breakpoint" to add a condition. For example, `text == "hello"` makes the breakpoint trigger only when that condition is true. Useful when your component runs many times in a loop.
- **The Debug Console** (bottom panel) lets you type C# expressions while paused and see their result immediately.

### Launch configurations

The debug configurations are defined in `.vscode/launch.json`. Each one points to a Rhino executable on a specific OS. If Rhino is installed in a non-default location, update the `program` path in that file.

All configurations run the "build: Debug" task before launching, so your code is always up to date.

Read more at https://code.visualstudio.com/docs/editor/debugging

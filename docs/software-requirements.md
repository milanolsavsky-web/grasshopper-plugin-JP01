# Software requirements

Install the following software in the order listed below. The order matters because some tools integrate with each other during installation.

## 1. Rhino 7 or 8

Rhino is the 3D modeling application that runs Grasshopper. You need it to test your plugin.

- **Download:** https://www.rhino3d.com/download/
- A free 90-day trial is available. After that you need a license.
- Install on your main OS (Windows or macOS). Both are supported.

## 2. VS Code (Visual Studio Code)

VS Code is the code editor you will use to write C#. Install it **before** Git, so that Git can use VS Code as its default text editor.

- **Download:** https://code.visualstudio.com/
- During installation, check the option to "Add to PATH" if asked.
- After installing, open VS Code and install these extensions (click the Extensions icon in the sidebar, or press `Ctrl+Shift+X`):
  - **C# Dev Kit** (by Microsoft) - C# language support
  - **CSharpier** (by csharpier) - auto-formatting
  - **Insert GUID** (by Heath Stewart) - generate GUIDs with `Ctrl+Shift+P` > "Insert GUID"

## 3. Git

Git tracks changes to your code and lets you collaborate with others.

- **Download:** https://git-scm.com/downloads

### Installation settings (Windows)

The Git installer on Windows asks several questions. Here are the important ones:

- **Default editor:** choose "Visual Studio Code" (this is why we installed VS Code first)
- **Line ending conversions:** choose "Checkout as-is, commit Unix-style line endings". This ensures consistent line endings across Windows and macOS.
- Everything else can be left at the defaults.

### Installation (macOS)

Git comes pre-installed on macOS. You can verify by opening Terminal and typing:

```bash
git --version
```

If it is not installed, macOS will prompt you to install the Xcode Command Line Tools.

## 4. .NET SDK

The .NET SDK includes the C# compiler and build tools.

- **Download:** https://dotnet.microsoft.com/download
- Install the **.NET SDK** (not just the Runtime). Version 6.0 or later.
- The SDK builds your plugin targeting .NET Framework 4.8, which Rhino uses. You do not need to install .NET Framework 4.8 separately.

### Verify installation

Open a terminal (or PowerShell on Windows) and run:

```bash
dotnet --version
```

You should see a version number like `8.0.100` or similar.

## Summary

| Software | Purpose | Download |
|----------|---------|----------|
| Rhino 7/8 | Run and test your plugin | https://www.rhino3d.com/download/ |
| VS Code | Write code | https://code.visualstudio.com/ |
| Git | Track changes | https://git-scm.com/downloads |
| .NET SDK | Build the plugin | https://dotnet.microsoft.com/download |

# Setting up Rhino

Before you can test your plugin, you need to configure Grasshopper to load it from your build output folder. This page covers the developer setup first, then the deployment workflow for distributing your plugin to others.

## Developer setup (do this once)

Point Grasshopper at your `build/` folder so it loads your plugin directly from where the build script puts it. This way you never need to copy files around during development.

1. Open Rhino, then type `Grasshopper` in the command line to open Grasshopper
2. Go to **File > Preferences** (Windows) or **Grasshopper > Settings** (Mac)
3. In the left sidebar, find **Solver**
4. Under **Additional Component Folders**, click the folder icon and add the full path to your project's `build/` folder, for example:
   - Windows: `C:\Users\YourName\source\grasshopper-plugin\build`
   - macOS: `/Users/YourName/source/grasshopper-plugin/build`
5. Close the settings, close Grasshopper, close Rhino
6. Open Rhino and Grasshopper again

Your plugin should now appear under its tab name on the Grasshopper toolbar.

## Developer memory aid

Grasshopper shows useful diagnostic information. If you suspect your plugin is not loading:

1. In Grasshopper, go to **File > Special Folders > Components Folder** to see the default libraries folder
2. Go to **File > Special Folders > Settings Folder** to find GH config files
3. Check the Grasshopper loading splash screen for error messages about your plugin

## Reloading after changes

Grasshopper loads plugins once at startup. After rebuilding, you need to:

1. Close Grasshopper
2. Close Rhino
3. Open Rhino again
4. Open Grasshopper

There is no way to hot-reload a plugin without restarting.

## Deploying to other users

When you want to share your plugin with someone who is not a developer, they need to place the `.gha` file in the Grasshopper libraries folder.

### Where is the libraries folder?

**Windows:**
```
%APPDATA%\Grasshopper\Libraries\
```
Type this path in the Windows Explorer address bar to open it.

**macOS:**
```
~/Library/Application Support/McNeel/Rhinoceros/7.0/Plug-ins/Grasshopper/Libraries/
```
In Finder, press `Cmd+Shift+G` and paste the path.

A subfolder (e.g. `PluginName/`) inside `Libraries` works too. Grasshopper scans subfolders.

### Unblocking on Windows

Windows may block `.gha` files downloaded from the internet. If the plugin does not appear in Grasshopper:

1. Right-click the `.gha` file in File Explorer
2. Click Properties
3. At the bottom, check "Unblock" if that option appears
4. Click OK and restart Rhino

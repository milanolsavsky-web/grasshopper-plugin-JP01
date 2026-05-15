// Plugin identity. Grasshopper reads this class to learn the plugin's name,
// version, author, description, and unique ID. There is exactly one of these
// per plugin.
//
// Modify this file once when you start a new plugin: change the name,
// description, author, contact URL, and generate a new GUID for Id.

using System;
using System.Drawing;
using System.Reflection;
using Grasshopper.Kernel;

namespace PluginName
{
    public class PluginNameInfo : GH_AssemblyInfo
    {
        public override string Name => "PluginName";

        public override string Version
        {
            get
            {
                var attr = Assembly
                    .GetExecutingAssembly()
                    .GetCustomAttribute<AssemblyInformationalVersionAttribute>();
                return attr?.InformationalVersion ?? "0.0.0";
            }
        }

        public override string Description => "A Grasshopper plugin.";

        public override Bitmap Icon => null;

        public override string AuthorName => "Your Name";

        public override string AuthorContact => "https://example.com";

        // TODO: Replace this GUID with a new one. Run `uuidgen` (Mac/Linux)
        // or use the "Insert GUID" VS Code extension.
        public override Guid Id => new Guid("49841C68-DD5E-432F-908E-24705E32982C");
    }
}

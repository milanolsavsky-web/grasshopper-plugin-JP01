// Example custom Grasshopper parameter. Parameters define how custom types
// appear on the canvas: their name, icon, and which tab they belong to.
// Every custom type needs a matching parameter so users can place it on the
// canvas as a standalone input.
//
// Copy this file to create your own parameter. You will need to:
// 1. Rename the class.
// 2. Change the generic type argument to your custom type.
// 3. Update the constructor (name, nickname, description, category, subcategory).
// 4. Generate a new GUID (run `uuidgen` or use the "Insert GUID" VS Code extension).

using System;
using System.Collections.Generic;
using System.Drawing;
using Grasshopper.Kernel;
using PluginName.Types;

namespace PluginName.Parameters
{
    public class GH_ExampleParameter : GH_PersistentParam<GH_ExampleType>
    {
        public GH_ExampleParameter()
            : base("Example", "Ex", "An example custom parameter.", "PluginName", "Parameters") { }

        public override GH_Exposure Exposure => GH_Exposure.primary;

        protected override Bitmap Icon => IconGenerator.GenerateParameterIcon(Name);

        // TODO: Replace this GUID with a new one. Run `uuidgen` (Mac/Linux)
        // or use the "Insert GUID" VS Code extension.
        public override Guid ComponentGuid => new Guid("20BC4F06-BAC8-4CAD-B0DE-A5DE04BF2DC6");

        protected override GH_GetterResult Prompt_Plural(ref List<GH_ExampleType> values)
        {
            return GH_GetterResult.cancel;
        }

        protected override GH_GetterResult Prompt_Singular(ref GH_ExampleType value)
        {
            return GH_GetterResult.cancel;
        }
    }
}

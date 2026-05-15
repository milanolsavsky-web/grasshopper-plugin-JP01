// Example Grasshopper component. Components are the boxes on the canvas
// that process data. Each has inputs, outputs, and a SolveInstance method
// that runs the logic. This example reverses a text string.
//
// Copy this file to create your own component. You will need to:
// 1. Rename the class.
// 2. Update the constructor (name, nickname, description, category, subcategory).
// 3. Generate a new GUID (run `uuidgen` or use the "Insert GUID" VS Code extension).
// 4. Define your inputs in RegisterInputParams.
// 5. Define your outputs in RegisterOutputParams.
// 6. Write your logic in SolveInstance.

using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace PluginName.Components
{
    public class GH_ExampleComponent : GH_Component
    {
        public GH_ExampleComponent()
            : base(
                "Example Component",
                "ExComp",
                "An example component that reverses a string.",
                "PluginName",
                "Utilities"
            ) { }

        public override GH_Exposure Exposure => GH_Exposure.primary;

        protected override Bitmap Icon => IconGenerator.GenerateComponentIcon(Name);

        // TODO: Replace this GUID with a new one. Run `uuidgen` (Mac/Linux)
        // or use the "Insert GUID" VS Code extension.
        public override Guid ComponentGuid => new Guid("96375036-9DA4-4B3C-846A-37BB60386E58");

        private const int InParam_Text = 0;

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Text", "T", "Text to reverse.", GH_ParamAccess.item);
        }

        private const int OutParam_Result = 0;

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Result", "R", "Reversed text.", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string text = null;
            if (!DA.GetData(InParam_Text, ref text))
            {
                return;
            }
            if (text == null)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "Input text is null.");
                return;
            }

            char[] chars = text.ToCharArray();
            Array.Reverse(chars);
            string reversed = new string(chars);

            DA.SetData(OutParam_Result, reversed);
        }
    }
}

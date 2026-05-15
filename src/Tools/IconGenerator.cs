// Generates simple 24x24 component and parameter icons at runtime from the
// component name. Components get a dark gray rectangle with white initials,
// parameters get a dark hexagon. No image files needed.
//
// You should not need to modify this file unless you want to change the
// icon style (colors, shapes, font). If you later switch to hand-drawn
// icons, you can stop using this class and return embedded PNGs from the
// Icon property instead.

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PluginName
{
    internal static class IconGenerator
    {
        private const int IconSize = 24;

        private static readonly Color ComponentBackground = Color.FromArgb(64, 64, 64);
        private static readonly Color ParameterBackground = Color.FromArgb(8, 8, 8);
        private static readonly Color TextColor = Color.FromArgb(255, 255, 255);

        // Process-lifetime font shared by all generated icons. The GDI+ handle
        // is released when the plugin unloads.
        private static readonly Font IconFont = new Font("Arial", 6, FontStyle.Bold);

        private static string ExtractInitials(string name)
        {
            string[] words = name.Split(new[] { ' ', '_' }, StringSplitOptions.RemoveEmptyEntries);
            string initials = "";
            for (int i = 0, collected = 0; i < words.Length && collected < 6; i++)
            {
                char first = words[i][0];
                if (char.IsLetter(first) && char.IsUpper(first))
                {
                    initials += first;
                    collected++;
                }
            }
            return initials.ToUpper();
        }

        private static void DrawText(Graphics graphics, string name)
        {
            string text = ExtractInitials(name);
            using (var textBrush = new SolidBrush(TextColor))
            {
                using (
                    var format = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center,
                    }
                )
                {
                    graphics.DrawString(
                        text,
                        IconFont,
                        textBrush,
                        new RectangleF(0, 0, IconSize, IconSize),
                        format
                    );
                }
            }
        }

        // Generates a 24x24 icon with a dark gray rectangle and white initials.
        // Use this for components.
        public static Bitmap GenerateComponentIcon(string name)
        {
            var bitmap = new Bitmap(IconSize, IconSize);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                using (var brush = new SolidBrush(ComponentBackground))
                {
                    graphics.FillRectangle(brush, 0, 0, IconSize, IconSize);
                }

                DrawText(graphics, name);
            }

            return bitmap;
        }

        // Generates a 24x24 icon with a dark hexagon and white initials.
        // Use this for parameters.
        public static Bitmap GenerateParameterIcon(string name)
        {
            var bitmap = new Bitmap(IconSize, IconSize);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                using (var brush = new SolidBrush(ParameterBackground))
                {
                    using (var path = new GraphicsPath())
                    {
                        var points = new PointF[6];
                        float center = IconSize / 2f;
                        float radius = IconSize / 2f;

                        for (int i = 0; i < 6; i++)
                        {
                            double angle = Math.PI / 3 * i;
                            points[i] = new PointF(
                                center + radius * (float)Math.Cos(angle),
                                center + radius * (float)Math.Sin(angle)
                            );
                        }

                        path.AddPolygon(points);
                        graphics.FillPath(brush, path);
                    }
                }

                DrawText(graphics, name);
            }

            return bitmap;
        }
    }
}

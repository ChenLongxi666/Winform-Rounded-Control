using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace Octopus.Client
{
    public static class GraphicsExtensions
    {
        public static void DrawRoundedBorder(this Graphics g, Color color, Rectangle rec,
                                      int radius, int borderWidth, RoundedCorners corners)
        {
            using (Bitmap b = new Bitmap(rec.Width, rec.Height))
            using (Graphics gb = Graphics.FromImage(b))
            {
                var gfRec = new Rectangle(0, 0, rec.Width, rec.Height);
                gb.Clear(Color.Green);

                gb.DrawRoundedRectangle(color, gfRec, radius, corners);

                gfRec.Height -= borderWidth << 1;
                gfRec.Width -= borderWidth << 1;
                gfRec.X += borderWidth;
                gfRec.Y += borderWidth;
                gb.DrawRoundedRectangle(Color.Green, gfRec, radius - borderWidth, corners);

                var maskAttr = new ImageAttributes();
                maskAttr.SetColorKey(Color.Green, Color.Green);

                g.DrawImage(b, rec, 0, 0, b.Width, b.Height, GraphicsUnit.Pixel, maskAttr);
            }
        }

        public static void DrawRoundedRectangle(this Graphics g, Color color, Rectangle rec, int radius,
                                            RoundedCorners corners)
        {
            using (var b = new SolidBrush(color))
            {
                int x = rec.X;
                int y = rec.Y;
                int diameter = radius * 2;
                var horiz = new Rectangle(x, y + radius, rec.Width, rec.Height - diameter);
                var vert = new Rectangle(x + radius, y, rec.Width - diameter, rec.Height);

                g.FillRectangle(b, horiz);
                g.FillRectangle(b, vert);

                if ((corners & RoundedCorners.TopLeft) == RoundedCorners.TopLeft)
                    g.FillEllipse(b, x, y, diameter, diameter);
                else
                    g.FillRectangle(b, x, y, diameter, diameter);

                if ((corners & RoundedCorners.TopRight) == RoundedCorners.TopRight)
                    g.FillEllipse(b, x + rec.Width - (diameter + 1), y, diameter, diameter);
                else
                    g.FillRectangle(b, x + rec.Width - (diameter + 1), y, diameter, diameter);

                if ((corners & RoundedCorners.BottomLeft) == RoundedCorners.BottomLeft)
                    g.FillEllipse(b, x, y + rec.Height - (diameter + 1), diameter, diameter);
                else
                    g.FillRectangle(b, x, y + rec.Height - (diameter + 1), diameter, diameter);

                if ((corners & RoundedCorners.BottomRight) == RoundedCorners.BottomRight)
                    g.FillEllipse(b, x + rec.Width - (diameter + 1), y + rec.Height - (diameter + 1), diameter, diameter);
                else
                    g.FillRectangle(b, x + rec.Width - (diameter + 1), y + rec.Height - (diameter + 1), diameter,
                                    diameter);
            }
        }

        public enum RoundedCorners
        {
            None = 0x00,
            TopLeft = 0x02,
            TopRight = 0x04,
            BottomLeft = 0x08,
            BottomRight = 0x10,
            All = 0x1F
        }
    }
}

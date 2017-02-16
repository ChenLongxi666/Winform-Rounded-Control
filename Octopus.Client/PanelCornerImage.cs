using Octopus.Client.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Octopus.Client
{
    public partial class PanelCornerImage : Panel
    {
        public PanelCornerImage()
        {
            InitializeComponent();
        }

        public PanelCornerImage(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void PanelCornerImage_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(Resources.f2deb48f8c5494eeb02d6e542df5e0fe99257e6b, this.Width, this.Height);
            int w = this.Width / 2;
            int h = this.Height / 2;
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    if ((x - w) * (x - w) + (y - h) * (y - h) > w * h)
                    {
                        bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(0, 255, 255, 255));
                    }
                }
            }

            Graphics g = CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawImage(bitmap, new Point(0, 0));
            g.DrawEllipse(new Pen(Color.LightGray), 0, 0, this.Width, this.Height);
            g.Dispose();
        }
    }
}

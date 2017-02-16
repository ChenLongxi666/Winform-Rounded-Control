using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Octopus.Client
{
    public partial class CornerImage : PictureBox
    {
        private bool radius = true;
        public CornerImage()
        {
            InitializeComponent();
        }

        public CornerImage(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        [Browsable(true)]
        [DefaultValue(true)]
        [Description("设置圆角半径（0表示为矩形）")]
        public bool CornerRadius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
                base.Refresh();
            }
        }

        public void Round(System.Drawing.Region region)
        {
            GraphicsPath gPath = new GraphicsPath();
            int x = 2, y = 2;
            int thisWidth = this.Width;
            int thisHeight = this.Height;
            if (radius)
            {
                //var g=CreateGraphics();
                //var pen = new System.Drawing.Pen(System.Drawing.Color.Black,10);
                //g.SmoothingMode = SmoothingMode.AntiAlias;
                //g.DrawEllipse(pen,x,y,this.Width,this.Height);
                //g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                gPath.AddEllipse(x, y, this.Width - 3f, this.Height - 3f);
                gPath.Flatten();
                gPath.ClearMarkers();
                gPath.CloseAllFigures();
                Region = new System.Drawing.Region(gPath);
            }
            else
            {
                gPath.AddLine(x, y, thisWidth, y);
                gPath.AddLine(thisWidth, y, thisWidth, thisHeight);
                gPath.AddLine(thisWidth, thisHeight, x, thisHeight);
                gPath.AddLine(x, y, x, thisHeight);
                Region = new System.Drawing.Region(gPath);
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {

            Graphics g = pe.Graphics;
            g.Clear(Color.Violet);
            Round(this.Region);
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pe.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            pe.Graphics.CompositingQuality = CompositingQuality.GammaCorrected;
            pe.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            pe.Graphics.PageUnit = System.Drawing.GraphicsUnit.Display;
            // pe.Graphics.DrawRoundedBorder(System.Drawing.Color.Black, this.ClientRectangle, 50, 5, GraphicsExtensions.RoundedCorners.All);
            //pe.Graphics.Save();

            base.OnPaint(pe);
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            base.Refresh();
        }

    }
}

using Octopus.Client.Properties;
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
    public partial class PictureBoxCornerImage : PictureBox
    {
        private Image curImg = Resources.f2deb48f8c5494eeb02d6e542df5e0fe99257e6b;
        public PictureBoxCornerImage()
        {
            InitializeComponent();
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Invalidate();
            //cornerRadius = this.Width;
        }
        public PictureBoxCornerImage(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        [Browsable(false)]
        public new Image Image { get; set; }

        private Image curPic;
        [Browsable(true)]
        public Image CurPic
        {
            get
            {
                return curPic;
            }
            set
            {
                curPic = value;
            }
        }


        private int cornerRadius;
        [Browsable(true), Description("圆角半径( 0 表示普通的PictureBox)")]
        public int Radius
        {
            get
            {
                return cornerRadius;
            }
            set
            {
                int r = this.Width - (this.borderSize * 2);
                if (value <= 0)
                {
                    cornerRadius = 0;
                }
                else
                {
                    if (cornerRadius > r)
                    {
                        cornerRadius = r;
                    }
                    else
                    {
                        cornerRadius = value;
                    }

                }
                base.Refresh();
            }
        }
        private Color borderColor;
        [Browsable(true), Description("圆角边框的颜色")]
        public Color BorderColor
        {
            get
            {
                return this.borderColor;
            }
            set
            {
                this.borderColor = value;
                base.Refresh();
            }
        }

        private int borderSize;
        [Browsable(true), Description("圆角边框的大小")]
        public new int BorderSize
        {
            get
            {
                return borderSize;
            }
            set
            {
                int r = this.Width - (value * 2);
                if (value <= 0)
                {
                    borderSize = 0;
                }
                else
                {
                    if (cornerRadius > r)
                    {
                        cornerRadius = r;
                    }
                    borderSize = value;
                }
                base.Refresh();
            }
        }



        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            base.Refresh();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            curImg = curPic;
            if (cornerRadius > 0)
            {
                //if (curImg == null)
                //{
                //    if (this.Image == null)
                //    {
                //        curImg = Resources._579ab875Ne74415b4;
                //    }
                //    else
                //    {
                //        curImg = this.Image.Clone() as Image;
                //    }
                //}
                DrawRoundedRectangle(pe.Graphics, this.ClientRectangle, cornerRadius, new Pen(borderColor, borderSize), Color.Red);
            }
            base.OnPaint(pe);
        }

        private void DrawRoundedRectangle(Graphics gfx, Rectangle Bounds, int CornerRadius, Pen DrawPen, Color FillColor)
        {
            int strokeOffset = Convert.ToInt32(Math.Ceiling(DrawPen.Width));
            Bounds = Rectangle.Inflate(Bounds, -strokeOffset, -strokeOffset);
            gfx.SmoothingMode = SmoothingMode.AntiAlias;
            DrawPen.EndCap = DrawPen.StartCap = LineCap.Round;

            GraphicsPath gfxPath = new GraphicsPath();
            gfxPath.AddArc(Bounds.X, Bounds.Y, CornerRadius, CornerRadius, 180, 90);
            gfxPath.AddArc(Bounds.X + Bounds.Width - CornerRadius, Bounds.Y, CornerRadius, CornerRadius, 270, 90);
            gfxPath.AddArc(Bounds.X + Bounds.Width - CornerRadius, Bounds.Y + Bounds.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
            gfxPath.AddArc(Bounds.X, Bounds.Y + Bounds.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
            gfxPath.CloseAllFigures();

            //gfx.FillPath(new SolidBrush(FillColor), gfxPath);
            //gfx.DrawPath(DrawPen, gfxPath);

            gfx.FillPath(new TextureBrush(new Bitmap(curImg, Bounds.Width, Bounds.Height)), gfxPath);
            gfx.DrawPath(DrawPen, gfxPath);
            //var bitmap = DrawRoundedRectangle(Resources.f2deb48f8c5494eeb02d6e542df5e0fe99257e6b, Color.Red, 0, 0, this.Height, this.Width, 50);
            //Form2 frm = new Form2(bitmap);
            //frm.Show();

            //gfx.DrawImage(bitmap, 0, 0);

            //gfx.DrawEllipse(new Pen(Color.LightGray),0,0,this.Width,this.Height);


        }

        public Bitmap DrawRoundedRectangle(Bitmap Image, Color BoxColor, int XPosition, int YPosition,
            int Height, int Width, int CornerRadius)
        {
            Bitmap NewBitmap = new Bitmap(Image, Width, Height);

            using (Graphics NewGraphics = Graphics.FromImage(NewBitmap))
            {
                NewGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (Pen BoxPen = new Pen(BoxColor))
                {
                    using (GraphicsPath Path = new GraphicsPath())
                    {
                        Path.AddLine(XPosition + CornerRadius, YPosition, XPosition + Width - (CornerRadius * 2), YPosition);
                        Path.AddArc(XPosition + Width - (CornerRadius * 2), YPosition, CornerRadius * 2, CornerRadius * 2, 270, 90);
                        Path.AddLine(XPosition + Width, YPosition + CornerRadius, XPosition + Width, YPosition + Height - (CornerRadius * 2));
                        Path.AddArc(XPosition + Width - (CornerRadius * 2), YPosition + Height - (CornerRadius * 2), CornerRadius * 2, CornerRadius * 2, 0, 90);
                        Path.AddLine(XPosition + Width - (CornerRadius * 2), YPosition + Height, XPosition + CornerRadius, YPosition + Height);
                        Path.AddArc(XPosition, YPosition + Height - (CornerRadius * 2), CornerRadius * 2, CornerRadius * 2, 90, 90);
                        Path.AddLine(XPosition, YPosition + Height - (CornerRadius * 2), XPosition, YPosition + CornerRadius);
                        Path.AddArc(XPosition, YPosition, CornerRadius * 2, CornerRadius * 2, 180, 90);
                        Path.CloseFigure();
                        NewGraphics.DrawPath(BoxPen, Path);
                    }
                }
            }
            return NewBitmap;
        }

        private void PictureBoxCornerImage_Paint(object sender, PaintEventArgs e)
        {
            //curImg = null;
        }

        private void PictureBoxCornerImage_Click(object sender, EventArgs e)
        {

        }
    }
}

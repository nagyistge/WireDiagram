using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;

using WireDiagram.Core.Model;

namespace WireDiagram.CustomElements
{
    public class CustomElementLine : FrameworkElement
    {
        #region Dependency Properties

        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke",
            typeof(Brush),
            typeof(CustomElementLine),
            new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        public static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.Register("Thickness",
            typeof(double),
            typeof(CustomElementLine),
            new FrameworkPropertyMetadata(0.05 * 96.0 / 2.54, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        public static readonly DependencyProperty InvertedThicknessProperty =
            DependencyProperty.Register("InvertedThickness", 
            typeof(double), 
            typeof(CustomElementLine),
            new FrameworkPropertyMetadata(0.125 * 96.0 / 2.54, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        public static readonly DependencyProperty X1Property =
            DependencyProperty.Register("X1",
            typeof(double),
            typeof(CustomElementLine),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        public static readonly DependencyProperty Y1Property =
            DependencyProperty.Register("Y1",
            typeof(double),
            typeof(CustomElementLine),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        public static readonly DependencyProperty X2Property =
            DependencyProperty.Register("X2",
            typeof(double),
            typeof(CustomElementLine),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        public static readonly DependencyProperty Y2Property =
            DependencyProperty.Register("Y2",
            typeof(double),
            typeof(CustomElementLine),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        public static readonly DependencyProperty StartAlignmentProperty =
            DependencyProperty.Register("StartAlignment",
            typeof(PinAlignment),
            typeof(CustomElementLine),
            new FrameworkPropertyMetadata(PinAlignment.None, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        public static readonly DependencyProperty EndAlignmentProperty =
            DependencyProperty.Register("EndAlignment",
            typeof(PinAlignment),
            typeof(CustomElementLine),
            new FrameworkPropertyMetadata(PinAlignment.None, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        public static readonly DependencyProperty EnableLineBreakProperty =
            DependencyProperty.Register("EnableLineBreak", 
            typeof(bool),
            typeof(CustomElementLine),
            new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        public static readonly DependencyProperty IsStartInvertedProperty =
            DependencyProperty.Register("IsStartInverted", 
            typeof(bool),
            typeof(CustomElementLine),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        public static readonly DependencyProperty IsEndInvertedProperty =
            DependencyProperty.Register("IsEndInverted",
            typeof(bool),
            typeof(CustomElementLine),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        #endregion

        #region Properties

        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double Thickness
        {
            get { return (double)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double InvertedThickness
        {
            get { return (double)GetValue(InvertedThicknessProperty); }
            set { SetValue(InvertedThicknessProperty, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double X1
        {
            get { return (double)GetValue(X1Property); }
            set { SetValue(X1Property, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double Y1
        {
            get { return (double)GetValue(Y1Property); }
            set { SetValue(Y1Property, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double X2
        {
            get { return (double)GetValue(X2Property); }
            set { SetValue(X2Property, value); }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double Y2
        {
            get { return (double)GetValue(Y2Property); }
            set { SetValue(Y2Property, value); }
        }

        public PinAlignment StartAlignment
        {
            get { return (PinAlignment)GetValue(StartAlignmentProperty); }
            set { SetValue(StartAlignmentProperty, value); }
        }

        public PinAlignment EndAlignment
        {
            get { return (PinAlignment)GetValue(EndAlignmentProperty); }
            set { SetValue(EndAlignmentProperty, value); }
        }

        public bool EnableLineBreak
        {
            get { return (bool)GetValue(EnableLineBreakProperty); }
            set { SetValue(EnableLineBreakProperty, value); }
        }

        public bool IsStartInverted
        {
            get { return (bool)GetValue(IsStartInvertedProperty); }
            set { SetValue(IsStartInvertedProperty, value); }
        }

        public bool IsEndInverted
        {
            get { return (bool)GetValue(IsEndInvertedProperty); }
            set { SetValue(IsEndInvertedProperty, value); }
        }

        #endregion

        #region Contructor

        public CustomElementLine()
        {
            pen.StartLineCap = PenLineCap.Round;
            pen.EndLineCap = PenLineCap.Round;
        }

        #endregion

        #region Private Properties

        private Pen pen = new Pen();
        private Brush previousStroke = null;
        private double previousThickness = double.NaN;
        private double penHalfThickness = double.NaN;

        private Point A = new Point();
        private Point B = new Point();
        private Point C = new Point();
        private Point D = new Point();
        private double deltaX = double.NaN;
        private double deltaY = double.NaN;
        private bool isX = false;
        private bool isY = false;

        private double alpha = double.NaN;
        private double theta = double.NaN;
        private double zet = double.NaN;
        private double headHeightY = double.NaN;
        private double headHeightX = double.NaN;
        private Point startCenter = new Point();
        private Point endCenter = new Point();

        #endregion

        #region Overrides

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            var dc = drawingContext;

			if(this.pen == null)
			{
	            pen = new Pen(Stroke, Thickness)
	            {
	                StartLineCap = PenLineCap.Round,
	                EndLineCap = PenLineCap.Round
	            };

                previousStroke = Stroke;
                previousThickness = Thickness;

                penHalfThickness = pen.Thickness / 2.0;
			}
			else 
			{
				if(Stroke != previousStroke || Thickness != previousThickness)
				{
					previousStroke = Stroke;
					previousThickness = Thickness;
					pen.Brush = Stroke;
					pen.Thickness = Thickness;

                    penHalfThickness = pen.Thickness / 2.0;
				}
			}

            A.X = X1;
            A.Y = Y1;

            B.X = X2;
            B.Y = Y2;

            isX = (A.X == B.X);
            isY = (A.Y == B.Y);

            if (EnableLineBreak == true && isX == false && isY == false)
            {
                deltaX = B.X - A.X;
                deltaY = B.Y - A.Y;

                if ((StartAlignment == PinAlignment.Left || StartAlignment == PinAlignment.Right) &&
                    (EndAlignment == PinAlignment.Left || EndAlignment == PinAlignment.Right))
                {
                    C.X = A.X + deltaX / 2.0;
                    C.Y = A.Y;

                    D.X = A.X + deltaX / 2.0;
                    D.Y = B.Y;

                    DrawGuidedLine(dc, A, C, true, false);
                    DrawGuidedLine(dc, C, D, false, false);
                    DrawGuidedLine(dc, D, B, false, true);
                }
                else if ((StartAlignment == PinAlignment.Left || StartAlignment == PinAlignment.Right) &&
                         (EndAlignment == PinAlignment.Top || EndAlignment == PinAlignment.Bottom))
                {
                    C.X = A.X + deltaX;
                    C.Y = A.Y;

                    DrawGuidedLine(dc, A, C, true, false);
                    DrawGuidedLine(dc, C, B, false, true);
                }
                else if ((StartAlignment == PinAlignment.Top || StartAlignment == PinAlignment.Bottom) &&
                         (EndAlignment == PinAlignment.Top || EndAlignment == PinAlignment.Bottom))
                {
                    C.X = A.X;
                    C.Y = A.Y + deltaY / 2.0;

                    D.X = B.X;
                    D.Y = A.Y + deltaY / 2.0;

                    DrawGuidedLine(dc, A, C, true, false);
                    DrawGuidedLine(dc, C, D, false, false);
                    DrawGuidedLine(dc, D, B, false, true);
                }
                else if ((StartAlignment == PinAlignment.Top || StartAlignment == PinAlignment.Bottom) &&
                         (EndAlignment == PinAlignment.Left || EndAlignment == PinAlignment.Right))
                {
                    C.X = A.X;
                    C.Y = A.Y + deltaY;

                    DrawGuidedLine(dc, A, C, true, false);
                    DrawGuidedLine(dc, C, B, false, true);
                }
                else
                {
                    DrawGuidedLine(dc, A, B, true, true);
                }
            }
            else
            {
                DrawGuidedLine(dc, A, B, true, true);
            }
        }

        #endregion

        #region Methods

        private void DrawGuidedLine(DrawingContext dc, Point start, Point end, bool enableStartInverted, bool enableEndInverted)
        {
            // calculate inverted start & end ellipse coordinates
            if (enableStartInverted == true || enableEndInverted == true)
            {
                alpha = Math.Atan2(start.Y - end.Y, end.X - start.X);
                theta = Math.PI - alpha;
                zet = theta - Math.PI / 2;

                headHeightY = Math.Cos(zet) * (InvertedThickness + Thickness / 2);
                headHeightX = Math.Sin(zet) * (InvertedThickness + Thickness / 2);
            }

            // update & push line guidelines
            GuidelineSet g = new GuidelineSet(new double[] { 0.0, 0.0 }, new double[] { 0.0, 0.0 });
            g.GuidelinesX[0] = start.X + penHalfThickness;
            g.GuidelinesX[1] = end.X + penHalfThickness;
            g.GuidelinesY[0] = start.Y + penHalfThickness;
            g.GuidelinesY[1] = end.Y + penHalfThickness;
            dc.PushGuidelineSet(g);

            // draw inverted start ellipse
            if (enableStartInverted == true && IsStartInverted == true)
            {
                startCenter.X = start.X + headHeightX;
                startCenter.Y = start.Y - headHeightY;

                start.X += 2 * headHeightX;
                start.Y -= 2 * headHeightY;

                dc.DrawEllipse(null, pen, startCenter, InvertedThickness, InvertedThickness);
            }

            // draw inverted end ellipse
            if (enableEndInverted == true && IsEndInverted == true)
            {
                endCenter.X = end.X - headHeightX;
                endCenter.Y = end.Y + headHeightY;

                end.X -= 2 * headHeightX;
                end.Y += 2 * headHeightY; 

                dc.DrawEllipse(null, pen, endCenter, InvertedThickness, InvertedThickness);
            }

            // draw line
            dc.DrawLine(pen, start, end);

            // pop line guidelines
            dc.Pop();
        }

        #endregion
    }
}

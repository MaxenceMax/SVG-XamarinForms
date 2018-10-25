using System;
using System.IO;
using System.Reflection;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using SKSvg = SkiaSharp.Extended.Svg.SKSvg;

namespace SVG.Xamarin.Forms
{
    public class SvgIcon : Frame
    {
        private readonly SKCanvasView _canvasView = new SKCanvasView();
        public SvgIcon()
        {
            Padding = new Thickness(0);
            Content = _canvasView;
            _canvasView.PaintSurface += CanvasViewOnPaintSurface;
            BackgroundColor = Color.Transparent;
            HasShadow = false;
            BorderColor = Color.Transparent;
        }


        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            nameof(Source), typeof(string), typeof(SvgIcon), default(string), propertyChanged: RedrawCanvas);

        public string Source
        {
            get => (string)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        private static void RedrawCanvas(BindableObject bindable, object oldvalue, object newvalue)
        {
            SvgIcon svgIcon = bindable as SvgIcon;
            svgIcon?._canvasView.InvalidateSurface();
        }

        private void CanvasViewOnPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKCanvas canvas = args.Surface.Canvas;
            canvas.Clear();

            if (string.IsNullOrEmpty(Source))
                return;

            var assembly = typeof(Application).GetTypeInfo().Assembly;
            if (assembly != null)
            {
                using (Stream stream = assembly.GetManifestResourceStream(Source))
                {
                    SKSvg svg = new SKSvg();
                    svg.Load(stream);

                    SKImageInfo info = args.Info;
                    canvas.Translate(info.Width / 2f, info.Height / 2f);

                    SKRect bounds = svg.ViewBox;
                    float xRatio = info.Width / bounds.Width;
                    float yRatio = info.Height / bounds.Height;

                    float ratio = Math.Min(xRatio, yRatio);

                    canvas.Scale(ratio);
                    canvas.Translate(-bounds.MidX, -bounds.MidY);

                    canvas.DrawPicture(svg.Picture);
                }
            }
        }

    }

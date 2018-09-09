using System;
using System.Collections.Generic;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace CSharp_SkiaSharp
{
    public class Draw_Pen : ContentPage
    {
        private SKCanvasView skiaView;
        private Dictionary<long, SKPath> temporaryPaths = new Dictionary<long, SKPath>();
        private List<SKPath> paths = new List<SKPath>();

        public Draw_Pen()
        {
            var view = new StackLayout();
            view.HorizontalOptions = LayoutOptions.FillAndExpand;
            view.VerticalOptions = LayoutOptions.FillAndExpand;

            skiaView = new SKCanvasView();
            skiaView.HorizontalOptions = LayoutOptions.FillAndExpand;
            skiaView.VerticalOptions = LayoutOptions.FillAndExpand;
            skiaView.PaintSurface += OnPaintSurface;
            skiaView.EnableTouchEvents = true;
            skiaView.Touch += OnTouch;

            view.Children.Add(skiaView);

            Content = view;
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;

            canvas.Clear(SKColors.White);

            var touchPathStroke = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Purple,
                StrokeWidth = 5
            };

            foreach (var touchPath in temporaryPaths)
                canvas.DrawPath(touchPath.Value, touchPathStroke);

            foreach (var touchPath in paths)
                canvas.DrawPath(touchPath, touchPathStroke);
        }

        private void OnTouch(object sender, SKTouchEventArgs e)
        {
            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    var p = new SKPath();
                    p.MoveTo(e.Location);
                    temporaryPaths[e.Id] = p;
                    break;
                case SKTouchAction.Moved:
                    if (e.InContact)
                        temporaryPaths[e.Id].LineTo(e.Location);
                    break;
                case SKTouchAction.Released:
                    paths.Add(temporaryPaths[e.Id]);
                    temporaryPaths.Remove(e.Id);
                    break;
                case SKTouchAction.Cancelled:
                    temporaryPaths.Remove(e.Id);
                    break;
            }

            e.Handled = true;

            ((SKCanvasView)sender).InvalidateSurface();
        }
    }
}


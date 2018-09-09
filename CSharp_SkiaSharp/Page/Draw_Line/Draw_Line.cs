using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace CSharp_SkiaSharp
{
    public class Draw_Line : ContentPage
    {
        private SKCanvasView skiaView;

        public Draw_Line()
        {
            var view = new StackLayout();
            view.HorizontalOptions = LayoutOptions.FillAndExpand;
            view.VerticalOptions = LayoutOptions.FillAndExpand;

            skiaView = new SKCanvasView();
            skiaView.HorizontalOptions = LayoutOptions.FillAndExpand;
            skiaView.VerticalOptions = LayoutOptions.FillAndExpand;
            skiaView.PaintSurface += OnPaintSurface;

            view.Children.Add(skiaView);

            Content = view;
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;

            canvas.Clear(SKColors.White);

            var pathStroke = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Green,
                StrokeWidth = 5
            };

            var path = new SKPath();
            path.MoveTo(160, 60);
            path.LineTo(240, 140);
            path.MoveTo(240, 60);
            path.LineTo(160, 140);

            // Рисуем путь
            canvas.DrawPath(path, pathStroke);
        }
    }
}


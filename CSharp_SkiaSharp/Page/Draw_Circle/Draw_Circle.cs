using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace CSharp_SkiaSharp
{
    public class Draw_Circle : ContentPage
    {
        private SKCanvasView skiaView;

        public Draw_Circle()
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

            var circleFill = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                Color = SKColors.Blue
            };

            var coor_circleFill = new SKPoint((float)skiaView.Width / 2, (float)skiaView.Height );

            canvas.DrawCircle(coor_circleFill, 40, circleFill);

            var circle_Fill_Border = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                Color = SKColors.Blue
            };

            var circleBorder = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Red,
                StrokeWidth = 5
            };

            canvas.DrawCircle(200, 200, 40, circle_Fill_Border);
            canvas.DrawCircle(200, 200, 40, circleBorder);
        }
    }
}


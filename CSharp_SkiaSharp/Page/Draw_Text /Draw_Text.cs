using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace CSharp_SkiaSharp
{
    public class Draw_Text : ContentPage
    {
        private SKCanvasView skiaView;

        public Draw_Text()
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

            var paint = new SKPaint
            {
                Color = SKColors.Black,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Right,
                TextSize = 24
            };

            var coord = new SKPoint((float)skiaView.Width / 2, 
                                    ((float)skiaView.Height + paint.TextSize) / 2);

            canvas.DrawText("SkiaSharp", coord, paint);
        }
    }
}


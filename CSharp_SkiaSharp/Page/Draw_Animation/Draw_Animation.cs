using System;
using System.Diagnostics;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

/*
 * Ссылка на репозиторий https://github.com/UdaraAlwis/XFSkiaSharpDemo
 */

namespace CSharp_SkiaSharp
{
    public class Draw_Animation : ContentPage
    {
        private SKCanvasView skiaView;

        Stopwatch stopwatch = new Stopwatch();
        bool pageIsActive;
        float t;
        const double cycleTime = 1000;

        public Draw_Animation()
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

        private void InitAnimation()
        {
            pageIsActive = true;
            stopwatch.Start();

            Device.StartTimer(TimeSpan.FromMilliseconds(33), () =>
            {
                t = (float)(stopwatch.Elapsed.TotalMilliseconds % cycleTime / cycleTime);
                skiaView.InvalidateSurface();

                if (!pageIsActive)
                {
                    stopwatch.Stop();
                }
                return pageIsActive;
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            InitAnimation();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            pageIsActive = false;
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo skImageInfo = e.Info;
            SKSurface skSurface = e.Surface;
            SKCanvas skCanvas = skSurface.Canvas;

            skCanvas.Clear(SKColors.White);

            var skCanvasWidth = skImageInfo.Width;
            var skCanvasheight = skImageInfo.Height;

            skCanvas.Translate((float)skCanvasWidth / 2, (float)skCanvasheight / 2);
            skCanvas.Scale(skCanvasWidth / 200f);

            float radius = 70 * t;

            using (SKPaint skPaint = new SKPaint())
            {
                skPaint.Style = SKPaintStyle.Stroke;
                skPaint.IsAntialias = true;
                skPaint.Color = SKColors.Black;
                skPaint.StrokeWidth = 10;

                skCanvas.DrawCircle(0, 0, radius, skPaint);
            }
        }
    }
}


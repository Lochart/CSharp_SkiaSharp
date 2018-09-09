using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

/*
 * Ссылка на репозиторий https://github.com/UdaraAlwis/XFSkiaSharpDemo
 */

namespace CSharp_SkiaSharp
{
    public class Draw_Tech_Talk : ContentPage
    {
        private SKCanvasView skiaView;

        public Draw_Tech_Talk()
        {
            var view = new StackLayout();
            view.HorizontalOptions = LayoutOptions.FillAndExpand;
            view.VerticalOptions = LayoutOptions.FillAndExpand;

            skiaView = new SKCanvasView();
            skiaView.HorizontalOptions = LayoutOptions.FillAndExpand;
            skiaView.VerticalOptions = LayoutOptions.FillAndExpand;
            skiaView.EnableTouchEvents = true;
            skiaView.PaintSurface += OnPaintSurface;
            skiaView.Touch += SkCanvasView_Touch;

            view.Children.Add(skiaView);

            Content = view;
        }

        private void SkCanvasView_Touch(object sender, SKTouchEventArgs e)
        {
            if (e.ActionType == SKTouchAction.Pressed)
            {
                _lastTouchPoint = e.Location;
                e.Handled = true;
            }

            _lastTouchPoint = e.Location;

            skiaView.InvalidateSurface();
        }

        private SKPoint _lastTouchPoint = new SKPoint();
        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo skImageInfo = e.Info;
            SKSurface skSurface = e.Surface;
            SKCanvas skCanvas = skSurface.Canvas;

            skCanvas.Clear();

            using (SKPaint paintTouchPoint = new SKPaint())
            {
                paintTouchPoint.Style = SKPaintStyle.Fill;
                paintTouchPoint.Color = SKColors.Red;
                paintTouchPoint.IsDither = true;
                skCanvas.DrawCircle(
                    _lastTouchPoint.X,
                    _lastTouchPoint.Y,
                    50, paintTouchPoint);

            }
        }
    }
}


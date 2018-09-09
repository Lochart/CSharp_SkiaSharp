using System;

using Xamarin.Forms;

namespace CSharp_SkiaSharp
{
    public static class Structure_Button 
    {
        public static Button Custom_Button(string text)
        {
            var button = new Button { 
                Text = text,
                TextColor = Color.White,
                BackgroundColor = Color.Black,
                BorderColor = Color.Red,
                CornerRadius = 25,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BorderWidth = 1
            };

            return button;
        }
    }
}


using System;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Collections.Generic;

namespace CSharp_SkiaSharp
{
    public class Main_Page : ContentPage
    {
        public Main_Page()
        {
            var view = new StackLayout();
            view.Padding = 20;
            view.Spacing = 10;

            var button_Draw_Text = Structure_Button.Custom_Button("Нарисованный текст");
            button_Draw_Text.Clicked += Clicked_Draw_Text;

            var button_Draw_Circle = Structure_Button.Custom_Button("Нарисованный круг");
            button_Draw_Circle.Clicked += Clicked_Draw_Circle;

            var button_Draw_Line = Structure_Button.Custom_Button("Нарисованный крест");
            button_Draw_Line.Clicked += Clicked_Draw_Line;

            var button_Draw_Pen = Structure_Button.Custom_Button("Рисование пером");
            button_Draw_Pen.Clicked += Clicked_Draw_Pen;

            var button_Draw_Tech_Talk = Structure_Button.Custom_Button("Движение");
            button_Draw_Tech_Talk.Clicked += Clicked_Draw_Tech_Talk;

            var button_Draw_Animate = Structure_Button.Custom_Button("Анимация");
            button_Draw_Animate.Clicked += Clicked_Draw_Animate;

            var button_Draw_Clock = Structure_Button.Custom_Button("Часы");
            button_Draw_Clock.Clicked += Clicked_Draw_Clock;

            var button_Draw_Connect_Four = Structure_Button.Custom_Button("Игра Connect Four");
            button_Draw_Connect_Four.Clicked += Clicked_Draw_Connect_Four;

            var button_Rect_Number = Structure_Button.Custom_Button("Четырехугольник");
            button_Rect_Number.Clicked += Clicked_Draw_Rectangle_Number;

            view.Children.Add(button_Draw_Text);
            view.Children.Add(button_Draw_Circle);
            view.Children.Add(button_Draw_Line);
            view.Children.Add(button_Draw_Pen);
            view.Children.Add(button_Draw_Tech_Talk);
            view.Children.Add(button_Draw_Animate);
            //view.Children.Add(button_Draw_Clock);
            view.Children.Add(button_Draw_Connect_Four);
            view.Children.Add(button_Rect_Number);

            var scroll = new ScrollView();
            scroll.Content = view;

            Content = scroll;
        }

        private void Clicked_Draw_Text(object sender, EventArgs e){
            var page = new Draw_Text();
            Navigation.PushAsync(page);
        }

        private void Clicked_Draw_Circle(object sender, EventArgs e)
        {
            var page = new Draw_Circle();
            Navigation.PushAsync(page);
        }

        private void Clicked_Draw_Line(object sender, EventArgs e)
        {
            var page = new Draw_Line();
            Navigation.PushAsync(page);
        }

        private void Clicked_Draw_Pen(object sender, EventArgs e)
        {
            var page = new Draw_Pen();
            Navigation.PushAsync(page);
        }

        private void Clicked_Draw_Tech_Talk(object sender, EventArgs e)
        {
            var page = new Draw_Tech_Talk();
            Navigation.PushAsync(page);
        }

        private void Clicked_Draw_Animate(object sender, EventArgs e)
        {
            var page = new Draw_Animation();
            Navigation.PushAsync(page);
        }

        private void Clicked_Draw_Clock(object sender, EventArgs e)
        {
            var page = new Draw_Clock();
            Navigation.PushAsync(page);
        }

        private void Clicked_Draw_Connect_Four(object sender, EventArgs e)
        {
            var page = new Game_Connect_Four();
            Navigation.PushAsync(page);
        }

        private void Clicked_Draw_Rectangle_Number(object sender, EventArgs e)
        {
            var page = new Draw_Rectangle_Number();
            Navigation.PushAsync(page);
        }
    }
}


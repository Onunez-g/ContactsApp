using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactsApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InitialsView : ContentView
    {
        const float TextOffSet = 1.25f;
        public InitialsView()
        {
            InitializeComponent();
        }
        void OnPainting(object sender, SKPaintSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;
            canvas.Clear();

            var circleFill = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                Color = SKColor.Parse(Color ?? "#EB6858")
            };
            canvas.DrawCircle((float)Height, (float)Height, (float)Height, circleFill);
            var textPaint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                Color = SKColors.White,
                TextSize = (float)Height,
                TextAlign = SKTextAlign.Center,
                Typeface = SKTypeface.FromFamilyName(FontFamily)
            };
            string text = Text ?? "";
            canvas.DrawText(text, (float)Height, (float)Height * TextOffSet, textPaint);
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(InitialsView), "#EB6858", propertyChanged: (bindable, oldValue, newValue) =>
            {
                var view = (bindable as InitialsView);
                view.ForceLayout();
            });
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create("Color", typeof(string), typeof(InitialsView), string.Empty, propertyChanged: (bindable, oldValue, newValue) =>
            {
                var view = (bindable as InitialsView);
                view.ForceLayout();
            });
        public string Color
        {
            get { return (string)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value ?? "#EB6858"); }
        }
        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create("FontFamily", typeof(string), typeof(InitialsView), string.Empty, propertyChanged: (bindable, oldValue, newValue) =>
            {
                var view = (bindable as InitialsView);
                view.ForceLayout();
            });
        public string FontFamily
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
}
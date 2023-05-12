using SkiaSharp;
using System;
using System.Diagnostics;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace linguid.Views.ContentMainPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TimerPage : ContentPage
	{
        Timer timer;
        int sec = 0;
        public TimerPage ()
		{
			InitializeComponent ();
        }

        SKPath path = new SKPath ();
		float arcLength = 105;

        private SKPaint GetPaintColor(SKPaintStyle style, string hexColor, float strokeWidth = 0, SKStrokeCap cap = SKStrokeCap.Round, bool IsAntialias = true)
        {
            return new SKPaint
            {
                Style = style,
                StrokeWidth = strokeWidth,
                Color = string.IsNullOrWhiteSpace(hexColor) ? SKColors.White : SKColor.Parse(hexColor),
                StrokeCap = cap,
                IsAntialias = IsAntialias
            };
        }

        private void canvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            SKPaint strokePaint = GetPaintColor(SKPaintStyle.Stroke, null, 10, SKStrokeCap.Square);
            SKPaint dotPaint = GetPaintColor(SKPaintStyle.Fill, "#6C19FF");
            SKPaint scPaint = GetPaintColor(SKPaintStyle.Stroke, "#262626", 4, SKStrokeCap.Square);
            SKPaint bgPaint = GetPaintColor(SKPaintStyle.Fill, "#F1F3F4");

            canvas.Clear();

            SKRect arcRect = new SKRect(10, 10, info.Width - 10, info.Height - 10);
            SKRect bgRect = new SKRect(25, 25, info.Width - 25, info.Height - 25);
            canvas.DrawOval(bgRect, bgPaint);

            strokePaint.Shader = SKShader.CreateLinearGradient(
                               new SKPoint(arcRect.Left, arcRect.Top),
                               new SKPoint(arcRect.Top, arcRect.Right),
                               new SKColor[] { SKColor.Parse("#6C19FF"), SKColors.MediumVioletRed },
                               new float[] { 0, 1 },
                               SKShaderTileMode.Repeat) ;

            path.ArcTo(arcRect, 45, arcLength, true);
            canvas.DrawPath(path, strokePaint);

            canvas.Translate(info.Width / 2, info.Height / 2);
            canvas.Scale(info.Width / 200f);

            canvas.Save();
            canvas.RotateDegrees(240);
            canvas.DrawCircle(75, 42, 3, dotPaint); //00
            canvas.DrawCircle(85, 0, 3, dotPaint); //55
            canvas.DrawCircle(75, -42, 3, dotPaint); //50
            canvas.DrawCircle(-42, -74, 3, dotPaint); //45
            canvas.DrawCircle(0, -85, 3, dotPaint); //40
            canvas.DrawCircle(42, -74, 3, dotPaint); //35
            canvas.DrawCircle(-73, -41, 3, dotPaint); //30
            canvas.DrawCircle(-85, 0, 3, dotPaint); //25
            canvas.DrawCircle(-75, 45, 3, dotPaint); //20
            canvas.DrawCircle(-42, 74, 3, dotPaint); //15
            canvas.DrawCircle(0, 85, 3, dotPaint); //10
            canvas.DrawCircle(40, 75, 3, dotPaint);//05 

            canvas.Restore();

            canvas.Save();
            canvas.DrawLine(0, 10, 0, -80, scPaint);
            canvas.Restore();
            canvas.DrawCircle(0, 0, 5, dotPaint);

            secondsTxt.Text = sec.ToString();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            canvasView.IsVisible = false;
            canvasViewStart.IsVisible = true;
            Device.StartTimer(TimeSpan.FromSeconds(1 / 60f), () =>
            {
                canvasViewStart.InvalidateSurface();
                return true;
            });

            start.IsVisible = false;
            stop.IsVisible = true;
            pause.IsVisible = true;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            sec++;
            if (sec > 59) sec = 0; 
        }

        private void canvasViewStart_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            SKPaint strokePaint = GetPaintColor(SKPaintStyle.Stroke, null, 10, SKStrokeCap.Square);
            SKPaint dotPaint = GetPaintColor(SKPaintStyle.Fill, "#6C19FF");
            SKPaint scPaint = GetPaintColor(SKPaintStyle.Stroke, "#262626", 4, SKStrokeCap.Square);
            SKPaint bgPaint = GetPaintColor(SKPaintStyle.Fill, "#F1F3F4");

            canvas.Clear();

            SKRect arcRect = new SKRect(10, 10, info.Width - 10, info.Height - 10);
            SKRect bgRect = new SKRect(25, 25, info.Width - 25, info.Height - 25);
            canvas.DrawOval(bgRect, bgPaint);

            strokePaint.Shader = SKShader.CreateLinearGradient(
                               new SKPoint(arcRect.Left, arcRect.Top),
                               new SKPoint(arcRect.Top, arcRect.Right),
                               new SKColor[] { SKColor.Parse("#6C19FF"), SKColors.MediumVioletRed },
                               new float[] { 0, 1 },
                               SKShaderTileMode.Repeat);

            path.ArcTo(arcRect, 45, arcLength, true);
            canvas.DrawPath(path, strokePaint);

            canvas.Translate(info.Width / 2, info.Height / 2);
            canvas.Scale(info.Width / 200f);

            canvas.Save();
            canvas.RotateDegrees(240);
            canvas.DrawCircle(75, 42, 3, dotPaint); //00
            canvas.DrawCircle(85, 0, 3, dotPaint); //55
            canvas.DrawCircle(75, -42, 3, dotPaint); //50
            canvas.DrawCircle(-42, -74, 3, dotPaint); //45
            canvas.DrawCircle(0, -85, 3, dotPaint); //40
            canvas.DrawCircle(42, -74, 3, dotPaint); //35
            canvas.DrawCircle(-73, -41, 3, dotPaint); //30
            canvas.DrawCircle(-85, 0, 3, dotPaint); //25
            canvas.DrawCircle(-75, 45, 3, dotPaint); //20
            canvas.DrawCircle(-42, 74, 3, dotPaint); //15
            canvas.DrawCircle(0, 85, 3, dotPaint); //10
            canvas.DrawCircle(40, 75, 3, dotPaint);//05 

            canvas.Restore();

            canvas.Save();
            canvas.RotateDegrees(60 * sec / 10f);
            canvas.DrawLine(0, 10, 0, -80, scPaint);
            canvas.Restore();

            canvas.DrawCircle(0, 0, 5, dotPaint);
            secondsTxt.Text = sec.ToString();

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            timer.Dispose();
            canvasViewStart.IsVisible = false;
            canvasView.IsVisible = true;
            stop.IsVisible = false;
            pause.IsVisible = false;
            start.IsVisible = true;
            sec = 0;
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            timer.Stop();
            pause.IsVisible = false;
            start.IsVisible = true;
        }
    }
}
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

namespace BTG_Test.Views;

public partial class MainPage : ContentPage
{
    private double[] prices;

    public MainPage()
    {
        InitializeComponent();
        prices = GenerateBrownianMotion(sigma: 0.2, mean: 0.001, initialPrice: 100, numDays: 252);
        PlotBrownianMotion();
    }

    private void PlotBrownianMotion()
    {
        var skiaChart = new SKCanvasView();
        skiaChart.PaintSurface += OnPaintCanvas;
        //this.Padding = new Thickness(50);
        //skiaChart.Margin = new Thickness(50);
        Content = skiaChart;
    }

    private void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        canvas.Clear(SKColors.White);

        float width = e.Info.Width;
        float height = e.Info.Height;

        SKPaint paint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Black,
            StrokeWidth = 2
        };

        SKPath skPath = new SKPath();
        skPath.MoveTo(0, height - (float)prices[0]);

        for (int i = 1; i < prices.Length; i++)
        {
            float x = i * (float)(width / prices.Length);
            float y = height - (float)prices[i];
            skPath.LineTo(x, y);
        }

        canvas.DrawPath(skPath, paint);
    }

    public double[] GenerateBrownianMotion(double sigma, double mean, double initialPrice, int numDays)
    {
        Random rand = new();
        double[] prices = new double[numDays];
        prices[0] = initialPrice;

        for (int i = 1; i < numDays; i++)
        {
            double u1 = 1.0 - rand.NextDouble();
            double u2 = 1.0 - rand.NextDouble();
            double z = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);

            double retornoDiario = mean + sigma * z;

            prices[i] =  prices[i - 1] * Math.Exp(retornoDiario);
           
        }

        return prices;
    }
}




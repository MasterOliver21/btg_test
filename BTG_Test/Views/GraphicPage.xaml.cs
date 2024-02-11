using BTG_Test.Core.Models;
using BTG_Test.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using System;

namespace BTG_Test.Views;

public partial class GraphicPage : ContentPage
{
    private GraphicViewModel _graphicViewModel;
    public GraphicPage(GraphicViewModel graphicViewModel)
    {
        InitializeComponent();
        BindingContext = graphicViewModel;
        _graphicViewModel = graphicViewModel;
    }



    private void Numeric_Validation(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.NewTextValue))
        {
            if (!_graphicViewModel.IsNumeric(e.NewTextValue)) ((Entry)sender).Text = e.OldTextValue;
        }
    }

    private void ChartView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        if (!_graphicViewModel.GraphicList.Any())
            return;
        var surface = e.Surface;
        var canvas = surface.Canvas;
        canvas.Clear(SKColors.LightGray);

        var height = e.Info.Height;
        var width = e.Info.Width;

        foreach (var item in _graphicViewModel.GraphicList)
        {
            var graphLinePaint = new SKPaint { Color = item.Color, StrokeWidth = 1, };

            for (int i = 0; i < item.Values.Length; i++)
            {
                var fromPoint = new SKPoint(_graphicViewModel.XPoint(i, width, item.Values.Length), _graphicViewModel.YPoint(height, (float)item.Values[i], (float)item.Values.Max()));
                if (i != item.Values.Length - 1)
                {
                    var toPoint = new SKPoint(_graphicViewModel.XPoint(i + 1, width, item.Values.Length), _graphicViewModel.YPoint(height, (float)item.Values[i + 1], (float)item.Values.Max()));
                    canvas.DrawLine(fromPoint, toPoint, graphLinePaint);
                }
            }
        }

    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            _graphicViewModel.SimulationGraphic();
            UpdateGraphic();
        }
        catch (Exception ex)
        {
          Application.Current.MainPage.DisplayAlert("Aviso!", $"{ex.Message} não foi preenchido.", "Ok");
        }
    }

    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        _graphicViewModel.GraphicList.Remove((Graphic)e.SelectedItem);
        UpdateGraphic();
    }

    private void UpdateGraphic()
    {
        var sk = new SKCanvasView();
        sk.PaintSurface += ChartView_PaintSurface;
        chartView.Content = sk;
    }
}


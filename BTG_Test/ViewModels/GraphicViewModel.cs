using BTG_Test.Core.Models;
using SkiaSharp;
using SkiaSharp.Views.Maui.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BTG_Test.ViewModels
{
    public class GraphicViewModel : BaseViewModel
    {
        private double? _precoInicial;
        private double? _volatilidade;
        private double? _media;
        private int? _tempo;
        private ObservableCollection<Graphic> _graphicList = new();
        public SKCanvasView Skia;
        public ICommand SimulationCommand { get; private set; }

        public GraphicViewModel()
        {
            SimulationCommand = new Command(execute: () => SimulationGraphic());
        }

        #region Bindable Properties
        public ObservableCollection<Graphic> GraphicList { get => _graphicList; set { _graphicList = value; OnPropertyChanged(nameof(GraphicList)); } }
        public double? PrecoInicial { get => _precoInicial; set { _precoInicial = value; OnPropertyChanged(nameof(PrecoInicial)); } }
        public double? Volatilidade { get => _volatilidade; set { _volatilidade = value; OnPropertyChanged(nameof(Volatilidade)); } }
        public double? Media { get => _media; set { _media = value; OnPropertyChanged(nameof(Media)); } }
        public int? Tempo { get => _tempo; set { _tempo = value; OnPropertyChanged(nameof(Tempo)); } }
        #endregion



        #region Functions
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

                double retonoDiario = mean + sigma * z;

                prices[i] = prices[i - 1] * Math.Exp(retonoDiario);
            }

            return prices;
        }

        /// <summary>
        /// </summary>
        /// <param name="height"> altura maxima do espaço destinado para o grafico</param>
        /// <param name="valueToGo"> valor de acordo com a proporcao que devera chegar</param>
        /// <param name="maxValue">maior valor existente na lista</param>
        /// <param name="visu">proporcao de visualizacao</param>
        /// <returns></returns>
        public float YPoint(int height, float valueToGo, float maxValue)
        {
            var prop = height / maxValue;

            return height - (float)(prop * valueToGo);
        }

        /// <summary>
        /// </summary>
        /// <param name="valueToGo"></param>
        /// <param name="width"></param>
        /// <param name="totalItens"></param>
        /// <returns></returns>
        public float XPoint(float valueToGo, float width, int totalItens)
        {
            var prop = (width / totalItens) * 0.90f;

            return valueToGo * prop;
        }

        public bool IsNumeric(string text)
        {
            return double.TryParse(text, out _);
        }

        public void SimulationGraphic()
        {
            try
            {
                if (PrecoInicial is null)
                    throw new Exception("Preço Inicial");
                else if (Tempo is null)
                    throw new Exception("Tempo");
                else if (Volatilidade is null)
                    throw new Exception("Volatilidade");
                else if (Media is null)
                    throw new Exception("Média");

                var graphic = new Graphic
                {
                    PrecoInicial = _precoInicial.Value,
                    Media = _media.Value,
                    Volatilidade = _volatilidade.Value,
                    Tempo = _tempo.Value,
                    Values = GenerateBrownianMotion(sigma: _volatilidade.Value / 100, initialPrice: _precoInicial.Value, numDays: _tempo.Value, mean: _media.Value / 100),
                    Color = Color()
                };

                GraphicList.Add(graphic);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private SKColor Color()
        {
            Random random = new();
            var numbers = random.Next(1, 6);
            return numbers switch
            {
                1 => SKColors.Yellow,
                2 => SKColors.Green,
                3 => SKColors.Red,
                4 => SKColors.Purple,
                5 => SKColors.Tomato,
                _ => SKColors.Teal
            };
        }


        #endregion
    }
}

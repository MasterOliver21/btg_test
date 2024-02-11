using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BTG_Test.Core.Models
{
    public class Graphic : INotifyPropertyChanged
    {
        public double PrecoInicial { get; set; }
        public double Volatilidade { get; set; }
        public double Media { get; set; }
        public int Tempo { get; set; }
        public double[] Values { get; set; }

        public SKColor Color { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

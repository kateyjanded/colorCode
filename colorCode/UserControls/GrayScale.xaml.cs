using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace colorCode
{
    /// <summary>
    /// Interaction logic for GrayScale.xaml
    /// </summary>
    public partial class GrayScale : UserControl
    {
        public static readonly DependencyProperty ColorValueProperty;
        public static readonly DependencyProperty GrayProperty;
        public GrayScale()
        {
            InitializeComponent();
        }

        static GrayScale()
        {
            GrayProperty = DependencyProperty.Register("Brightness", typeof(int), typeof(GrayScale),
               new FrameworkPropertyMetadata(0,(d,e)=>((GrayScale)d).OnGrayChanged(d,e)));
            ColorValueProperty = DependencyProperty.Register("ColorValue", typeof(SolidColorBrush),
               typeof(GrayScale), new FrameworkPropertyMetadata(Brushes.Black,FrameworkPropertyMetadataOptions.AffectsRender, (d,e) =>((GrayScale)d).OnColorChanged(d,e)));
        }
        private void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SolidColorBrush newColor = (SolidColorBrush)e.NewValue;
         //   SolidColorBrush oldColor = (SolidColorBrush)e.OldValue;
            int red = 100-(newColor.Color.R*100/255);
            int blue = 100-(newColor.Color.B*100/255);
            int green = 100-(newColor.Color.G*100/255);
            
        }
        public int Gray
        {
            get { return (int)GetValue(GrayProperty); }
            set { SetValue(GrayProperty, value); }
        }
        public SolidColorBrush ColorValue
        {
            get { return (SolidColorBrush)GetValue(ColorValueProperty); }
            set { SetValue(ColorValueProperty, value); }
        }
        public static double gray;

        private void OnGrayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            gray = (int)e.NewValue;
            gray = (100-gray )/ 100 *255;
            ColorValue = new SolidColorBrush(Color.FromRgb((byte)gray, (byte)gray, (byte)gray));
        }
    }
}

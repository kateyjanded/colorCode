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
    /// Interaction logic for CMYKColorControl.xaml
    /// </summary>
    public partial class CMYKColorControl : UserControl
    {
        public static readonly DependencyProperty ColorValueProperty;
        public static readonly DependencyProperty CyanProperty;
        public static readonly DependencyProperty MagentaProperty;
        public static readonly DependencyProperty YellowProperty;
        public static readonly DependencyProperty KeyProperty;
        public CMYKColorControl()
        {
            InitializeComponent();
        }
        static CMYKColorControl()
        {
            ColorValueProperty = DependencyProperty.Register("ColorValue", typeof(SolidColorBrush),
                typeof(CMYKColorControl), new FrameworkPropertyMetadata(Brushes.Black, 
                (d, e) => ((CMYKColorControl)d).OnColorChanged(d, e)));
            CyanProperty = DependencyProperty.Register("Cyan", typeof(double),
                typeof(CMYKColorControl), new FrameworkPropertyMetadata((double)0, 
                (d,e)=>((CMYKColorControl)d).OnColorCMYKChanged(d,e)));
            MagentaProperty = DependencyProperty.Register("Magenta", typeof(double),
                typeof(CMYKColorControl), new FrameworkPropertyMetadata((double)0, 
                (d,e)=>((CMYKColorControl)d).OnColorCMYKChanged(d,e)));
            YellowProperty = DependencyProperty.Register("Yellow", typeof(double),
                typeof(CMYKColorControl), new FrameworkPropertyMetadata((double)0, 
                (d,e)=>((CMYKColorControl)d).OnColorCMYKChanged(d, e)));
            KeyProperty = DependencyProperty.Register("Key", typeof(double),
                typeof(CMYKColorControl), new FrameworkPropertyMetadata((double)0, 
                (d,e)=>((CMYKColorControl)d).OnColorCMYKChanged(d,e)));

        }
        public double key;
    
        public double Cyan
        {
            get { return (double)GetValue(CyanProperty); }
            set { SetValue(CyanProperty, value); }
        }
        public double Magenta
        {
            get { return (double)GetValue(MagentaProperty); }
            set { SetValue(MagentaProperty, value); }
        }
        public double Yellow
        {
            get { return (double)GetValue(YellowProperty); }
            set { SetValue(YellowProperty, value); }
        }
        public double Key
        {
            get { return (double)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }
        public SolidColorBrush ColorValue
        {
            get { return (SolidColorBrush)GetValue(ColorValueProperty); }
            set { SetValue(ColorValueProperty, value); }
        }

        private void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var colorControl = (CMYKColorControl)d;

            if (!colorControl.IsVisible)
            {
                SolidColorBrush newColor = (SolidColorBrush)e.NewValue;
                double red = newColor.Color.R;
                double green = newColor.Color.G;
                double blue = newColor.Color.B;
                double r = red / 255;
                double g = green / 255;
                double b = blue / 255;
                key = (1 - (Math.Max(Math.Max(r, g), b))) ;
                colorControl.Key = key * 100;
                colorControl.Cyan = ((1 - r - key) / (1 - key)) * 100;
                colorControl.Magenta = ((1 - g - key) / (1 - key)) * 100;
                colorControl.Yellow = ((1 - b - key) / (1 - key)) * 100;
            }
        }
        private void OnColorCMYKChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var colorControl = (CMYKColorControl)d;
            byte[] RGB = CYMKToRGB(colorControl.Cyan, colorControl.Magenta, colorControl.Yellow, colorControl.Key);
            if (colorControl.IsVisible)
                colorControl.ColorValue = new SolidColorBrush(Color.FromRgb(RGB[0], RGB[1], RGB[2]));
        }
        private static byte[] CYMKToRGB(double cyan, double magenta, double yellow, double key)
        {
            double C = cyan / 100;
            double M = magenta / 100;
            double Y = yellow / 100;
            double K = key / 100;
            double R = 255 * (1 - C) * (1 - K);
            double G = 255 * (1 - M) * (1 - K);
            double B = 255 * (1 - Y) * (1 - K);
            byte[] RGB = new byte[3];
            RGB[0] = (byte)R;
            RGB[1] = (byte)G;
            RGB[2] = (byte)B;
            return RGB;
        }
    }
} 
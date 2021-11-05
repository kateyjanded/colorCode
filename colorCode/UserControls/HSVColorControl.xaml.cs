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
    /// Interaction logic for HSVColorControl.xaml
    /// </summary>
    public partial class HSVColorControl : UserControl
    {
        public static readonly DependencyProperty ColorValueProperty;
        public static readonly DependencyProperty HueProperty;
        public static readonly DependencyProperty SaturationProperty;
        public static readonly DependencyProperty BrightnessProperty;
        public HSVColorControl()
        {
            InitializeComponent();
        }
        static HSVColorControl()
        {
            HueProperty = DependencyProperty.Register("Hue", typeof(double), typeof(HSVColorControl),
                new FrameworkPropertyMetadata((double)0, (d,e)=>((HSVColorControl)d).OnHueChanged(d,e)));
            SaturationProperty = DependencyProperty.Register("Saturation", typeof(double), typeof(HSVColorControl),
                new FrameworkPropertyMetadata((double)100, (d,e)=>((HSVColorControl)d).OnHueChanged(d,e)));
            BrightnessProperty = DependencyProperty.Register("Brightness", typeof(double), typeof(HSVColorControl),
                new FrameworkPropertyMetadata((double)0, (d,e)=>((HSVColorControl)d).OnHueChanged(d,e)));
            ColorValueProperty = DependencyProperty.Register("ColorValue", typeof(SolidColorBrush),
               typeof(HSVColorControl), new FrameworkPropertyMetadata(Brushes.Black,
               (d, e) => ((HSVColorControl)d).OnColorChanged(d, e)));
        }
        public double Hue
        {
            get { return (double)GetValue(HueProperty); }
            set { SetValue(HueProperty, value); }
        }
        public double Saturation
        {
            get { return (double)GetValue(SaturationProperty); }
            set { SetValue(SaturationProperty, value); }
        }
        public double Brightness
        {
            get { return (double)GetValue(BrightnessProperty); }
            set { SetValue(BrightnessProperty, value); }
        }
        public SolidColorBrush ColorValue
        {
            get { return (SolidColorBrush)GetValue(ColorValueProperty); }
            set { SetValue(ColorValueProperty, value); }
        }

        public static double hue;
        public static double saturation;
        public static double brightness;

        private void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var colorControl = (HSVColorControl)d;

            if (!colorControl.IsVisible)
            {              
                SolidColorBrush newColor = (SolidColorBrush)e.NewValue;
                SolidColorBrush oldColor = (SolidColorBrush)e.OldValue;
                double red = newColor.Color.R;
                double green = newColor.Color.G;
                double blue = newColor.Color.B;
                            
                UtilityClass ut = new UtilityClass();
                colorControl.Hue = ut.FindHu(red, green, blue);
                colorControl.Saturation = ut.FindSaturation(red, green, blue);
                colorControl.Brightness = ut.FindVal(red, green, blue);
            }
        }
        private void OnHueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var colorControl = (HSVColorControl)d;
            UtilityClass ut = new UtilityClass();
            byte[] RGB = ut.HSVToRGB(colorControl.Hue, colorControl.Saturation, colorControl.Brightness);
            
            if(colorControl.IsVisible)            
                colorControl.ColorValue = new SolidColorBrush(Color.FromRgb(RGB[0], RGB[1], RGB[2]));
        }
    }
}
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
    /// Interaction logic for ColorPickers.xaml
    /// </summary>
    public partial class RGBColorControl : UserControl
    {
        public static readonly DependencyProperty ColorValueProperty;
        public static readonly DependencyProperty RedProperty;
        public static readonly DependencyProperty GreenProperty;
        public static readonly DependencyProperty BlueProperty;
        public static readonly DependencyProperty AlphaProperty;


        static RGBColorControl()
        {
            ColorValueProperty= DependencyProperty.Register("ColorValue", typeof(SolidColorBrush),
                typeof(RGBColorControl), new FrameworkPropertyMetadata(Brushes.Black,
                FrameworkPropertyMetadataOptions.AffectsRender,
                (d,e)=>((RGBColorControl)d).OnColorChanged(d,e)));
            RedProperty=DependencyProperty.Register("RedPropertyValue", typeof(byte),
                typeof(RGBColorControl), new FrameworkPropertyMetadata((byte)0, 
                (d,e)=>((RGBColorControl)d).OnColorRGBChanged(d,e)));
            GreenProperty=DependencyProperty.Register("GreenPropertyValue", typeof(byte),
                typeof(RGBColorControl), new FrameworkPropertyMetadata((byte)0, 
                (d,e)=>((RGBColorControl)d).OnColorRGBChanged(d, e)));
            BlueProperty= DependencyProperty.Register("BluePropertyValue", typeof(byte),
                typeof(RGBColorControl), new FrameworkPropertyMetadata((byte)0,
                (d,e)=>((RGBColorControl)d).OnColorRGBChanged(d,e)));
            AlphaProperty = DependencyProperty.Register("AlphaPropertyValue", typeof(byte),
                typeof(RGBColorControl), new FrameworkPropertyMetadata((byte)255,
                (d, e) => ((RGBColorControl)d).OnColorRGBChanged(d, e)));
        }
        #region Dependency Properties
        public RGBColorControl()
        {
            InitializeComponent();
        }
        public byte RedPropertyValue
        {
            get { return (byte)GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }
        public byte AlphaPropertyValue
        {
            get { return (byte)GetValue(AlphaProperty); }
            set { SetValue(AlphaProperty, value); }
        }
        public byte BluePropertyValue
        {
            get { return (byte)GetValue(BlueProperty); }
            set { SetValue(BlueProperty, value); }
        }
        public byte GreenPropertyValue
        {
            get { return (byte)GetValue(GreenProperty); }
            set { SetValue(GreenProperty, value); }
        }
        public SolidColorBrush ColorValue
        {
            get { return (SolidColorBrush)GetValue(ColorValueProperty); }
            set { SetValue(ColorValueProperty, value); }
        }
        #endregion
 
        private void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var colorControl = (RGBColorControl)d;

            if (!colorControl.IsVisible)
            {
                SolidColorBrush newColor = (SolidColorBrush)e.NewValue;
                colorControl.RedPropertyValue = newColor.Color.R;
                colorControl.GreenPropertyValue = newColor.Color.G;
                colorControl.BluePropertyValue = newColor.Color.B;
                colorControl.AlphaPropertyValue = newColor.Color.A;
            }
        }

        private void OnColorRGBChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var colorControl = (RGBColorControl)d;
            if (colorControl.IsVisible)
                colorControl.ColorValue = new SolidColorBrush(Color.FromArgb(colorControl.AlphaPropertyValue, 
                    colorControl.RedPropertyValue, colorControl.GreenPropertyValue, colorControl.BluePropertyValue));
        }
    }
}

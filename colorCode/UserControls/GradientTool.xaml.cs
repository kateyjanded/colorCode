using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace colorCode.UserControls
{
    /// <summary>
    /// Interaction logic for GradientTool.xaml
    /// </summary>
    public partial class GradientTool : UserControl
    {
        public GradientTool()
        {
            InitializeComponent();
            
        }


        public double Position
        {
            get { return (double)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }


        public SolidColorBrush Color
        {
            get { return (SolidColorBrush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        public bool Isselected
        {
            get { return (bool)GetValue(IsselectedProperty); }
            set { SetValue(IsselectedProperty, value); }
        }
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }
        public static readonly DependencyProperty MinimumProperty =
           DependencyProperty.Register("Minimum", typeof(double), typeof(GradientTool),
               new FrameworkPropertyMetadata(0.00, new PropertyChangedCallback(SetMinimumValue)));

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(GradientTool),
                new FrameworkPropertyMetadata(0.00, new PropertyChangedCallback(SetMaximumValue)));

        // Using a DependencyProperty as the backing store for Isselected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsselectedProperty =
            DependencyProperty.Register("Isselected", typeof(bool), typeof(GradientTool), new FrameworkPropertyMetadata(false));

        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(double), typeof(GradientTool), new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(OnValueChanged),
                    new CoerceValueCallback(CreateCoerceValue)));


        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(SolidColorBrush), typeof(GradientTool), new FrameworkPropertyMetadata(Brushes.Black));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var newval = (double)e.NewValue;
            var oldval = (double)e.OldValue;
            
        }
        private static object CreateCoerceValue(DependencyObject d, object baseValue)
        {
            var thumb = d as GradientTool;
            if ((double)baseValue > thumb.Maximum)
            {
                return thumb.Maximum;
            }
            if ((double)baseValue < thumb.Minimum)
            {
                return thumb.Minimum;
            }
            //thumb.Position = (double)baseValue;
            return baseValue;
        }
        private static void SetMaximumValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as GradientTool;
            var max = (double)e.NewValue;
            if (max < control.Minimum)
            {
                control.Minimum = max;
            }
        }

        private static void SetMinimumValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as GradientTool;
            var min = (double)e.NewValue;
            if (min > control.Maximum)
            {
                control.Maximum = min;
            }
        }

        // Using a DependencyProperty as the backing store for Position.  This enables animation, styling, binding, etc...
        
        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var thumb = sender as Thumb;
            GradientTool thumbparent = (GradientTool)thumb.Parent;
            thumbparent.Isselected = true;
            thumbparent.Position = Position + e.VerticalChange;
        }

        private void grdGradient_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void GradienTool_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void Thumb_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var thumb = sender as Thumb;
            GradientTool thumbparent = (GradientTool)thumb.Parent;
            thumbparent.Isselected = true;
            Path p = new Path();
            p.Fill = Brushes.Black;
            ControlTemplate ct = new ControlTemplate(typeof(GradientTool));

            thumbparent.BorderThickness = new Thickness(3);
        }

       
    }
}

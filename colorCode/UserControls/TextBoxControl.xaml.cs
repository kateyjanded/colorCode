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
    /// Interaction logic for TextBoxControl.xaml
    /// </summary>
    public partial class TextBoxControl : UserControl
    {
        public TextBoxControl()
        {
            InitializeComponent();
        }
        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }
        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(TextBoxControl), 
                new FrameworkPropertyMetadata((int)0, new PropertyChangedCallback(OnValueChanged),
                    new CoerceValueCallback(CreateCoerceValue)));

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(int), typeof(TextBoxControl), 
                new FrameworkPropertyMetadata((int)0, new PropertyChangedCallback(SetMinimumValue)));
        
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(int), typeof(TextBoxControl), 
                new FrameworkPropertyMetadata((int)0, new PropertyChangedCallback(SetMaximumValue)));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var text = d as TextBoxControl;
            var newval = (int)e.NewValue;
            var oldval = (int)e.OldValue;

            text.Value = newval;
        }
        private static void SetMaximumValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as TextBoxControl;
            var max = (int)e.NewValue;
            if (max < control.Minimum)
            {
                control.Minimum = max;
            }
        }
        private static void SetMinimumValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as TextBoxControl;
            var max = (int)e.NewValue;
            if (max < control.Maximum)
            {
                control.Maximum = max;
            }
        }
        private static object CreateCoerceValue(DependencyObject d, object baseValue)
        {
            var textboxcontrol = d as TextBoxControl;
            if ((int)baseValue > textboxcontrol.Maximum)
            {
                return textboxcontrol.Maximum;
            }
            if ((int)baseValue< textboxcontrol.Minimum)
            {
                return textboxcontrol.Minimum;
            }
            textboxcontrol.txtValue.Text = baseValue.ToString();
            return baseValue;
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                var Values = int.Parse(txtValue.Text);
                if (Values < Maximum)
                {
                    Values++;
                    Value = Values;
                }
            }
            catch (Exception)
            {
                txtValue.Text = Maximum.ToString();
            }
        }
        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var val = int.Parse(txtValue.Text);
                if (val>Minimum)
                {
                    val--;
                    Value = val;
                }
            }
            catch (Exception)
            {
                txtValue.Text = Minimum.ToString();
            }
        }
        private void txtValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtValue.Text != "")
            {
                try
                {
                    Value = int.Parse(txtValue.Text);
                }
                catch (Exception)
                {
                    Value=0;
                }
            }
        }
    }
}

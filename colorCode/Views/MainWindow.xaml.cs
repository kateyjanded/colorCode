using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            
            DataContext = this; 
        }
        public event EventHandler ReturnColorEvent1;
        public event EventHandler ReturnColorEvent2;
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged!= null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private SolidColorBrush myColor;
        public SolidColorBrush MyColor
        {
            get { return myColor; }
            set { myColor = value;
                OnPropertyChanged();
            }
        }
        public SolidColorBrush Select { get; set; }
        public void Apply()
        {
            
        }
        private void cmbModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (cmbModel.SelectedItem == RGB)
            {
                grdRGB.Visibility= Visibility.Visible;
                grdHSV.Visibility = Visibility.Collapsed;
                grdCYMK.Visibility = Visibility.Collapsed;
                grdGray.Visibility = Visibility.Collapsed;
            }
            else if (cmbModel.SelectedItem == HSV)
            {
                grdHSV.Visibility = Visibility.Visible;
                grdCYMK.Visibility = Visibility.Collapsed;
                grdGray.Visibility = Visibility.Collapsed;
                grdRGB.Visibility = Visibility.Collapsed;
            }
            else if (cmbModel.SelectedItem == CMYK)
            {
                grdCYMK.Visibility = Visibility.Visible;
                grdGray.Visibility = Visibility.Collapsed;
                grdRGB.Visibility = Visibility.Collapsed;
                grdHSV.Visibility = Visibility.Collapsed;
            }
            else
            {
                grdGray.Visibility = Visibility.Visible;
                grdCYMK.Visibility = Visibility.Collapsed;
                grdRGB.Visibility = Visibility.Collapsed;
                grdHSV.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ReturnColorEvent1 != null)
            {
                ReturnColorEvent1.Invoke(this.DataContext, EventArgs.Empty);
            }
            else
            {
                ReturnColorEvent2.Invoke(this.DataContext, EventArgs.Empty);
            }
        }
    }
}

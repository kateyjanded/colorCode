using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace colorCode.ViewModels
{
    public class GradientViewModel : BaseModel
    {
        #region Fields
        private Color colorName2 = Color.FromRgb(255,70,70);
        private Color colorName1 = Color.FromRgb(255, 70, 70);
        private SolidColorBrush selectedColor1;
        private SolidColorBrush selectedColor2;
        private double opacity =1;
        #endregion

        #region Constructor
        public GradientViewModel()
        {
            Select1Command = new DelegateCommand(Select1);
            Select2Command = new DelegateCommand(Select2);
        }
        #endregion

        #region Properties
        public Color ColorName1
        {
            get { return colorName1; }
            set
            {
                colorName1 = value;
                OnPropertyChanged();
            }
        }
        public Color ColorName2
        {
            get { return colorName2; }
            set
            {
                colorName2 = value;
                OnPropertyChanged();
            }
        }
        public SolidColorBrush SelectedColor1
        {
            get { return selectedColor1; }
            set { selectedColor1 = value; OnPropertyChanged(); }
        }
        public SolidColorBrush SelectedColor2
        {
            get { return selectedColor2; }
            set { selectedColor2 = value; OnPropertyChanged(); }
        }
        public double Opacities 
        {
            get { return opacity; }
            set { opacity = value;
                OnPropertyChanged();
            }
        }
    
        #endregion

        #region Methods
        public void Select1()
        {
            MainWindow Main = new MainWindow();
            Main.ReturnColorEvent1 += Main_ReturnColorEvent1;
            Main.ShowDialog();
        }

        private void Main_ReturnColorEvent1(object sender, EventArgs e)
        {
            var main = (MainWindow)sender;
            SelectedColor1 = (SolidColorBrush)main.cnvColorPalette.Background;
            ColorName1 = selectedColor1.Color;
        }

        public void Select2()
        {
            MainWindow Main = new MainWindow();
            Main.ReturnColorEvent2 += Main_ReturnColorEvent2;
            Main.ShowDialog();
        }
        private void Main_ReturnColorEvent2(object sender, EventArgs e)
        {
            var main = (MainWindow)sender;
            SelectedColor2 = (SolidColorBrush)main.cnvColorPalette.Background;
            ColorName2 = SelectedColor2.Color;
        }
     
        public ObservableCollection<Thumb> Thumbs { get; set; }
        public DelegateCommand Select1Command { get; set; }
        public DelegateCommand Select2Command { get; set; }
        #endregion
    }

}
using colorCode.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace colorCode
{
    /// <summary>
    /// Interaction logic for GradientBrush.xaml
    /// </summary>
    public partial class GradientBrush : Window, INotifyPropertyChanged
    {
        #region Fields
        private GradientStopCollection GradientStop;
        private Thumbs selectedThumb;
        private double canvasPosition = 350;
        private double position;
        private double minimum = 100;
        private double opacities = 100;
        private Thumbs oldThumb;
        private double maximum = 7000;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructor
        public GradientBrush()
        {
            InitializeComponent();
            DataContext = this;
            GradientStop = new GradientStopCollection();
            UpdateGradient();
            GetActualPosition(CanvasPosition);
        }
        #endregion

        #region Properties
        
        public double CanvasPosition
        {
            get { return canvasPosition; }
            set
            {
                canvasPosition = value;
                GetActualPosition(CanvasPosition);
                OnPropertyChanged();
            }
        }
        public double Opacities 
        {
            get { return opacities; }
            set
            {
                opacities = value;
                ChangeOpactity();
                OnPropertyChanged();
            }
        }
        public Thumbs SelectedThumb
        {
            get{return selectedThumb; }
            set
            {
                selectedThumb = value;
                OnSelectionChanged(OldThumb, SelectedThumb);
            }
        }
        public Thumbs OldThumb
        {
            get { return oldThumb; }
            set { oldThumb = value; }
        }
        public double Position
        {
            get{ return position; }
            set
            {
                position = value;
                OnPositionChanged(SelectedThumb, Position);
                OnPropertyChanged();
            }
        }
        public double Maximum
        {
            get{ return maximum; }
            set
            {
                maximum = value;
                GetActualPosition(CanvasPosition);
                OnPropertyChanged();
            }
        }
        public double Minimum
        {
            get { return minimum; }
            set
            {
                minimum = value;
                GetActualPosition(CanvasPosition);
                OnPropertyChanged();
            }
        }
        #endregion

        #region Methods
        private void CnvColor_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            for (int i = 0; i < cnvColor.Children.Count; i++)
            {
                var newvalue = e.NewSize.Height;
                Thumbs thumb = (Thumbs)cnvColor.Children[i];
                Canvas.SetTop(cnvColor.Children[i], thumb.Position/100 * newvalue);
                OldThumb = SelectedThumb;
                SelectedThumb = thumb;
            }
        }
        private void OnSelectionChanged(Thumbs oldThumb, Thumbs selectedthumb)
        {
            if (oldThumb != null && oldThumb != selectedthumb && selectedthumb != null)
            {
                Canvas.SetZIndex(oldThumb, 1);
                Canvas.SetZIndex(selectedthumb, cnvColor.Children.Count);
                var path = oldThumb.Template.FindName("tmb_Top", oldThumb) as Path;
                if (path != null)
                {
                    path.Stroke = Brushes.Blue;
                    path.StrokeThickness = 1;
                }
                TemplateChange(selectedthumb);
            }
        }
        public Color GetColor(GradientStopCollection gsc, double offset)
        {
            UtilityClass ut = new UtilityClass();
            return ut.GetColor(gsc, offset);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double nextstop = GetNextThumb(SelectedThumb);
            double offset = (SelectedThumb.Position  / 100 + nextstop / cnvColor.ActualHeight)/2;
            Thumbs thumb = new Thumbs();
            OldThumb = SelectedThumb;
            SelectedThumb = thumb;
            Color color = GetColor(GradientStop, offset);
            thumb.Height = 15;
            UpdateNewThumb(thumb, color, offset);
            ThumbTemplate(thumb);
            cnvColor.Children.Add(thumb);
            Position = thumb.Position;
            Canvas.SetTop(thumb, thumb.Position/100 *cnvColor.ActualHeight);
            thumb.DragDelta += thumb_DragDelta;
            thumb.PreviewMouseLeftButtonDown += tmbTop_PreviewMouseLeftButtonDown;
        }
        public void UpdateNewThumb(Thumbs thumb, Color color, double offset)
        {
            GradientStop.Add(new GradientStop { Color = color, Offset = offset });
            thumb.Color = new SolidColorBrush(color);
            thumb.Position = offset * 100;
        }
        private void ThumbTemplate(Thumbs thumb)
        {
            var element = new FrameworkElementFactory(typeof(Path));
            element.SetValue(Path.StrokeProperty, Brushes.Black);
            element.SetValue(Path.FillProperty, thumb.Color);
            element.SetValue(Path.DataProperty, Geometry.Parse("M 0,0 v10 H35 L40,5 L35,0 Z"));
            element.SetValue(Path.StrokeThicknessProperty, 3.0);
            element.Name = "tmb_Top";
            ControlTemplate ct = new ControlTemplate();
            ct.VisualTree = element;
            thumb.Template = ct;
        }
        public void GetActualPosition(double postion)
        {
            if (postion<=Maximum && postion>=Minimum)
            {
                double post =(Maximum- postion)/(Maximum - Minimum);
                Color color = GetColor(GradientStop, post);
                cnvColorPallette.Background = new SolidColorBrush(color);
            }
        }
        private double GetNextThumb(Thumbs thumb)
        {
            var childrens = new List<double>();
            double result = 0;
            for (int i = 0; i < cnvColor.Children.Count; i++)
            {
                childrens.Add(Canvas.GetTop(cnvColor.Children[i]));
            }
            childrens.Sort();
            for (int i = 0; i < childrens.Count; i++)
            {
                if (childrens[i].Equals(Canvas.GetTop(thumb)))
                {
                    if (i< childrens.Count-1)
                    {
                        result = childrens[i + 1];
                    }
                    else
                    {
                        result = childrens[i - 1];
                    }
                }
            }
            return result;
        }
        private void ChangeOpactity()
        {
            SelectedThumb.Color = new SolidColorBrush(Color.FromArgb((byte)(Opacities / 100 * 255),
                SelectedThumb.Color.Color.R, SelectedThumb.Color.Color.G, SelectedThumb.Color.Color.B));
            UpdateGradient();
        }
        private void thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var thumb = (Thumbs)sender;
            double offset = 0;
            Canvas.SetTop(thumb, Canvas.GetTop(thumb) + e.VerticalChange);
            if (Canvas.GetTop(thumb) < 0)
            {
                Canvas.SetTop(thumb, 0);
            }
            else if (Canvas.GetTop(thumb) > cnvColor.ActualHeight )
            {
                Canvas.SetTop(thumb, cnvColor.ActualHeight);
            }
            offset = Canvas.GetTop(thumb) / cnvColor.ActualHeight;
            thumb.Position = offset * 100;
            UpdateGradient();
            Position = thumb.Position;
        }
        public void UpdateGradient()
        {
            GradientStop.Clear();
            for (int i = 0; i < cnvColor.Children.Count; i++)
            {
                Thumbs thumb = (Thumbs)cnvColor.Children[i];
                var stop = new GradientStop { Color = thumb.Color.Color, Offset = thumb.Position / 100 };
                GradientStop.Add(stop);
            }
            cnvColor.Background = new LinearGradientBrush(GradientStop);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow Main = new MainWindow();
            Main.MyColor = SelectedThumb.Color;
            Main.ReturnColorEvent2 += Main_ReturnColorEvent2;
            Main.ShowDialog();
        }
        private void Main_ReturnColorEvent2(object sender, EventArgs e)
        {
            var main = (MainWindow)sender;
            SelectedThumb.Color = (SolidColorBrush)main.cnvColorPalette.Background;
            UpdateGradient();
            TemplateChange(SelectedThumb);
        }
        private void tmbTop_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OldThumb = SelectedThumb;
            Thumbs thumb = sender as Thumbs;
            Canvas.SetZIndex(thumb, cnvColor.Children.Count);
            if (thumb != SelectedThumb)
            {
                SelectedThumb = thumb;
            }
            TemplateChange(thumb);
            double offset = 0;
            offset = (Canvas.GetTop(thumb)) / cnvColor.ActualHeight;
            thumb.Position = offset * 100;
            Position = thumb.Position;
            Opacities = ((double)SelectedThumb.Color.Color.A) / 255 * 100;
        }
        private void TemplateChange(Thumbs thumb)
        {
            var path = thumb.Template.FindName("tmb_Top", thumb) as Path;
            if (path != null)
            {
                path.Stroke = Brushes.Black;
                path.Fill = SelectedThumb.Color;
                path.StrokeThickness = 3;
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (cnvColor.Children.Count>2)
            {
                cnvColor.Children.Remove(SelectedThumb);
                SelectedThumb = OldThumb;
                OldThumb = (Thumbs)cnvColor.Children[cnvColor.Children.Count - 2];
                UpdateGradient();
            }
            TemplateChange(SelectedThumb);
        }
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public void OnPositionChanged(Thumbs thumb, double position)
        {
            if (thumb != null)
            {
                thumb.Position = position;
                Canvas.SetTop(thumb, position / 100 * cnvColor.ActualHeight);
                UpdateGradient();
            }
        }
        #endregion
    }
}
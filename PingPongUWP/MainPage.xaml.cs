using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PingPongUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer disp;
        private Ellipse ellipse;
        private Rectangle rectangle;

        private double posX;
        private double posY;
        private double dX;
        private double dY;


        public MainPage()
        {
            this.InitializeComponent();

            disp = new DispatcherTimer();
            disp.Tick += Disp_Tick;
            disp.Interval = new TimeSpan(1);
            disp.Start();

            ellipse = new Ellipse();
            ellipse.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            ellipse.Width = 30;
            ellipse.Height = 30;
            ellipse.Margin = new Thickness(0, 0, 0, 0);
            ellipse.HorizontalAlignment = HorizontalAlignment.Left;
            ellipse.VerticalAlignment = VerticalAlignment.Top;
            gridBoard.Children.Add(ellipse);

            rectangle = new Rectangle();
            rectangle.Fill = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
            rectangle.Width = 70;
            rectangle.Height = 30;
            rectangle.HorizontalAlignment = HorizontalAlignment.Left;
            rectangle.VerticalAlignment = VerticalAlignment.Top;
            rectangle.Margin = new Thickness(Window.Current.Bounds.Width/2, Window.Current.Bounds.Height-50, 0, 0);
            gridBoard.Children.Add(rectangle);

            dX = 5;
            dY = 1;
            posX = (Window.Current.Bounds.Width / 2) - ((Window.Current.Bounds.Width / 2) % dX);
            posY = Window.Current.Bounds.Height / 2;

            Window.Current.CoreWindow.KeyDown += (s, e) =>
            {
                if (e.VirtualKey == Windows.System.VirtualKey.Left) rectangle.Margin = new Thickness(rectangle.Margin.Left - 10, rectangle.Margin.Top, 0, 0);
                if (e.VirtualKey == Windows.System.VirtualKey.Right) rectangle.Margin = new Thickness(rectangle.Margin.Left + 10, rectangle.Margin.Top, 0, 0);
            };

            Window.Current.CoreWindow.PointerMoved += (s, e) =>
            {
                rectangle.Margin = new Thickness(e.CurrentPoint.Position.X, rectangle.Margin.Top, 0, 0);
            };
        }

        private void Disp_Tick(object sender, object e)
        {
            //if (posY > Window.Current.Bounds.Height - ellipse.Height || posY < 0) dY *= -1;
            //if (posX > Window.Current.Bounds.Width - ellipse.Width || posX < 0) dX *= -1;
            if (posY < 0) dY *= -1;
            if (posX > Window.Current.Bounds.Width - ellipse.Width || posX < 0) dX *= -1;

            if (posY > rectangle.Margin.Top-30 && (posX > rectangle.Margin.Left && posX < rectangle.Margin.Left + 70)) dY *= -1;

            posX = dX + posX;
            posY = dY + posY;
            ellipse.Margin = new Thickness(posX, posY, 0, 0);

            textBlock.Text = String.Concat("posX: ", posX, "\n", "posY: ", posY, "\n", "dX: ", dX, "\n", "dY: ", dY, "\n", "rectangle: ",rectangle.Margin.Left);
        }
    }
}
